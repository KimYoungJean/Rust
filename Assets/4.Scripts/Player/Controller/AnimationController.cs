using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;  // 애니메이터 컴포넌트

    private bool isCrouch;
    private bool isWalk;
    private bool isRun;
    private bool isJump;

    private float xDir;
    private float yDir;


    float horizontal;
    float vertical;


    private void Start()
    {

        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        
        switch(GameManager.instance.playerState)
        {
            case Define.PlayerState.Idle:
                isCrouch = false;
                isWalk = false;
                isRun = false;
                isJump = false;
                break;
            case Define.PlayerState.Walk:
                isCrouch = false;
                isWalk = true;
                isRun = false;
                isJump = false;
                break;
            case Define.PlayerState.Run:
                isCrouch = false;
                isWalk = false;
                isRun = true;
                isJump = false;
                break;
            case Define.PlayerState.Crouch:
                isCrouch = true;
                isWalk = false;
                isRun = false;
                isJump = false;
                break;
            case Define.PlayerState.Jump:
                isCrouch = false;
                isWalk = false;
                isRun = false;
                isJump = true;
                break;
        }

        animator.SetBool("isCrouch", isCrouch);
        animator.SetBool("isWalk", isWalk);
        animator.SetBool("isRun", isRun);
        animator.SetBool("isJump", isJump);


        animator.SetFloat("xDir", horizontal);
        animator.SetFloat("yDir", vertical);
    }

    
}
