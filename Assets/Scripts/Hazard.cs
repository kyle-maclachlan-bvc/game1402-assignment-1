using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        if (player == null) return;
        
        player.TakeDamage(damage);
        
        player.ApplyKnockback(transform.position);
    }
}
