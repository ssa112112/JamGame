using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace sskvortsov.Scripts
{
    public class NetworkLobbyManager : MonoBehaviourPunCallbacks
    {
        const string ConnectionStatusPrefix = " Connection status: ";
        const string ConnectionStatusPrefixError = " FAILED: ";
        const float ErrorDisplayTime = 5f;
        [SerializeField] private string sceneName = "1";

        float _errorDisplayTimeLeft;

        [SerializeField] TMP_Text connectionStatusText;

        //For make these buttons interactable, when connect has been set
        [SerializeField] Button createGameButton, joinRoomButton;

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

        #region Handling fails //Just report to user

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
            connectionStatusText.text = ConnectionStatusPrefixError + message;
            _errorDisplayTimeLeft = ErrorDisplayTime;
        }

        #endregion

        public override void OnConnectedToMaster()
        {
            joinRoomButton.interactable = true;
            createGameButton.interactable = true;
        }

        public override void OnJoinedRoom()
        {
            int i = Random.Range(1, 4);
            PhotonNetwork.LoadLevel(i.ToString());
        }

        void Update()
        {
            if (_errorDisplayTimeLeft <= 0)
                connectionStatusText.text = ConnectionStatusPrefix + PhotonNetwork.NetworkClientState;
            else
                _errorDisplayTimeLeft -= Time.deltaTime;
        }

        public static void CreateRoom(int roomID)
        {
            //Set properties
            RoomOptions roomOptions = new RoomOptions();

            roomOptions.PlayerTtl = 250; //diconectTime
            roomOptions.MaxPlayers = 2; //MaxPlayers
            /*
        roomOptions.CustomRoomProperties = new Hashtable
        {
            {RoomOptionKeys.PlayersInRoom, (byte) playersInRoom}, //MinPlayers, in this case == MaxPlayers
            {RoomOptionKeys.MapSize, (byte) mapSize},
            {RoomOptionKeys.GameSpeed, (byte) gameSpeed},
            {RoomOptionKeys.GameTimeInSeconds, (byte) gameTime}
        };
        */
            //Create room
            PhotonNetwork.CreateRoom(roomID.ToString(),roomOptions);
        }

        public static void CreateRandomRoom()
        {
            CreateRoom(Random.Range(-32000, 32000));
        }

        public static void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public static void JoinRoomByID(int roomID)
        {
            PhotonNetwork.JoinRoom(roomID.ToString());
        }

        public static void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}