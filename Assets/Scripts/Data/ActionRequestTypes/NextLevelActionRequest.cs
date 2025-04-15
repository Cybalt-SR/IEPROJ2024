using Assets.Scripts.Library.ActionBroadcaster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.ActionRequestTypes
{
    [CreateAssetMenu(menuName = "Game/ActionRequest/Next Level")]
    public class NextLevelActionRequest : ActionRequest
    {
        [TextArea] public string source;
    }
}
