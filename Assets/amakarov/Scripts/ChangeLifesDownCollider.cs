using UnityEngine;

public class ChangeLifesDownCollider : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Obstacle")) return;
        if (GameManager.Instance.isInvincible) return;
        // Добавить отскок
        GameManager.Instance.DecreaseLives();
    }
}
