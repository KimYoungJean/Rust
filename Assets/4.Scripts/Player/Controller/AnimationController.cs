using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;  // 애니메이터 컴포넌트


    private void Start()
    {

        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        
        

        if (animator == null)
        {
            return;
        }

        switch (GameManager.instance.playerState)
        {
            case Define.PlayerState.Crouch:
                animator.Play("Crouch");
                break;
            case Define.PlayerState.Walk:
                animator.Play("Walk");
                break;
            case Define.PlayerState.Run:
                animator.Play("Run");
                break;
            case Define.PlayerState.Idle:                
                animator.Play("Idle");
                break;


        }



    }




}
