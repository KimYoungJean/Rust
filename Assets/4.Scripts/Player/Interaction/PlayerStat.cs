using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviourPun
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Slider healthSlider;
    public Text healthCount;


    private void Start()
    {        

        healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;

    }

    //�÷��̾ ���ݹ޾��� ��,

    [PunRPC] 
    public void TakeDamage(float damage)
    {
        if (!photonView.IsMine)  return;

        currentHealth -= damage;
        Debug.Log($"�÷��̾ {damage}�� �������� �Ծ����ϴ�. ���� ü�� : {currentHealth}");

        healthSlider.value = currentHealth/100;
        healthCount.text = (currentHealth/100).ToString();

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
