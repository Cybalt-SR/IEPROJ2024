using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Library.ActionBroadcaster
{
    public abstract class ActionRequestHandler_Base : MonoBehaviour
    {
        abstract public bool TryPass(IActionRequest somerequest);
    }

    public class ActionRequestHandler<T>: ActionRequestHandler_Base where T : IActionRequest
    {
        public override bool TryPass(IActionRequest somerequest)
        {
            return somerequest.GetType() == typeof(T);
        }
    }
}
