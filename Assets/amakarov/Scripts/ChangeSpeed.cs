using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    [SerializeField] private float speedAmount;
    
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ChangeSpeed(speedAmount);
    }
}
