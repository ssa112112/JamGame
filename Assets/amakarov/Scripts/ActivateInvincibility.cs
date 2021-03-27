using System.Collections;
using UnityEngine;

public class ActivateInvincibility : MonoBehaviour
{
    [SerializeField] private int invincibilityTime = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.InvincibilityOn();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(WaitInvincibilityTime());
            
        }
    }

    IEnumerator WaitInvincibilityTime()
    {
        yield return new WaitForSecondsRealtime(invincibilityTime);
        GameManager.Instance.InvincibilityOff();
    }
}
