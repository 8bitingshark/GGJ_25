using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class TestInput : MonoBehaviour
    {
        private InputActions _playerInputActions;
        public event EventHandler OnJumpAction;
        public event EventHandler OnKickAction;
        public event EventHandler OnSuckAction;
        
        private float _movementInput;
        
        //*********************************Setting Stuff*********************************
        private void Awake()
        {
            _playerInputActions = new InputActions();
            _playerInputActions.Player1.Enable();
            
            // input events
            _playerInputActions.Player1.Jump.performed += Jump_performed;
            _playerInputActions.Player1.Kick.performed += Kick_performed;
            _playerInputActions.Player1.Suck.performed += Suck_performed;
        }
        
        private void OnDestroy()
        {
            _playerInputActions.Player1.Jump.performed -= Jump_performed;
            _playerInputActions.Player1.Kick.performed -= Kick_performed;
            _playerInputActions.Player1.Suck.performed -= Suck_performed;
        }
        
        //*********************************Input Management*********************************
        
        public float GetMovementInput()
        {
            var inputDir = _playerInputActions.Player1.Move.ReadValue<float>();
            return inputDir;
        }

        private void Jump_performed(InputAction.CallbackContext obj)
        {
            OnJumpAction?.Invoke(this, EventArgs.Empty); 
        }
        
        private void Kick_performed(InputAction.CallbackContext obj)
        {
            OnKickAction?.Invoke(this, EventArgs.Empty); 
        }
        
        private void Suck_performed(InputAction.CallbackContext obj)
        {
            OnSuckAction?.Invoke(this, EventArgs.Empty); 
        }
    }
}
