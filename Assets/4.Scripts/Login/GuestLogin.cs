using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GuestLogin : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() // ���� ����
    {
        //�Խ�Ʈ ����ڷ� �뿡 �����ϱ�.
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();// �κ� ����
    }

    public override void OnJoinedLobby() // �κ� ����
    {
        Debug.Log("Joined Lobby");
        //���� �����մϴ�. �� �̸��� (Room)


        PhotonNetwork.JoinRandomRoom(); //������ �濡 ����.
       
    }
    public override void OnJoinRandomFailed(short returnCode, string message) // ���� �� ���� ����
    {
        Debug.Log("Failed to join a random room. Creating a new room.");

        // ���� ������ "Room"�̶�� �̸����� ���� �����մϴ�.
        PhotonNetwork.CreateRoom("Room", new RoomOptions { MaxPlayers = 10 });
    }

    public override void OnCreatedRoom() // �� ����
    {
        Debug.Log("Created Room");
        //�� ������ �÷��̾ �ڵ����� �濡 �߰� �մϴ�.

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

    // OnplayerEnteredRoom�� OnJoinedRoom�� ���� ����� �մϴ�.
    // �������� : OnplayerEnteredRoom�� �ٸ� �÷��̾ �濡 ���ö� ȣ��ǰ�
    // OnJoinedRoom�� �ڽ��� �濡 ���ö� ȣ��˴ϴ�.

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player " + otherPlayer.NickName + " left room");
    }
}