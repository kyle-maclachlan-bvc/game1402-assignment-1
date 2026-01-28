using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] public static CoinManager Instance;
    [SerializeField] private int coinCount = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log($"Coins: {coinCount}");
    }
}
