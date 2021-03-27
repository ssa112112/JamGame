using UnityEngine;

public class ChangeScoreNumber : MonoBehaviour
{
    [SerializeField] private int amount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag($"Bonus"))
        {
            GameManager.Instance.ChangeScore(amount);
            Destroy(gameObject);
        }
    }
}
