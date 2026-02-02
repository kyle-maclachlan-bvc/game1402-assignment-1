//using UnityEditor.Tilemaps;

using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // [serialized] is an attribute that makes the private variables editable in the inspector windows 
    [Header("Player Movement")] // All these attributes are dedicated to the movement of the player:
    [SerializeField] private float moveSpeed = 5f;              // The player's horizontal movement speed
    [SerializeField] private float jumpForce = 7.5f;            // Fixed Jump Height, WORK TO MAKE IT VARIABLE JUMP
    [SerializeField] private float acceleration = 30f;          // 
    [SerializeField] private float deceleration = 30f;          // 
    [SerializeField] private InputManager inputManager;         // Name is misspelled to ManaNger rather than Manager;
    [SerializeField] private float _jumpMultiplier = 0.7f;      
    [SerializeField] private bool interactPressed;              // allows the player to enter doors / entry ways
    
    // All these attributes are hidden from Editor to help identify jumping and grounding states.
    private float _horizontalInput = 0;
    private Rigidbody2D _playerRB;
    private bool _isGrounded;
    private bool _isJumping;

    [Header("Ground Check")]    // These attributes help with identifying if the player is grounded to allow jumping
    [SerializeField] private LayerMask groundLayer;         // Identify if the object below the player is on the Layer labelled Ground. Don't forget to set Ground Layer
    [SerializeField] private Vector2 startPointOffset;      // offset is how far from the player center our point is located
    [SerializeField] private float groundCheckDistance;     // sets how long of a distance check is being made below the player.
    [SerializeField] private float checkDelay;              
    //[SerializeField] private float coyoteTime = 0.1f;
    //[SerializeField] private float delayTime;
    
    [Header("Death Check")]     // These attributes check if the player hit a Death Barrier below the stage. Pitfalls cause deaths.
    [SerializeField] private LayerMask deathLayer;      // Identify if the object below the player is on the Layer labelled Death.
    [SerializeField] private float deathCheckDistance;  // Sets how long of a distance check is being made below the player.
    [SerializeField] private Vector2 deathRayOffset;    // Offset is how far from the player center our point is located. groundCheckDistance and startPointOffset COULD be used.
    
    [Header("Health")]      // These attributes check how much health the player had during the gameplay.
    [SerializeField] private int maxHealth = 8;

    // invicibility frames
    private float damageCooldown = 0.5f;
    private float lastDamageTime = -999f;
    
    [Header("Knockback")]
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float knockbackUpForce = 4f;

    private bool isDead = false;

    private int currentHealth;
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();        // Ensures Rigidbody is on the player object.
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
    
    void OnEnable()
    {
        inputManager.OnJump += HandleJumpInput;
        inputManager.OnMove += HandleMoveInput;
        inputManager.JumpRelease += HandleJumpCancel;
        inputManager.OnInteract += HandleInteract;
        //inputManager.OnRunning += HandleRunInput;
    }

    void OnDisable()
    {
        inputManager.OnJump -= HandleJumpInput;
        inputManager.OnMove -= HandleMoveInput;
        inputManager.JumpRelease -= HandleJumpCancel;
        inputManager.OnInteract -= HandleInteract;
        //inputManager.OnRunning -= HandleRunInput;
    }

    void HandleJumpInput()
    {
        // Apply the Jump Force
        if (_playerRB == null) return;

        _isJumping = true;
        if (_isGrounded)
            _playerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    void HandleMoveInput(float value)
    {
        _horizontalInput = value;
    }

    /*void HandleRunInput(float value)
    {
        _horizontalInput = value * 2f;
    }*/
    
    private void FixedUpdate()
    {
        HandleMovement();
        GroundCheck();
        DeathCheck();
        
    }

    void HandleMovement()
    {
        if (_playerRB == null) return;      // if playerRB is nonexistent, then stop / ignore
        
        //_playerRB.linearVelocityX = moveSpeed * _horizontalInput;
        float targetSpeed = moveSpeed * _horizontalInput;
        float newAcceleration = Mathf.Abs(_horizontalInput) <= 0.01f ? deceleration : acceleration;
        float newSpeed = Mathf.MoveTowards(_playerRB.linearVelocityX, targetSpeed, newAcceleration * Time.fixedDeltaTime);
        _playerRB.linearVelocityX = newSpeed;
    }

    void GroundCheck()
    {
        _isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance, groundLayer);
    }

    void OnDrawGizmos()
    {
        //Ground Check
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.down * groundCheckDistance, _isGrounded ? Color.forestGreen : Color.red);
        
        //Death Check
        Debug.DrawLine((Vector2)transform.position + deathRayOffset, (Vector2)transform.position + deathRayOffset + Vector2.down * deathCheckDistance, Color.magenta);
    }

    void HandleJumpCancel()
    {
        if (_isJumping && _playerRB.linearVelocity.y > 0)
        {
            //Debug.Log("Handling Jump Canceled Code.");
            _playerRB.linearVelocity = new Vector2(
                _playerRB.linearVelocity.x,
                _playerRB.linearVelocity.y * _jumpMultiplier
            );
            _isJumping = false;
        }
    }

    void HandleInteract()
    {
        interactPressed = true;
    }

    public bool ConsumeInteract()
    {
        if (!interactPressed) return false;
        interactPressed = false;
        return true;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        if (Time.time < lastDamageTime + damageCooldown) return;
        
        lastDamageTime = Time.time;

        currentHealth -= damage;
        
        HUDManager.Instance.UpdateHealth(currentHealth,  maxHealth);
        
        Debug.Log($"Player HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
            Die();
    }

    public void ApplyKnockback(Vector2 sourcePosition)
    {
        if (_playerRB == null) return;
        
        Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;
        Vector2 force = new Vector2(direction.x * knockbackForce, knockbackUpForce);
        
        _playerRB.linearVelocity = Vector2.zero;
        _playerRB.AddForce(force, ForceMode2D.Impulse);
    }
    
    void DeathCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            (Vector2)transform.position + deathRayOffset,
            Vector2.down,
            deathCheckDistance,
            deathLayer
        );

        if (hit.collider != null)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        
        GameManager.Instance.LoseLife();
        
        currentHealth = maxHealth;      // reset player Health.
        
        HUDManager.Instance.UpdateHealth(currentHealth,  maxHealth);
        
        RespawnManager.Instance.RespawnPlayer(gameObject);

        isDead = false;
    }

    public void HidePlayer()
    {
        // Disable rendering
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
            sprite.enabled = false;
        
        // Disable collisions
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        
        // Stop Movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }
        
        // Disable player logic
        enabled = false;
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        
        Debug.Log($"Player HP: {currentHealth}/{maxHealth}");
        
        HUDManager.Instance.UpdateHealth(currentHealth,  maxHealth);
    }
    
}
