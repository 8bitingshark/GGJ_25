using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputs : MonoBehaviour
    {
        private InputActions _playerInputActions;
        private InputActionMap _currentPlayerActionMap;
        
        public event EventHandler OnJumpAction;
        public event EventHandler OnKickAction;
        public event EventHandler OnSuckAction;
        
        //*********************************Setting Stuff*********************************
        private void Awake()
        {
            _playerInputActions = new InputActions();
        }
       
        /*
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
        */

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

            Debug.Log($"Action map '{actionMapName}' initialized successfully.");

            // Enable the action map, so Player1 or Player2
            _currentPlayerActionMap.Enable();
            // Subscribe to the input events
            SubscribeToEvents(_currentPlayerActionMap);
        }
        
        private void SubscribeToEvents(InputActionMap actionMap)
        {
            var jumpAction = actionMap.FindAction("Jump");
            if (jumpAction != null)
            {
                jumpAction.performed += Jump_performed;
                Debug.Log("Jump action found!");
            }
            else
            {
                Debug.LogError("Jump action not found!");
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
        
        public Vector2 GetMovementInput()
        {
            return _currentPlayerActionMap.FindAction("Move").ReadValue<Vector2>();
        }

        private void Jump_performed(InputAction.CallbackContext context)
        {
            Debug.Log("OnJumpAction");
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
