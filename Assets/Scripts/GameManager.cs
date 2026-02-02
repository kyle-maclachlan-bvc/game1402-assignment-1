using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Collectables")] [SerializeField]
    private int winFruitCount = 30; // sets a cap fruit amount to collect to clear the level

    private int currentFruitCount = 0; // sets the current fruit count for the level to 0
    private int currentCoinCount = 0; // sets the current fruit count for the level to 0

    [Header("Lives")]
    [SerializeField] private int maxLives = 6;
    private int currentLives;

    [Header("Level Timer")] 
    [SerializeField] private float levelTime;
    [SerializeField] private bool timerRunning = true;
    
    [Header("Pause")]
    [SerializeField] private bool isPaused = false;
    
    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        Debug.Log("Timer Running: " + timerRunning);
        
        currentLives = maxLives;
        Debug.Log($"Lives: {currentLives}");
    }

    public void Update()
    {
        if (!timerRunning) return;

        levelTime += Time.deltaTime;
        HUDManager.Instance.UpdateTimer(levelTime);
    }

    public int GetLives()
    {
        return currentLives;
    }
    public void LoseLife()
    {
        currentLives--;
        HUDManager.Instance.UpdateLives(currentLives);
        
        Debug.Log($"Player Died. Lives left: {currentLives}");

        if (currentLives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        timerRunning = false;
        Debug.Log("GAME OVER");
        
        // disable player movement, stop game, etc.
        player.enabled = false;
        player.HidePlayer();
        ShowTutorialMessage("----- GAME OVER -----\n \nGood try, you can always try again.");
       Time.timeScale = 0;     // pauses game.
    }

    public void AddFruit(string fruitType)
    {
        currentFruitCount++;
        HUDManager.Instance.UpdateFruit(currentFruitCount, winFruitCount);
        Debug.Log($"FruitCollected: {fruitType} ({currentFruitCount} / {winFruitCount})");

        if (currentFruitCount >= winFruitCount)
            WinGame();
    }

    public void AddCoin(int amount = 1)
    {
        currentCoinCount += amount;
        HUDManager.Instance.UpdateCoin(currentCoinCount);
        Debug.LogFormat($"Coin Collected: {currentCoinCount}");
    }

    void WinGame()
    {
        Debug.Log("You Win! 30 fruit collected! The next level is not built");
        
        //freeze the player for now.
        PlayerController player = FindObjectOfType<PlayerController>();
        timerRunning = false;
        
        if (player != null)
            player.enabled = false;
        
        ShowTutorialMessage("You collected 30 fruit!\nYou Win!\n \nUnfortunately, the second world is still under development,\nHope you enjoyed this level!");
        
        // play sound
        // load next scene
    }

    public void ShowTutorialMessage(string message)
    {
        HUDManager.Instance.ShowTutorialMessage(message);
    }

    public void HideTutorialMessage()
    {
        HUDManager.Instance.HideTutorialMessage();
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
        
            PauseGame();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        timerRunning = false;
        
        ShowTutorialMessage("----- GAME PAUSED -----\n \nPress ESC, or the Menu Buttons to Resume.");
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        timerRunning = true;

        HideTutorialMessage();
    }
    
}
