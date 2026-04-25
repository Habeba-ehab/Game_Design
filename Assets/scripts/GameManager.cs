using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Start Panel")]
    public GameObject startPanel;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    [Header("HUD")]
    public TextMeshProUGUI scoreText;

    [Header("Win Panel")]
    public GameObject winPanel;
    public Button playAgainButton;

    [Header("Game Over Panel")]
    public GameObject gameOverPanel;
    public Button restartButton;

    [Header("Scene Objects")]
    public GameObject enemy;

    [Header("Audio")] // NEW
    public AudioSource backgroundMusic;
    public AudioSource playerFootsteps;

    private int collected = 0;
    private int total = 0;
    private bool gameStarted = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        scoreText.gameObject.SetActive(false);
        enemy.SetActive(false);

        // Make sure footsteps don't play before game starts --- NEW
        if (playerFootsteps != null)
            playerFootsteps.Stop();

        easyButton.onClick.AddListener(() => StartGame(false));
        mediumButton.onClick.AddListener(() => StartGame(true));
        hardButton.onClick.AddListener(() => StartGame(true));

        playAgainButton.onClick.AddListener(RestartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void StartGame(bool withEnemy)
    {
        Time.timeScale = 1f;

        startPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        enemy.SetActive(withEnemy);
        gameStarted = true;
        UpdateScore();
    }

    public void SetTotalCrystals(int t)
    {
        total = t;
        UpdateScore();
    }

    public void CrystalCollected()
    {
        if (!gameStarted) return;
        collected++;
        UpdateScore();

        if (collected >= total)
            ShowWin();
    }

    void UpdateScore()
    {
        scoreText.text = "Ceystals: " + collected + " / " + total;
    }

    public void ShowWin()
    {
        StopAllGameSounds(); // NEW
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowGameOver()
    {
        StopAllGameSounds(); // NEW
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void StopAllGameSounds() // NEW
    {
        if (backgroundMusic != null) backgroundMusic.Stop();
        if (playerFootsteps != null) playerFootsteps.Stop();
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}