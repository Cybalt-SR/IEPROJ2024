using Assets.Scripts.Library.ActionBroadcaster;
using External.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data.ActionRequestTypes
{
    public struct DialogueActionRequest : IActionRequest
    {
        public Dialogue dialogue;
    }
}
