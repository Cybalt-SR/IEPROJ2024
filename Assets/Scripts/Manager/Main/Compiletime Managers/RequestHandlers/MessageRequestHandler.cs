using Assets.Scripts.Data.ActionRequestTypes;
using Assets.Scripts.Library.ActionBroadcaster;
using External.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager.Main.Compiletime_Managers.RequestHandlers
{
    public class MessageRequestHandler : ActionRequestHandler<MessageActionRequest>
    {
        protected override bool ProcessRequest(MessageActionRequest somerequest)
        {
            return DialogueController.LoadMessage(somerequest.message);
        }
    }
}
