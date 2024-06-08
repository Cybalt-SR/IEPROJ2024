using Assets.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebugger : MonoBehaviour, IUiInputReceiver, IPlayerInputReceiver
{
    public void Aim(InputAction.CallbackContext callback)
    {
    }

    public void Fire(InputAction.CallbackContext callback)
    {
    }

    public void Look(InputAction.CallbackContext callback)
    {
    }

    public void Move(InputAction.CallbackContext callback)
    {
    }

    public void Toggle_Attachement(InputAction.CallbackContext callback)
    {
        Debug.Log("Toggle_Attachement");
    }
}
