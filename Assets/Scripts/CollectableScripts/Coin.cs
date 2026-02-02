using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        GameManager.Instance.AddCoin();
        Destroy(gameObject);
    }
}
