using UnityEngine;

public class ChangeLifesDownCollider : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag($"Obstacle"))
            GameManager.Instance.DecreaseLives();
    }
}
