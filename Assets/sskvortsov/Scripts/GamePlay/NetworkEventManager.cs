using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace sskvortsov.Scripts.GamePlay
{
    public class NetworkEventManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == RemoteEventsNames.LeftPaddleMove)
            {
                BoatController.LeftRotate();
            }

            if (photonEvent.Code == RemoteEventsNames.LiveChange)
            {
                int change = (int) photonEvent.CustomData;
                if (change > 0)
                {
                    GameManager.Instance.DecreaseLives();
                }
                else
                {
                    GameManager.Instance.IncreaseLives();
                }
            }
        }

        public static void SendChangeLiveEvent(int i)
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.Others};
            SendOptions sendOptions = new SendOptions {Reliability = true};
            PhotonNetwork.RaiseEvent(RemoteEventsNames.LiveChange, i, raiseEventOptions, sendOptions);
        }

        private void Awake()
        {
            Debug.LogWarning("AVAILABLE CHEATS");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                BoatController.isLeftPressed = true;
            }
        }

        #region OnEnable/OnDisable

        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
            base.OnEnable();
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
            base.OnDisable();
        }

        #endregion

    }
}