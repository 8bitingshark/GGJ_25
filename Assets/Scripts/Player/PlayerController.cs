using UnityEngine;
using System;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private PlayerDataConfig playerDataConfig; // it collects data parameters for the player
        [SerializeField] private int playerNumber; // then we can change with enum or other suitable stuff
        
        private Rigidbody2D _rb;
        [Header("Movement Params")]
        private Vector2 _inputHorizontalDirection;
        private float _movementControlMultiplier;
        private float _horizontalVelocity;
        private float _movementAcceleration;
        private float _movementMaxSpeedX;
        private float _movementMaxSpeedY;
        [Header("Jump params")]
        private float _jumpForce;
        private float _currentAirControlMultiplier;
        private float _fallGravityMultiplier;
        private bool _isGrounded;
        [SerializeField] private LayerMask groundLayer;
        [Header("Terrain check")]
        private float _groundCheckDistance;
        
        [Header("Kick params")]
        private bool _isAbleToKick;
        
        // Coyote Time: add some time tolerance to allow the player to jump immediately after he's left the terrain
        private float _coyoteTime;
        private float _currentCoyoteTime;
        
        
        // Events to update UI and Animation
        
        //*********************************Setting Stuff*********************************
        private void Start()
        {
            playerInputs = GetComponent<PlayerInputs>();
            _rb = GetComponent<Rigidbody2D>();
            
            if (playerInputs == null)
            {
                Debug.LogError("PlayerInput Component not found!");
            }
            playerInputs.InitializePlayerInputs(playerNumber);
            
            playerInputs.OnJumpAction += JumpActionReceived;
            playerInputs.OnKickAction += KickActionReceived;
            playerInputs.OnSuckAction += SuckActionReceived;
            
            InitializePlayerData();
        }

        private void InitializePlayerData()
        {
            // set the parameters
            _coyoteTime = playerDataConfig.coyoteTime;
            _currentCoyoteTime = _coyoteTime;
            _movementControlMultiplier = 1.0f;
            _currentAirControlMultiplier = playerDataConfig.airControlMultiplier;
            _movementMaxSpeedX = playerDataConfig.movementMaxSpeedX;
            _movementMaxSpeedY = playerDataConfig.movementMaxSpeedY;
            _movementAcceleration = playerDataConfig.movementAcceleration;
            _fallGravityMultiplier = playerDataConfig.fallGravityMultiplier;
            _jumpForce = playerDataConfig.jumpForce;
            _groundCheckDistance = playerDataConfig.groundCheckDistance;
        }
        

        //*********************************Updating*********************************
        
        private void Update()
        {
            CheckGrounded();
            UpdateCoyoteTime();
        }

        private void FixedUpdate()
        {
            Move();
            AddFallGravity();
            //ClampVelocity();
        }

        //*********************************Movement*********************************

        private void Move()
        {
            _inputHorizontalDirection = playerInputs.GetMovementInput();
            //apply forces and acceleration?
            
            _movementControlMultiplier = _isGrounded ? 1.0f : playerDataConfig.airControlMultiplier;
            _horizontalVelocity = _inputHorizontalDirection.x * _movementAcceleration * _movementControlMultiplier;
            _rb.linearVelocity = new Vector2(_horizontalVelocity, _rb.linearVelocity.y); //possibility to accelerate gradually?
        }

        private void ClampVelocity()
        {
            float clampedX = Mathf.Clamp(_rb.linearVelocity.x, -_movementMaxSpeedX, _movementMaxSpeedX);
            float clampedY = Mathf.Clamp(_rb.linearVelocity.y, -_movementMaxSpeedY, _movementMaxSpeedY);
            _rb.linearVelocity = new Vector2(clampedX, clampedY);
        }

        //*********************************Jump Management*********************************
        
        private void JumpActionReceived(object sender, EventArgs e)
        {
            Debug.Log("Jump action received");
            if (_currentCoyoteTime > 0.0f)
            {
                PerformJump();
            }
        }

        private void PerformJump()
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
        }

        private void AddFallGravity()
        {
            // apply fall gravity
            if (_rb.linearVelocity.y < 0.0f)
            {
                _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * _fallGravityMultiplier * Time.deltaTime;
            }
        }

        private void CheckGrounded()
        {
            //_isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, _groundCheckDistance, groundLayer);
            _isGrounded = hit.collider != null;
        }
        
        //for debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundCheckDistance);
        }

        private void UpdateCoyoteTime()
        {
            if (_isGrounded)
            {
                _currentCoyoteTime = _coyoteTime;
            }
            else
            {
                _currentCoyoteTime -= Time.deltaTime;
            }
        }
        
        // not sure for the interruption
        /*
        private IEnumerator CoyoteTimeCoroutine()
        {
            yield return new WaitForSeconds(_coyoteTime);
            
        } */
        
        //*********************************Actions*********************************
        
        private void KickActionReceived(object sender, EventArgs e)
        {
            if (_isAbleToKick)
            {
                PerformKick();
            }
        }

        private void PerformKick()
        {
            
        }

        private void SuckActionReceived(object sender, EventArgs e)
        {
            
        }
    }
}
