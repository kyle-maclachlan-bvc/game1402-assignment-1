using TMPro;
using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{

    [SerializeField] private int healAmount = 1;
    
    public void OnCollect(GameObject collector)
    {
        PlayerController player = collector.GetComponent<PlayerController>();
        if (player == null) return;
        
        player.Heal(healAmount);
        
        Destroy(gameObject);
    }
}
