using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerStat : MonoBehaviourPun
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    //�÷��̾ ���ݹ޾��� ��,

    [PunRPC] 
    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine)  return;

        currentHealth -= damage;
        Debug.Log($"�÷��̾ {damage}�� �������� �Ծ����ϴ�. ���� ü�� : {currentHealth}");
        if (currentHealth <= 0)
        { Die(); }
    }

    // ���ó��
    void Die()
    {
        //��� �ִϸ��̼�.
        Debug.Log("���");
        PhotonNetwork.Destroy(gameObject);
    }
}
