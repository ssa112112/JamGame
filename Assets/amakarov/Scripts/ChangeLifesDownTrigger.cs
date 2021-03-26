using UnityEngine;

public class ChangeLifesDownTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Instance.DecreaseLives();
            Destroy(gameObject);
        }
            
    }
}
