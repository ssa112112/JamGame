using UnityEngine;

public class VodkaBonus : MonoBehaviour
{
    [SerializeField] private RowButtonChaos rowButtonChaos = null; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rowButtonChaos.GenerateChaosNumber();
            Destroy(gameObject);
        }
    }
}
