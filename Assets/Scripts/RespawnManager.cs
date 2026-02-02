using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [SerializeField] private Vector2 respawnPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Find player start position
        PlayerController player = FindObjectOfType<PlayerController>();
        respawnPosition = player.transform.position;
    }

    public void SetRespawnPosition(Vector2 newPosition)
    {
        respawnPosition = newPosition;
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = respawnPosition;
        
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
