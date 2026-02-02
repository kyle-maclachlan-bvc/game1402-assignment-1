using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private bool playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void Update()
    {
        if (!playerInRange || player == null) return;
    
        if (player.ConsumeInteract())
        {
            TeleportPlayer();
        }
    }

    void TeleportPlayer()
    {
        player.transform.position = exitPoint.position;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
    
    
}
