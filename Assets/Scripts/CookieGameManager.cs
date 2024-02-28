using UnityEngine;
using TMPro;

public class CookieGameManager : MonoBehaviour
{
    public int cookiesToWin = 50; // Number of cookies needed to win
    public TextMeshProUGUI cookieCounterText; // Reference to text to display cookie counter
    public TextMeshProUGUI highScoreText; // Reference to text to display high score
    public TextMeshProUGUI timerText; // Reference to text to display timer

    private int cookiesEaten = 0; // Counter for cookies eaten
    private float startTime; // Time when the game starts
    private float currentTime; // Current time during the game
    private float bestTime = Mathf.Infinity; // High score
    private bool gameStarted = false; // Flag indicating whether the game has started
    private bool gameOver = false; // Flag indicating whether the game is over

    void Start()
    {
        UpdateHighScoreText();
    }

    void Update()
    {
        if (gameStarted && !gameOver)
        {
            currentTime = Time.time - startTime;
            UpdateTimerText(currentTime);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        startTime = Time.time;
    }

    public void EndGame()
    {
        gameOver = true;
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("HighScore", bestTime);
            UpdateHighScoreText();
        }
    }

    public void IncrementCookieCounter()
    {
        cookiesEaten++;
        UpdateCookieCounterText();
        if (cookiesEaten >= cookiesToWin)
        {
            EndGame();
        }
        else if (!gameStarted)
        {
            StartGame();
        }
    }

    void UpdateCookieCounterText()
    {
        if (cookieCounterText != null)
        {
            cookieCounterText.text = "Cookies Eaten: " + cookiesEaten;
        }
    }

    void UpdateHighScoreText()
    {
        bestTime = PlayerPrefs.GetFloat("HighScore", Mathf.Infinity);
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + bestTime.ToString("F2") + "s";
        }
    }

    void UpdateTimerText(float time)
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + time.ToString("F2") + "s";
        }
    }
}
