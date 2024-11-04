using Assets.Scripts.Data.ActionRequestTypes;
using Assets.Scripts.Library.ActionBroadcaster;
using External.Dialogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Manager.Main.Runtime_Managers.RequestHandlers
{
    public class DialogueRequestHandler : ActionRequestHandler<DialogueActionRequest>
    {
        protected override bool ProcessRequest(DialogueActionRequest somerequest)
        {
            return DialogueController.LoadDialogue(somerequest.dialogue, somerequest.characterset, null, () => { });
        }
    }
}
