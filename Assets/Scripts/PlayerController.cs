//using UnityEditor.Tilemaps;

using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // [serialized] is an attribute that makes the private variables editable in the inspector windows 
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7.5f;          // Fixed Jump Height, WORK TO MAKE IT VARIABLE JUMP
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float deceleration = 30f;
    [SerializeField] private InputMananger inputManager;    // Name is misspelled to ManaNger rather than Manager;
    [SerializeField] private float _jumpMultiplier = 0.7f;
    
    private float _horizontalInput = 0;
    private Rigidbody2D _playerRB;
    private bool _isGrounded;
    private bool _isJumping;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startPointOffset;  //offset is how far from the player center our point is located
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float checkDelay;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float delayTime;

    
    [Header("Death Check")]
    [SerializeField] private LayerMask deathLayer;
    [SerializeField] private float deathCheckDistance;
    [SerializeField] private Vector2 deathRayOffset;
    
    
    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable()
    {
        inputManager.OnJump += HandleJumpInput;
        inputManager.OnMove += HandleMoveInput;
        inputManager.JumpRelease += HandleJumpCancel;
        //inputManager.OnRunning += HandleRunInput;
    }

    void OnDisable()
    {
        inputManager.OnJump -= HandleJumpInput;
        inputManager.OnMove -= HandleMoveInput;
        inputManager.JumpRelease -= HandleJumpCancel;
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

    private void Update()
    {
        
    }
    
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
        Debug.Log("Player Died");
        Destroy(gameObject);
    }
    
    
}
