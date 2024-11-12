using Assets.Scripts.Library.ActionBroadcaster;
using External.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.ActionRequestTypes
{
    [CreateAssetMenu(menuName = "Game/ActionRequest/Dialogue")]
    public class DialogueActionRequest : ActionRequest
    {
        public CharacterSet characterset;
        public Dialogue dialogue;
    }
}
