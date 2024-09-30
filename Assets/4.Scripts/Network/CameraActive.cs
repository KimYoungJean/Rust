using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraActive : MonoBehaviourPunCallbacks
{
    public void Start()
    {
        GetComponent<Camera>().enabled = photonView.IsMine;
        GetComponent<AudioListener>().enabled = photonView.IsMine;
    }
}

