using UnityEngine;

public class Fruit : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        Debug.Log("Fruit Collected");
        Destroy(gameObject);
    }
}
