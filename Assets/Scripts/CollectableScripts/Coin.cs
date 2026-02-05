using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void OnCollect(GameObject collector)
    {
        AudioManager.Instance.PlayCoinPickup();
        GameManager.Instance.AddCoin();
        Destroy(gameObject);
    }
}
