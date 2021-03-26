using UnityEngine;

public class ChangeLifesDown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.DecreaseLives();
    }
}
