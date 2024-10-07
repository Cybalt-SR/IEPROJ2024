using System;
using System.Collections.Generic;

namespace gab_roadcasting
{
    public class EventBroadcasting
    {
        private static EventBroadcasting instance;
        private static EventBroadcasting Instance
        {
            get
            {
                if (instance == null)
                    instance = new EventBroadcasting();

                return instance;
            }
        }

        private Dictionary<string, List<Action<Dictionary<string, object>>>> registeredListeners = new();

        public static void AddListener(string id, Action<Dictionary<string, object>> action)
        {
            if (Instance.registeredListeners.ContainsKey(id) == false)
                Instance.registeredListeners.Add(id, new());

            Instance.registeredListeners[id].Add(action);
        }
        public static void InvokeEvent(string id, Dictionary<string, object> parameters)
        {
            if (Instance.registeredListeners.ContainsKey(id) == false)
                return;

            Instance.registeredListeners[id].RemoveAll(someaction =>
            {
                return someaction == null || someaction.Target == null;
            });

            foreach (var action in Instance.registeredListeners[id])
            {
                action.Invoke(parameters);
            }
        }
    }
}