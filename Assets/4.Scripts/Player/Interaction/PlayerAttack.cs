using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//MonoBehaviourPun�� MonoBehaviourPunCallBack�� ����: MonoBehaviourPunCallBack�� PhotonNetwork�� �ݹ��Լ��� ����� �� �ְ� ���ִ� �������̽��̴�.
public class PlayerAttack : MonoBehaviourPun
{
    #region ������ ����
    public float punchRange = 1.5f;
    public float punchDamage = 10f;

    public float daggerRange = 1.5f;
    public float daggerDamage = 20f;

    public float gunRange = 15f;
    public float gunDamage = 30f;
    public bool isFocus = false;


    #endregion
        
    public GameObject dagger;
    public GameObject gun;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();


    }


    private void Update()
    {
        Debug.Log($"Weapon : {GameManager.instance.Weapon}");

        if (!photonView.IsMine)
        {
            return;
        }

        //1��Ű �� ���� �ֹ��� Ȱ��ȭ ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {          
            gun.SetActive(!gun.activeSelf);    
            dagger.SetActive(false);

            if (gun.activeSelf)
            {   GameManager.instance.Weapon = Define.Weapon.Gun;                
                animator.SetInteger("weaponType", (int)Define.Weapon.Gun);                
            }
            else
            {
                GameManager.instance.Weapon = Define.Weapon.Punch;                
                animator.SetInteger("weaponType", (int)Define.Weapon.Punch);
            }

        }
        if(gun.activeSelf)
        {
            Debug.Log($"isFocus: {isFocus}");
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isFocus = !isFocus;
                animator.SetBool("isFocus", isFocus);
            }
        }
        // 3��Ű �� ���� �ܰ� Ȱ��ȭ ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isFocus = false;
            
            dagger.SetActive(!dagger.activeSelf);
            gun.SetActive(false);

            if (dagger.activeSelf)
            {
                GameManager.instance.Weapon = Define.Weapon.Dagger;                
                animator.SetInteger("weaponType", (int)Define.Weapon.Dagger);

            }
            else
            {
                GameManager.instance.Weapon = Define.Weapon.Punch;                
                animator.SetInteger("weaponType", (int)Define.Weapon.Punch);
            }

        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attack");
            Attack();
        }


    }


    private void Attack()
    {

        switch (GameManager.instance.Weapon)
        {
            case Define.Weapon.Punch:

                Punch();
                break;
            case Define.Weapon.Dagger:
                Dagger();
                break;
        }



    }

    private void Punch()
    {


        int randAttack = Random.Range(0, 2);
        animator.SetTrigger($"Attack{randAttack}");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, punchRange))
        {
            PlayerStat targetHealth = hit.transform.GetComponent<PlayerStat>();
            if (targetHealth != null)
            {
                targetHealth.photonView.RPC("TakeDamage", RpcTarget.All, punchDamage);
            }
        }
    }
    private void Dagger()
    {
        Debug.Log("Dagger Attack");
        animator.SetTrigger("AttackDagger");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, daggerRange))
        {
            PlayerStat targetHealth = hit.transform.GetComponent<PlayerStat>();
            if (targetHealth != null)
            {
                targetHealth.photonView.RPC("TakeDamage", RpcTarget.All, daggerDamage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // �浹ó��             
        }
    }
}
