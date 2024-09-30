using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GuestLogin : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() // 서버 연결
    {
        //게스트 사용자로 룸에 참여하기.
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();// 로비에 참여
    }

    public override void OnJoinedLobby() // 로비 참여
    {
        Debug.Log("Joined Lobby");
        //방을 생성합니다. 방 이름은 (Room)


        PhotonNetwork.JoinRandomRoom(); //랜덤한 방에 참여.
       
    }
    public override void OnJoinRandomFailed(short returnCode, string message) // 랜덤 방 참여 실패
    {
        Debug.Log("Failed to join a random room. Creating a new room.");

        // 방이 없으면 "Room"이라는 이름으로 방을 생성합니다.
        PhotonNetwork.CreateRoom("Room", new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnCreatedRoom() // 방 생성
    {
        Debug.Log("Created Room");
        //방 생성후 플레이어를 자동으로 방에 추가 합니다.

        PhotonNetwork.LocalPlayer.NickName = "Guest" + Random.Range(0, 1000).ToString("0000");
       
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString());
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " entered room");
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("map");
    }

    // OnplayerEnteredRoom과 OnJoinedRoom은 같은 기능을 합니다.
    // 차이점은 : OnplayerEnteredRoom은 다른 플레이어가 방에 들어올때 호출되고
    // OnJoinedRoom은 자신이 방에 들어올때 호출됩니다.

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + " left room");
    }
}