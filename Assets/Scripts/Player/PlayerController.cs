using UnityEngine;
using System;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputs playerInputs;
        [SerializeField] private PlayerDataConfig playerDataConfig;
        private Rigidbody _rb;
        
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            playerInputs = GetComponent<PlayerInputs>();
            playerInputs.InitializePlayerInputs(0);
            playerInputs.OnJumpAction += JumpActionReceived;
            playerInputs.OnKickAction += KickActionReceived;
            playerInputs.OnSuckAction += SuckActionReceived;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void JumpActionReceived(object sender, EventArgs e)
        {
            
        }

        private void KickActionReceived(object sender, EventArgs e)
        {
            
        }

        private void SuckActionReceived(object sender, EventArgs e)
        {
            
        }
    }
}
