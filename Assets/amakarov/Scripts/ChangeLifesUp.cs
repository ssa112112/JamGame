using Photon.Pun;
using sskvortsov.Scripts.GamePlay;
using UnityEngine;

public class ChangeLifesUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.transform.CompareTag("Player"))
            {
                GameManager.Instance.IncreaseLives();
                Destroy(gameObject);
                NetworkEventManager.SendChangeLiveEvent(1);
            }
        }
    }
}
