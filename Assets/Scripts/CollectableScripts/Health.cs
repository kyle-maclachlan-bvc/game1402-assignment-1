using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        Debug.Log("Heart Collected");
        Destroy(gameObject);
    }
}
