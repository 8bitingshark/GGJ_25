using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Video;

namespace Player
{
    public class PlayerInputs : MonoBehaviour
    {
        private InputActions _playerInputActions;
        private InputActionMap _currentPlayerActionMap;
        
        public event EventHandler OnJumpAction;
        public event EventHandler OnKickAction;
        public event EventHandler OnSuckAction;
        
        private float _movementInput;
        
        //*********************************Setting Stuff*********************************
        private void Awake()
        {
            _playerInputActions = new InputActions();
        }
       
        private void OnEnable()
        {
            if (_currentPlayerActionMap == null) return;
            _currentPlayerActionMap.Enable();
            SubscribeToEvents(_currentPlayerActionMap);
        }

        private void OnDisable()
        {
            if (_currentPlayerActionMap == null) return;
            UnsubscribeFromEvents(_currentPlayerActionMap);
            _currentPlayerActionMap.Disable();
        }

        private void OnDestroy()
        {
            if (_currentPlayerActionMap != null)
            {
                UnsubscribeFromEvents(_currentPlayerActionMap);
            }
        }
        
        //*********************************Initialization*********************************

        public void InitializePlayerInputs(int playerNumber)
        {
            //Not so well implemented, I suppose, but to avoid to write stuff two times
            string actionMapName = playerNumber == 0 ? "Player1" : "Player2";
            _currentPlayerActionMap = _playerInputActions.asset.FindActionMap(actionMapName);

            if (_currentPlayerActionMap == null)
            {
                Debug.LogError($"Action map '{actionMapName}' not found!");
                return;
            }

            // Enable the action map, so Player1 or Player2
            _currentPlayerActionMap.Enable();
            // Subscribe to the input events
            SubscribeToEvents(_currentPlayerActionMap);
        }
        
        private void SubscribeToEvents(InputActionMap actionMap)
        {
            var moveAction = actionMap.FindAction("Move");
            if (moveAction != null)
            {
                moveAction.performed += OnMovePerformed;
            }

            var jumpAction = actionMap.FindAction("Jump");
            if (jumpAction != null)
            {
                jumpAction.performed += Jump_performed;
            }

            var kickAction = actionMap.FindAction("Kick");
            if (kickAction != null)
            {
                kickAction.performed += Kick_performed;
            }

            var suckAction = actionMap.FindAction("Suck");
            if (suckAction != null)
            {
                suckAction.performed += Suck_performed;
            }
        }

        private void UnsubscribeFromEvents(InputActionMap actionMap)
        {
            var moveAction = actionMap.FindAction("Move");
            if (moveAction != null)
            {
                moveAction.performed -= OnMovePerformed;
            }

            var jumpAction = actionMap.FindAction("Jump");
            if (jumpAction != null)
            {
                jumpAction.performed -= Jump_performed;
            }

            var kickAction = actionMap.FindAction("Kick");
            if (kickAction != null)
            {
                kickAction.performed -= Kick_performed;
            }

            var suckAction = actionMap.FindAction("Suck");
            if (suckAction != null)
            {
                suckAction.performed -= Suck_performed;
            }
        }
        
        //*********************************Input Events*********************************
        
        public float GetMovementInput()
        {
            return _movementInput;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<float>();
        }

        private void Jump_performed(InputAction.CallbackContext context)
        {
            OnJumpAction?.Invoke(this, EventArgs.Empty);
        }

        private void Kick_performed(InputAction.CallbackContext context)
        {
            OnKickAction?.Invoke(this, EventArgs.Empty);
        }

        private void Suck_performed(InputAction.CallbackContext context)
        {
            OnSuckAction?.Invoke(this, EventArgs.Empty);
        }
    }
}
