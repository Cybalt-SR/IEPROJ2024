using Assets.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Controller
{
    public class UiController : MonoBehaviour, IUiInputReceiver
    {
        [SerializeField] private string owner;


        //[SerializeField] private GameObject Attachment_screen;
        [SerializeField] private GameObject GearConfig_screen;

        [SerializeField] private GameObject Pause_screen;

        private void Awake()
        {
            void PropagateOwnership(Transform parent)
            {
                foreach (Transform child in parent)
                {
                    PropagateOwnership(child);

                    foreach (var comp in child.gameObject.GetComponents<IPlayerSpecificUi>())
                        comp.PlayerAssigned = owner;
                }
            }

            PropagateOwnership(this.transform);
        }

        void IUiInputReceiver.Toggle_Attachement(InputAction.CallbackContext callback)
        {
            if (callback.performed)
                GearConfig_screen.SetActive(!GearConfig_screen.activeSelf);
                //Attachment_screen.SetActive(!Attachment_screen.activeSelf);
        }

        private void Start()
        {
            GearConfig_screen.SetActive(false);
            //Attachment_screen.SetActive(false);
        }

        public void Toggle_Pause(InputAction.CallbackContext callback)
        {
            if (callback.performed)
                Pause_screen.SetActive(!Pause_screen.activeSelf);
        }
    }
}
