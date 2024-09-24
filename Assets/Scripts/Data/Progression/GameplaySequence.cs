using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.Progression
{
    [CreateAssetMenu(menuName = "Game/Dictionary/GameplaySequence")]
    public class GameplaySequence : ScriptableObject
    {
        public enum StepType { message, cutdialogue, cutslides, cutvideo}

        [Serializable]
        public class Step
        {
            public string trigger_event;
        }
    }
}
