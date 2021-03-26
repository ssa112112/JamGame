using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Game Manager is NULL!");
            return _instance;
        }
    }
    private static int _overallScore;
    private static int _overallLife;
    
    [SerializeField] private float boatSpeed = 0f;
    [SerializeField] private float minimumSpeed = 0f;
    [SerializeField] private float defaultSpeed = 0f;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject[] lifeImages = null;
    [SerializeField] private GameObject gameOverPanel = null;
    
    private void Awake()
    {
        _instance = this;
        _overallLife = 3;
        _overallScore = 0;
    }

    public void ChangeScore(int amount)
    {
        _overallScore += amount;
        scoreText.text = _overallScore.ToString();
    }

    public void IncreaseLives()
    {
        _overallLife = _overallLife >= 3 ? _overallLife = 3 : _overallLife += 1;
        UpdateLivesUI();
    }

    public void DecreaseLives()
    {
        _overallLife = _overallLife > 1 ? _overallLife -= 1 : _overallLife = 0;
        UpdateLivesUI();
    }
    private void UpdateLivesUI()
    {
        switch (_overallLife)
        {
            case 3:
            {
                lifeImages[0].SetActive(true);
                lifeImages[1].SetActive(true);
                lifeImages[2].SetActive(true);
                break;
            }
            case 2:
            {
                lifeImages[0].SetActive(true);
                lifeImages[1].SetActive(true);
                lifeImages[2].SetActive(false);
                break;
            }
            case 1:
            {
                lifeImages[0].SetActive(true);
                lifeImages[1].SetActive(false);
                lifeImages[2].SetActive(false);
                break;
            }
            case 0:
            {
                lifeImages[0].SetActive(false);
                lifeImages[1].SetActive(false);
                lifeImages[2].SetActive(false);           
                GameOver();
                break;
            }
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        // Если успею - подключить твайны и сделать анимацию
    }

    public void ChangeSpeed(float speedAmount)
    {
        boatSpeed += speedAmount;
        boatSpeed = boatSpeed < minimumSpeed ? minimumSpeed : boatSpeed;
    }

    public void SetDefaultSpeed()
    {
        boatSpeed = defaultSpeed;
    }

    public void TryAgain()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene($"MainMenuScene");
    }
}
