using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public interface IUiInputReceiver : IInputReceiver
    {
        public void Toggle_Attachement(InputAction.CallbackContext callback);
        public void Toggle_Pause(InputAction.CallbackContext callback);
    }
}