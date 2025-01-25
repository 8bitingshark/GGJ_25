using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerDataConfig", menuName = "Scriptable Objects/PlayerDataConfig")]
    public class PlayerDataConfig : ScriptableObject
    {
        // movement params
        public float movementAcceleration;
        public float movementMaxSpeed;
        // jump params
        public float jumpForce;
        public float coyoteTime;
        public float airControlMultiplier;
        public float fallGravityMultiplier;
        public float lowJumpMultiplier;
    }
}
