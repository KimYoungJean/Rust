using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//MonoBehaviourPun�� MonoBehaviourPunCallBack�� ����: MonoBehaviourPunCallBack�� PhotonNetwork�� �ݹ��Լ��� ����� �� �ְ� ���ִ� �������̽��̴�.
public class PlayerAttack : MonoBehaviourPun
{
    public float AttackRange = 1.5f;
    public float AttackDamage = 10f;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            Attack();
        }
    }


    private void Attack()
    {

        GameManager.instance.playerAction = Define.PlayerAction.Attack;     
        int randAttack = Random.Range(0, 2);
        animator.SetTrigger($"Attack{randAttack}");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange))
        {
            PlayerStat targetHealth = hit.transform.GetComponent<PlayerStat>();
            if (targetHealth != null)
            {
                targetHealth.photonView.RPC("TakeDamage", RpcTarget.All, AttackDamage);
            }
        }
        GameManager.instance.playerAction = Define.PlayerAction.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // �浹ó��             
        }
    }
}
