using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void OnCollect(GameObject collector)
    {
        GameManager.Instance.AddCoin();
        Destroy(gameObject);
    }
}
