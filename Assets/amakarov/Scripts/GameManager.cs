using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using sskvortsov.Scripts.GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IOnEventCallback
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
    public static int overallScore;
    private static int _overallLife;
    public bool isGameOver;
    [SerializeField] private GameObject waitPanel = null;
    [SerializeField] private float boatSpeed = 0f;
    [SerializeField] private float minimumSpeed = 0f;
    [SerializeField] private float defaultSpeed = 0f;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private GameObject[] lifeImages = null;
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;
    public bool isInvincible;
    public int rowButtonHand;

    private void Awake()
    {
        isGameOver = false;
        _instance = this;
        _overallLife = 3;
        overallScore = 0;
        isInvincible = false;
        rowButtonHand = PhotonNetwork.IsMasterClient ? 1 : 0;
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.Players.Count > 1)
        {
            waitPanel.SetActive(false);
            isGameOver = false;
            if (PhotonNetwork.IsMasterClient)
            {
                buttonLeft.SetActive(false);
                buttonRight.SetActive(true);
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                buttonLeft.SetActive(true);
                buttonRight.SetActive(false);
            }
        }
    }

    public void ChangeScore(int amount)
    {
        overallScore += amount;
        scoreText.text = overallScore.ToString();
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
        Time.timeScale = 0;
        isGameOver = true;
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

    public void InvincibilityOn()
    {
        isInvincible = true;
    }

    public void InvincibilityOff()
    {
        isInvincible = false;
    }

    public void TryAgain()
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.Others};
        SendOptions sendOptions = new SendOptions {Reliability = true};
        PhotonNetwork.RaiseEvent(RemoteEventsNames.Restart, true, raiseEventOptions, sendOptions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == RemoteEventsNames.LeftPaddleMove)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
