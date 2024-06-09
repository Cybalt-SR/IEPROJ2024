using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public interface IPlayerInputReceiver : IInputReceiver
    {
        public bool IsFire { get; set; }
        public void Move(InputAction.CallbackContext callback);
        public void Aim(InputAction.CallbackContext callback);
        public void Look(InputAction.CallbackContext callback);
        public void Fire(InputAction.CallbackContext callback);
    }
}