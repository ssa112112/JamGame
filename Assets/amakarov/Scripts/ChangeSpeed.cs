using System.Collections;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    [SerializeField] private float speedAmount = 0f;
    [SerializeField] private int speedBonusTime = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Instance.ChangeSpeed(speedAmount);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(WaitAndEndSpeedBonus());
        }
    }

    IEnumerator WaitAndEndSpeedBonus()
    {
        yield return new WaitForSecondsRealtime(speedBonusTime);
        GameManager.Instance.SetDefaultSpeed();
    }
}
