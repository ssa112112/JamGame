using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText = null;

    public void ShowScore()
    {
        var score = GameManager.overallScore;
        scoreText.text = "WELL DONE! + \n + LEVEL COMPLETE! + \n Your score is:" + score;
    }
    
}
