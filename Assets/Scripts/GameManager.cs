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
        currentLives = maxLives;
        Debug.Log($"Lives: {currentLives}");
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
        Debug.Log("GAME OVER");
        // UI will go here later
        // disable player movement, stop game, etc.
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
        if (player != null)
            player.enabled = false;
        
        // show victory UI
        // play sound
        // load next scene
    }
}
