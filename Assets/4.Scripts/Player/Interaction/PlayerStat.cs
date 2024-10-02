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

    //플레이어가 공격받았을 때,

    [PunRPC] 
    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine)  return;

        currentHealth -= damage;
        Debug.Log($"플레이어가 {damage}의 데미지를 입었습니다. 남은 체력 : {currentHealth}");
        if (currentHealth <= 0)
        { Die(); }
    }

    // 사망처리
    void Die()
    {
        //사망 애니메이션.
        Debug.Log("사망");
        PhotonNetwork.Destroy(gameObject);
    }
}
