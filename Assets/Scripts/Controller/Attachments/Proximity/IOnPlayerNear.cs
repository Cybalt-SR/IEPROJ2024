using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    public interface IOnPlayerNear
    {
        public void OnPlayerNear(UnitController player);
    }
}