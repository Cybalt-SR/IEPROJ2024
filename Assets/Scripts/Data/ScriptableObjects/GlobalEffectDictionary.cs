using Assets.Scripts.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Game/Dictionary/GlobalEffect")]
    public class GlobalEffectDictionary : SingletonDictionary<GlobalEffectController>
    {
    }
}
