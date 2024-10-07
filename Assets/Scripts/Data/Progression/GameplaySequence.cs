using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using Assets.Scripts.Library;
using Assets.Scripts.Library.ActionBroadcaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.Progression
{
    [CreateAssetMenu(menuName = "Game/Dictionary/GameplaySequence")]
    public class GameplaySequence : SingletonResource<GameplaySequence>
    {
        [Serializable]
        public class EventResponse
        {
            public string trigger_event;
            public ActionRequest request;
        }

        [SerializeField] private List<EventResponse> sequentialresponses;

        public int Count => sequentialresponses.Count;
        public EventResponse GetKey(int i) => sequentialresponses[i];
    }
}
