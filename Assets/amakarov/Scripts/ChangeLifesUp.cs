using UnityEngine;

public class ChangeLifesUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseLives();
            Destroy(gameObject);
        }
    }
}
