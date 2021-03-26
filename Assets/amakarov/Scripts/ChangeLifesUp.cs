using UnityEngine;

public class ChangeLifesUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.IncreaseLives();
    }
}
