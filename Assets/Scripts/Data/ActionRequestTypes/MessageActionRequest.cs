using Assets.Scripts.Library.ActionBroadcaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data.ActionRequestTypes
{
    public struct MessageActionRequest : IActionRequest
    {
        public string message;
    }
}
