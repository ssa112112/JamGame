using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace sskvortsov.Scripts.GamePlay
{
    public class BoatController : MonoBehaviour, IOnEventCallback
    {
        [SerializeField]
        public int TimeBeforeAddImpulse;

        [SerializeField]
        public int ImpulseLiveTime;

        [SerializeField]
        public Vector3 RotateAddForce;

        [SerializeField]
        private Vector3 VectorRotate;

        [SerializeField]
        private Vector3 VectorForward;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private bool useKeys = true;

        private static BoatController Instance;

        public static bool isRightPressed = false;
        public static bool isLeftPressed = false;

        private void Awake()
        {
            Instance = this;
            if (PhotonNetwork.IsMasterClient)
            {
                _rigidbody = GetComponent<Rigidbody>();
                _rigidbody.AddForce(Instance.VectorForward);
            }
        }

        private void Update()
        {
            Debug.Log($"velocity: {_rigidbody.velocity}");
            Debug.Log($"angularVelocity: {_rigidbody.angularVelocity}");

            if (!useKeys)
            {
                return;
            }

            if (PhotonNetwork.IsMasterClient)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    isRightPressed = true;
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

        void FixedUpdate()
        {
            if (isRightPressed)
            {
                RightRotate();
                isRightPressed = false;
            }

            if (isLeftPressed)
            {
                LeftRotate();
                isLeftPressed = false;
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

            Instance._rigidbody.AddTorque(-Instance.VectorRotate);
            AddForceAfterWait().Forget();
        }

        private static async UniTask AddForceAfterWait()
        {
            await UniTask.Delay(Instance.TimeBeforeAddImpulse);
            Debug.Log("AddForceAfterWait");
            Instance._rigidbody.AddRelativeForce(Instance.RotateAddForce, ForceMode.Impulse);
            await UniTask.Delay(Instance.ImpulseLiveTime);
            Debug.Log("AddForceAfterWaitReturn");
            Instance._rigidbody.AddRelativeForce(-Instance.RotateAddForce, ForceMode.Impulse);
        }

        public static void LeftRotate()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("TRY MOVE BOAT FROM NOT MASTER CLIENT");
                return;
            }

            Debug.Log("LeftRotate");
            Instance._rigidbody.AddTorque(Instance.VectorRotate);
            AddForceAfterWait().Forget();
        }
/*
        private void ForwardMove()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("TRY MOVE BOAT FROM NOT MASTER CLIENT");
                return;
            }
        }
*/
        public void OnEvent(EventData photonEvent) { }

        public static void SendLeftPaddleMoveEvent()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.Others};
            SendOptions sendOptions = new SendOptions {Reliability = true};
            PhotonNetwork.RaiseEvent(RemoteEventsNames.LeftPaddleMove, true, raiseEventOptions, sendOptions);
        }
    }
}
