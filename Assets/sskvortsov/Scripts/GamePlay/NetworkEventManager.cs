using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

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