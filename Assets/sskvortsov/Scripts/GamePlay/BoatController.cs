    using System;
    using Cysharp.Threading.Tasks;
    using ExitGames.Client.Photon;
    using Photon.Pun;
    using Photon.Realtime;
    using sskvortsov.Scripts.GamePlay;
    using UnityEditor.Build;
    using UnityEngine;

    public class BoatController : MonoBehaviour, IOnEventCallback
    {
        [SerializeField]
        public float rebound;

        [SerializeField]
        public int TimeBeforeAddImpulse;

        [SerializeField]
        public Vector3 RotateAddForce;

        [SerializeField]
        private Vector3 VectorRotate;

        [SerializeField]
        [Min(0.01f)]
        private float maxVelocity;

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
            }
        }

        private void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                while (_rigidbody.velocity.sqrMagnitude > maxVelocity)
                {
                    Debug.Log("MAX VELOCITY");
                    _rigidbody.velocity *= 0.99f;
                }

            }

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
            Instance._rigidbody.AddRelativeForce(Instance.RotateAddForce, ForceMode.Impulse);
            //Instance._rigidbody.AddRelativeForce(-Instance.RotateAddForce, ForceMode.Impulse);
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

        public void OnEvent(EventData photonEvent) { }

        public static void SendLeftPaddleMoveEvent()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.Others};
            SendOptions sendOptions = new SendOptions {Reliability = true};
            PhotonNetwork.RaiseEvent(RemoteEventsNames.LeftPaddleMove, true, raiseEventOptions, sendOptions);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == 8)
            {
                Vector3 dir = other.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                // And finally we add force in the direction of dir and multiply it by force.
                // This will push back the player
                _rigidbody.AddForce(dir*rebound, ForceMode.Impulse);
            }
        }
    }