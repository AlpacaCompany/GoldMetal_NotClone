using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigid;
    bool CanJump = true;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // 리지드 바디 2D 가져오기
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Jump(450);
        
        RunOrStay();
    }

    public void Move() { //이동 함수
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { //방향키 or wasd 눌렀을때
            rigid.velocity +=  new Vector2(0.1f * speed, 0); //움직이기 (속력)
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { //방향키 or wasd 눌렀을때
            rigid.velocity +=  new Vector2(-0.1f * speed, 0); // 움직이기(속력)
        }
        if(Mathf.Abs(rigid.velocity.x) > 5) {
            rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * 5 , rigid.velocity.y);
        }
        if(rigid.velocity.x < 0) { //왼쪽으로 가고 있을때
            GetComponent<SpriteRenderer>().flipX = true; //뒤집기
        } else {
            GetComponent<SpriteRenderer>().flipX = false; // 뒤집기 취소
        }

    }
    
    public void Jump(float JumpForce) { //점프 함수
        if(Input.GetKeyDown(KeyCode.Space) && CanJump) { //스페이스바를 눌렀을때
            animator.SetBool("IsGround", false); //기본 애니 비활성화
            animator.SetBool("IsJumping", true); //점프 애니 활성화
            rigid.velocity = new Vector2(rigid.velocity.x , 0); //가속도 초기화
            rigid.AddForce(Vector2.up * JumpForce * Time.fixedDeltaTime , ForceMode2D.Impulse); // 위로 힘 주기(점프)
            CanJump = false; //점프 비활성화
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        animator.SetBool("IsJumping", false); //점프 애니메이션 비활성화
        animator.SetBool("IsGround" , true); //기본 애니메이션 활성화
        CanJump = true; //점프 가능하게 설정
    }

    public void RunOrStay() {
        if(Mathf.Abs(rigid.velocity.x) >= 0.5f) { //가속도가 1보다 크면
            animator.SetBool("IsGround" , true); //기본 애니메이션 활성화
            animator.SetBool("IsRunning", true); //뛰는 애니메이션 활성화
        } else {
            animator.SetBool("IsGround" , true); //기본 애니메이션 활성화
            animator.SetBool("IsRunning", false); //뛰는 애니메이션 비활성화
        }
    }
}
