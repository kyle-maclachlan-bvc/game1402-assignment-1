using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    
    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI fruitText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Tutorial UI")]
    [SerializeField] private TextMeshProUGUI tutorialText;
    
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    // Update Methods
    public void UpdateFruit(int current, int win)
    {
        fruitText.text = current +  " / " + win;
    }

    public void UpdateCoin(int value)
    {
        coinText.text = value.ToString();
    }

    public void UpdateLives(int value)
    {
        livesText.text = value.ToString();
    }

    public void UpdateHealth(int current, int max)
    {
        healthText.text = current + " / " + max;
    }
    
    // Tutorial Methods
    public void ShowTutorialMessage(string message)
    {
        tutorialText.text = message;
        tutorialText.gameObject.SetActive(true);
    }

    public void HideTutorialMessage()
    {
        tutorialText.gameObject.SetActive(false);
    }
    
    // Timer Methods
    public void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}
