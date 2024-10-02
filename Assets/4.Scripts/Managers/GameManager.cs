using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Define.PlayerState playerState { get; set ; } = Define.PlayerState.Idle;
    public Define.PlayerAction playerAction { get; set; } = Define.PlayerAction.None;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        PhotonNetwork.Instantiate("PlayerPrefab", new Vector3(0, 0, 0), Quaternion.identity);
    }




}
