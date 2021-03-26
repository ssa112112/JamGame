using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //If we already connected then return
        if (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != PeerStateValue.Disconnected)
            return;

        //Set settings
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.0.1";

        //Connect
        PhotonNetwork.ConnectUsingSettings();
    }

    #region Handling fails

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ReportFail(message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        ReportFail(message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ReportFail(message);
    }

    void ReportFail(string message)
    {
        Debug.Log(message);
    }

    #endregion

    public override void OnConnectedToMaster()
    {
        var test = new OpJoinRandomRoomParams();
        PhotonNetwork.NetworkingClient.OpJoinRandomOrCreateRoom(null, null);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        PhotonNetwork.LoadLevel("Gameplay");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.LoadLevel("Gameplay");
    }
}