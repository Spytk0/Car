using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public TMP_InputField playerName;
    byte maxPlayerPerRoom = 3;
    bool isConnecting;
    public TextMeshProUGUI networkText;
    string gameVersion = "2";

   
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if(PlayerPrefs.HasKey("PlayerName"))
        {
            playerName.text = PlayerPrefs.GetString("PlayerName");
        }
    }
    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName",name);
    }
    public void StartTrial()
    {
        SceneManager.LoadScene(0);
        OnConnectedToMaster()
    }

    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "OnConnectToMaster...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode,string message)
    {
        networkText.text += "Failed to join random room.\n";
        PhotonNetwork.CreateRoom(null,new RoomOptions { MaxPlayers = this.maxPlayerPerRoom});
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because" + cause + "\n";
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        networkText.text = "Joined Room with" + PhotonNetwork.CurrentRoom.PlayerCount + "players.\n";
        PhotonNetwork.LoadLevel("complete_track_demo");
    }

    public void ConnectNetwork()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = playerName.text;

        if(PhotonNetwork.IsConnected)
        {
            networkText.text += "Join Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting...\n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
