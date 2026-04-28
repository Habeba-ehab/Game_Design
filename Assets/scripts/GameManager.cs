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
    public GameObject obstacles; // NEW — drag your top Obstacle GameObject here

    [Header("Audio")]
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
        obstacles.SetActive(false); // NEW — make sure obstacles are hidden at start

        if (playerFootsteps != null)
            playerFootsteps.Stop();

        easyButton.onClick.AddListener(() => StartGame(false, false));   // no enemy, no obstacles
        mediumButton.onClick.AddListener(() => StartGame(true, false));  // enemy, no obstacles
        hardButton.onClick.AddListener(() => StartGame(true, true));     // enemy + obstacles

        playAgainButton.onClick.AddListener(RestartGame);
        restartButton.onClick.AddListener(RestartGame);
    }

    void StartGame(bool withEnemy, bool withObstacles) // UPDATED
    {
        Time.timeScale = 1f;

        startPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        enemy.SetActive(withEnemy);
        obstacles.SetActive(withObstacles); // NEW
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
        scoreText.text = "Crystals: " + collected + " / " + total;
    }

    public void ShowWin()
    {
        StopAllGameSounds();
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowGameOver()
    {
        StopAllGameSounds();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void StopAllGameSounds()
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