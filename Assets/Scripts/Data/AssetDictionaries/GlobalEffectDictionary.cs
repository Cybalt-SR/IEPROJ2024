using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Data.AssetDictionaries
{
    [CreateAssetMenu(menuName = "Game/Dictionary/GlobalEffect")]
    public class GlobalEffectDictionary : SingletonDictionary<GlobalEffectController>
    {
    }
}
