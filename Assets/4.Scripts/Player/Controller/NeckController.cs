using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NeckController : MonoBehaviourPunCallbacks
{
    
    public Transform Shoot;

    private void Update()
    {
        if (PhotonNetwork.IsConnected && !photonView.IsMine)
        {
            return;
        }


        transform.LookAt(Shoot);
    }
}
