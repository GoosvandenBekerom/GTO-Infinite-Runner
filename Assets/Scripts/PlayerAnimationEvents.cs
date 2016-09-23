using UnityEngine;
namespace Assets.Scripts
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public PlayerMovement PlayerScript;

        public void StopRolling()
        {
            PlayerScript.StopRolling();
        }
    }
}
