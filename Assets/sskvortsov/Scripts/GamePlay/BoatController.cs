using System;
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

        private Vector3 targetPos;
        private Vector3 _newTargetPos;
        
        private void Awake()
        {
            Instance = this;
            if (PhotonNetwork.IsMasterClient)
            {
                _rigidbody = GetComponent<Rigidbody>();
                // _rigidbody.AddRelativeForce(VectorForward, ForceMode.Force);
            }
            
        }

        private void Start()
        {
            targetPos = transform.position;
            _newTargetPos = transform.position;
        }

        private void Update()
        {
            // Debug.Log($"velocity: {_rigidbody.velocity}");
            // Debug.Log($"angularVelocity: {_rigidbody.angularVelocity}");

            transform.Translate(transform.forward * Time.deltaTime * 10);
            
            Debug.Log(transform.forward);
            
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

            Instance.targetPos.y -= 30;
            
            
            
            
            // Instance._rigidbody.AddRelativeTorque(new Vector3(0, -50, 0));
            
            // Instance.transform.Rotate (new Vector3 (0f, -1f, 0f));
            // steerFactor = Mathf.Lerp(steerFactor, horizontalInput, Time.deltaTime * turnThreshold);
            // AddForceAfterWait().Forget();
        }

        private static async UniTask AddForceAfterWait()
        {
            await UniTask.Delay(Instance.TimeBeforeAddImpulse);
            // Debug.Log("AddForceAfterWait");
            Instance._rigidbody.AddRelativeForce(Instance.RotateAddForce, ForceMode.Impulse);
            await UniTask.Delay(Instance.ImpulseLiveTime);
            // Debug.Log("AddForceAfterWaitReturn");
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
            
            Instance.targetPos.y += 30;
            
            // Instance._rigidbody.AddRelativeTorque(new Vector3(0, 50, 0));

            // Instance._rigidbody.AddRelativeTorque(new Vector3(0, -50, 0));
            // Instance.transform.Rotate (new Vector3 (0f, 1f, 0f)); 
            // AddForceAfterWait().Forget();
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
