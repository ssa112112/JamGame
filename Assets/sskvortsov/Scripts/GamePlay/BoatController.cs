using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace sskvortsov.Scripts.GamePlay
{
    public class BoatController : MonoBehaviour, IOnEventCallback
    {
        public float SpeedRotate = 5f;
        public float SpeedForward = 0.005f;

        [SerializeField]
        private bool useKeys = true;

        private static BoatController Instance;

        private void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ForwardMove();
            }

            if (!useKeys)
            {
                return;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    RightRotate();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    SendLeftPaddleMoveEvent();
                }
            }
        }

        public static void RightRotate()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("TRY MOVE BOAT FROM NOT MASTER CLIENT");
                return;
            }

            Debug.Log("RightRotate");
            Instance.transform.Rotate(0, Time.deltaTime * -Instance.SpeedRotate, 0);
        }

        public static void LeftRotate()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("TRY MOVE BOAT FROM NOT MASTER CLIENT");
                return;
            }

            Debug.Log("LeftRotate");
            Instance.transform.Rotate(0, Time.deltaTime * Instance.SpeedRotate,0);
        }

        public static void ForwardMove()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("TRY MOVE BOAT FROM NOT MASTER CLIENT");
                return;
            }

            Instance.transform.Translate(-Instance.SpeedForward,0,0);
        }

        public void OnEvent(EventData photonEvent) { }

        public static void SendLeftPaddleMoveEvent()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.Others};
            SendOptions sendOptions = new SendOptions {Reliability = true};
            PhotonNetwork.RaiseEvent(RemoteEventsNames.LeftPaddleMove, true, raiseEventOptions, sendOptions);
        }
    }
}
