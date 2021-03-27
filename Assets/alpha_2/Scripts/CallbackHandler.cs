using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace alpha_2.Scripts
{
    public class CallbackHandler : MonoBehaviourPunCallbacks
    {
        public override void OnLeftRoom()
        {
            Debug.Log("GoodBuy");
            PhotonNetwork.LoadLevel("Alpha2Menu");
            base.OnLeftRoom();
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
