using Assets.Scripts.Library;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputController : MonoSingleton<InputController>
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

        private readonly Stack<InputBlocker> blockers = new();
        private readonly List<InputBlocker> blockersToPop = new();

        public static void BlockerEnabled(InputBlocker blocker)
        {
            Instance.blockers.Push(blocker);
        }

        public static void BlockerDisabled(InputBlocker blocker)
        {
            var stack = Instance.blockers;
            var list = Instance.blockersToPop;

            if (stack.TryPeek(out var top) == false)
                return;

            if (top != blocker)
            {
                list.Add(blocker);
                return;
            }

            stack.Pop();

            while (stack.TryPeek(out top))
            {
                if (list.Contains(top))
                {
                    list.Remove(top);
                    stack.Pop();
                }
                else
                {
                    break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

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
                                var blocked = blockers.TryPeek(out var top);

                                inputReceivers[receiverType].ForEach((receiver) =>
                                {
                                    if (blocked && top.inputReceiver != (receiver as MonoBehaviour))
                                        return;

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
                                var blocked = blockers.TryPeek(out var top);

                                inputReceivers[receiverType].ForEach((receiver) =>
                                {
                                    if (blocked && top.inputReceiver != (receiver as MonoBehaviour))
                                        return;

                                    property.SetValue(receiver, true);
                                });
                            }
                            void offAction(InputAction.CallbackContext callback)
                            {
                                var blocked = blockers.TryPeek(out var top);

                                inputReceivers[receiverType].ForEach((receiver) =>
                                {
                                    if (blocked && top.inputReceiver != (receiver as MonoBehaviour))
                                        return;
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