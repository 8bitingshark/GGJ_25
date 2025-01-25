using UnityEngine;
using System;
using System.Collections;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private PlayerDataConfig playerDataConfig; // it collects data parameters for the player
        [SerializeField] private int playerNumber; // then we can change with enum or other suitable stuff
        
        private Rigidbody _rb;
        //movement params
        private float _inputHorizontalDirection;
        private float _movementControlMultiplier;
        private float _horizontalVelocity;
        private float _movementAcceleration;
        private float _movementMaxSpeed;
        //jump params
        private float _jumpForce;
        private float _currentAirControlMultiplier;
        private float _fallGravityMultiplier;
        private bool _isGrounded;
        
        private bool _isAbleToKick;
        
        // Coyote Time: add some time tolerance to allow the player to jump immediately after he's left the terrain
        private float _coyoteTime;
        private float _currentCoyoteTime;
        
        
        // Events to update UI and Animation
        
        //*********************************Setting Stuff*********************************
        private void Start()
        {
            playerInputs = GetComponent<PlayerInputs>();
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
            _movementMaxSpeed = playerDataConfig.movementMaxSpeed;
            _movementAcceleration = playerDataConfig.movementAcceleration;
            _fallGravityMultiplier = playerDataConfig.fallGravityMultiplier;
            _jumpForce = playerDataConfig.jumpForce;
        }

        //*********************************Updating*********************************
        
        private void Update()
        {
            UpdateCoyoteTime();
        }

        private void FixedUpdate()
        {
            Move();
        }

        //*********************************Movement*********************************

        private void Move()
        {
            _inputHorizontalDirection = playerInputs.GetMovementInput();
            //apply forces and acceleration?
            
            _movementControlMultiplier = _isGrounded ? 1.0f : playerDataConfig.airControlMultiplier;
            _horizontalVelocity = _inputHorizontalDirection * _movementAcceleration * _movementControlMultiplier;
        }

        //*********************************Jump Management*********************************
        
        private void JumpActionReceived(object sender, EventArgs e)
        {
            if (_currentCoyoteTime > 0.0f)
            {
                PerformJump();
            }
        }

        private void PerformJump()
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpForce, _rb.linearVelocity.z);
            
            // apply fall gravity
            if (_rb.linearVelocity.y < 0.0f)
            {
                _rb.linearVelocity += Vector3.up * Physics2D.gravity.y * _fallGravityMultiplier * Time.deltaTime;
            }
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
        private IEnumerator CoyoteTimeCoroutine()
        {
            yield return new WaitForSeconds(_coyoteTime);
            
        }
        
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
