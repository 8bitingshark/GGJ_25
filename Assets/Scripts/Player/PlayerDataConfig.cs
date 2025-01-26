using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerDataConfig", menuName = "Scriptable Objects/PlayerDataConfig")]
    public class PlayerDataConfig : ScriptableObject
    {
        // movement params
        public float movementAcceleration;
        public float movementMaxSpeedX;
        public float movementMaxSpeedY;
        // jump params
        public float jumpForce;
        public float coyoteTime;
        public float airControlMultiplier;
        public float fallGravityMultiplier;
        public float groundCheckDistance;
        public float maxJumpHoldTime;
        public float minJumpForce;
        public float maxJumpForce;
        public bool canChangeJumpForce;
        //against wall params
        public float wallCheckDistance;
        public float wallCheckHeight;
        public float boxCastOffsetCenterY;
    }
}
