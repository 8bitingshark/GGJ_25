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
        private Vector2 _lastMovementDirection;
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
        [SerializeField] private LayerMask jumpLayer;
        private bool _canJumpAddAcceleration;
        private bool _canChangeJumpForce;
        private float _currentJumpPressTime;
        private float _maxJumpHoldTime;
        private float _minJumpForce;
        private float _maxJumpForce;
        private float _currentJumpForce;
        [Header("Terrain check")]
        private float _groundCheckDistance;
        [Header("Wall check")]
        private bool _isTouchingWall;
        private float _wallCheckDistance;
        private float _wallCheckHeight;
        private float _boxCastOffsetCenterY;
        [SerializeField] private LayerMask groundLayer;
        
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
            playerInputs.OnJumpRelease += JumpActionReleased;
            playerInputs.OnKickAction += KickActionReceived;
            playerInputs.OnSuckAction += SuckActionReceived;
            
            InitializePlayerData();
        }

        private void InitializePlayerData()
        {
            // set the parameters
            _lastMovementDirection = Vector2.right;
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
            
            _canJumpAddAcceleration = false;
            _currentJumpPressTime = 0.0f;
            _currentJumpForce = _minJumpForce;
            _canChangeJumpForce = playerDataConfig.canChangeJumpForce;
            _maxJumpHoldTime = playerDataConfig.maxJumpHoldTime;
            _minJumpForce = playerDataConfig.minJumpForce;
            _maxJumpForce = playerDataConfig.maxJumpForce;
            
            _isTouchingWall = false;
            _wallCheckDistance = playerDataConfig.wallCheckDistance;
            _wallCheckHeight = playerDataConfig.wallCheckHeight;
            _boxCastOffsetCenterY = playerDataConfig.boxCastOffsetCenterY;
        }
        

        //*********************************Updating*********************************
        
        private void Update()
        {
            CheckGrounded();
            UpdateCoyoteTime();
            if(_canChangeJumpForce) UpdateJumpHold();
        }

        private void FixedUpdate()
        {
            CheckWallContact();
            Move();
            AddFallGravity();
            if(_canChangeJumpForce) ApplyJumpForce();
            ClampVelocity();
            //Debug.Log(_currentJumpForce);
        }

        //*********************************Movement*********************************

        private void Move()
        {
            _inputHorizontalDirection = playerInputs.GetMovementInputNormalized();

            if (_inputHorizontalDirection != Vector2.zero)
            {
                _lastMovementDirection = _inputHorizontalDirection;
            }

            // avoid being stuck against a wall
            if (!_isGrounded && _isTouchingWall)
            {
                _horizontalVelocity = 0.0f;
            }
            else
            {
                _movementControlMultiplier = _isGrounded ? 1.0f : playerDataConfig.airControlMultiplier;
                _horizontalVelocity = _inputHorizontalDirection.x * _movementAcceleration * _movementControlMultiplier;
            }

            _rb.linearVelocity = new Vector2(_horizontalVelocity, _rb.linearVelocity.y); //possibility to accelerate gradually?
        }

        private void ClampVelocity()
        {
            float clampedX = Mathf.Clamp(_rb.linearVelocity.x, -_movementMaxSpeedX, _movementMaxSpeedX);
            float clampedY = Mathf.Clamp(_rb.linearVelocity.y, -_movementMaxSpeedY, _movementMaxSpeedY);
            _rb.linearVelocity = new Vector2(clampedX, clampedY);
        }
        
        private void CheckWallContact()
        {
            Vector2 direction = new Vector2(_lastMovementDirection.x, 0);
            Vector2 boxSize = new Vector2(_wallCheckDistance, _wallCheckHeight);
            
            Vector2 boxCenter = (Vector2)transform.position + new Vector2(0, _boxCastOffsetCenterY);

            // Esegui il BoxCast
            RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0, direction, _wallCheckDistance, groundLayer);
            
            _isTouchingWall = hit.collider != null;
        }

        //*********************************Jump Management*********************************
        
        private void JumpActionReceived(object sender, EventArgs e)
        {
            if (_canChangeJumpForce)
            {
                if (_currentCoyoteTime > 0.0f && !_canJumpAddAcceleration)
                {
                    _canJumpAddAcceleration = true;
                    _currentJumpPressTime = 0.0f;
                }
            }
            else if (_currentCoyoteTime > 0.0f)
            {
                PerformJump(_jumpForce);
            }
        }
        
        private void JumpActionReleased(object sender, EventArgs e)
        {
            Debug.Log("JumpActionReleased");
            _canJumpAddAcceleration = false;
        }
        
        private void UpdateJumpHold()
        {
            if (_canJumpAddAcceleration && _currentJumpPressTime < _maxJumpHoldTime)
            {
                _currentJumpPressTime += Time.deltaTime;
                _currentJumpForce = Mathf.Lerp(_minJumpForce, _maxJumpForce, _currentJumpPressTime / _maxJumpHoldTime);
            }
            else
            {
                _currentJumpForce = 0; // No adding force
            }
        }
        
        private void ApplyJumpForce()
        {
            if (_currentJumpForce > 0)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _currentJumpForce);
                //_rb.linearVelocity += Vector2.up * (_currentJumpForce * Time.fixedDeltaTime);
            }
        }

        private void PerformJump(float jumpStrength)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpStrength);
        }

        private void AddFallGravity()
        {
            // apply fall gravity
            if (_rb.linearVelocity.y < 0.0f)
            {
                _rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * _fallGravityMultiplier * Time.deltaTime);
            }
        }

        private void CheckGrounded()
        {
            //_isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, _groundCheckDistance, jumpLayer);
            _isGrounded = hit.collider != null;
        }
        
        //for debug
        private void OnDrawGizmosSelected()
        {
            Vector2 direction = new Vector2(_lastMovementDirection.x, 0);
            Vector2 boxSize = new Vector2(_wallCheckDistance, _wallCheckDistance);
            Gizmos.color = _isTouchingWall ? Color.green : Color.red;
            Vector2 boxCenter = (Vector2)transform.position + new Vector2(0, _boxCastOffsetCenterY);
            Gizmos.DrawWireCube(boxCenter, boxSize);
            
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
