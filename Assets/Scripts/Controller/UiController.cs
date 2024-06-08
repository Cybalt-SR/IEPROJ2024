using Assets.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controller
{
    public class UiController : MonoBehaviour, IUiInputReceiver
    {
        [SerializeField] private GameObject Attachment_screen;

        void IUiInputReceiver.Toggle_Attachement(InputAction.CallbackContext callback)
        {
            Attachment_screen.SetActive(!Attachment_screen.activeSelf);
        }
    }
}
