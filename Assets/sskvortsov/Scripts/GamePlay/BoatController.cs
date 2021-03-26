using Photon.Pun;
using UnityEngine;

namespace sskvortsov.Scripts.GamePlay
{
    public class BoatController : MonoBehaviour
    {
        public float speedRotate = 5;
        public float speedForward = 1;

        // Update is called once per frame
        void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("L");
                transform.Rotate(0, 0, Time.deltaTime * speedRotate);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("R");
                transform.Rotate(0, 0, Time.deltaTime * -speedRotate);
            }

            transform.Translate(0,speedForward,0);
        }
    }
}
