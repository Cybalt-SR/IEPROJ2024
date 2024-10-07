using Assets.Scripts.Library.ActionBroadcaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.ActionRequestTypes
{
    [CreateAssetMenu(menuName = "Game/ActionRequest/Message")]
    public class MessageActionRequest : ActionRequest
    {
        [TextArea] public string message;
    }
}
