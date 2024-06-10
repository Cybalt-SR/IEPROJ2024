using Assets.Scripts.Input;
using Assets.Scripts.Library;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class InputLabel : MonoBehaviour
    {
        private TextMeshProUGUI mTextMeshProUGUI;

        [SerializeField] private string action_name;
        [SerializeField] private InputActionReference mInputActionReference;

        private void Awake()
        {
            mTextMeshProUGUI= GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {

            mTextMeshProUGUI.text = action_name + " (" + mInputActionReference.action.GetBindingDisplayString() + ")";
        }
    }
}