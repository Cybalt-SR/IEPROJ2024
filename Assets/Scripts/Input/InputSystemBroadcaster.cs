using Assets.Scripts.Library;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputSystemBroadcaster : MonoBehaviour, ISingleton<InputSystemBroadcaster>
    {
        private PlayerInput mPlayerInput;

        private enum ReceiverType { GENERIC, PLAYER, UI };

        [SerializeField] private List<int> autoEnabledMaps = new();
        [SerializeField] private SerializableDictionary<ReceiverType, List<int>> receiver_mapping = new(ReceiverType.GENERIC);

        private readonly Dictionary<ReceiverType, List<IInputReceiver>> inputReceivers = new();
        private readonly Dictionary<ReceiverType, System.Type> receiver_types = new()
        {
            {ReceiverType.PLAYER, typeof(IPlayerInputReceiver) },
            {ReceiverType.UI, typeof(IUiInputReceiver) },
        };

        private void Awake()
        {
            mPlayerInput = GetComponent<PlayerInput>();
            foreach (var mapid in autoEnabledMaps)
            {
                mPlayerInput.actions.actionMaps[mapid].Enable();
            }

            var allMonoBehaviours = FindObjectsByType<MonoBehaviour>(sortMode: FindObjectsSortMode.None);

            foreach (ReceiverType receiverType in System.Enum.GetValues(typeof(ReceiverType)))
            {
                if (receiver_mapping.ContainsKey(receiverType) == false)
                    continue;

                inputReceivers.Add(receiverType, new());

                foreach (var receiver_object in allMonoBehaviours)
                {
                    foreach (IInputReceiver receiver in receiver_object.GetComponents(receiver_types[receiverType]))
                    {
                        inputReceivers[receiverType].Add(receiver);
                    }
                }
            }

            foreach (ReceiverType receiverType in System.Enum.GetValues(typeof(ReceiverType)))
            {
                if (receiver_mapping.ContainsKey(receiverType) == false)
                    continue;

                foreach (var mapping in receiver_mapping[receiverType])
                {
                    foreach (var methods in receiver_types[receiverType].GetMethods())
                        foreach (var inputaction in mPlayerInput.actions.actionMaps[mapping].actions)
                        {
                            if (methods.Name != inputaction.name)
                                continue;

                            void onAction(InputAction.CallbackContext callback)
                            {
                                inputReceivers[receiverType].ForEach((receiver) =>
                                {
                                    methods.Invoke(receiver, new object[] { callback });
                                });
                            }

                            inputaction.started += onAction;
                            inputaction.performed += onAction;
                            inputaction.canceled += onAction;
                        }
                    foreach (var property in receiver_types[receiverType].GetProperties())
                        foreach (var inputaction in mPlayerInput.actions.actionMaps[mapping].actions)
                        {
                            if (property.PropertyType != typeof(bool))
                                continue;
                            if (property.Name != "Is" + inputaction.name)
                                continue;

                            void onAction(InputAction.CallbackContext callback)
                            {
                                inputReceivers[receiverType].ForEach((receiver) =>
                                {
                                    property.SetValue(receiver, true);
                                });
                            }
                            void offAction(InputAction.CallbackContext callback)
                            {
                                inputReceivers[receiverType].ForEach((receiver) =>
                                {
                                    property.SetValue(receiver, false);
                                });
                            }

                            inputaction.performed += onAction;
                            inputaction.canceled += offAction;
                        }
                }
            }
        }
    }
}