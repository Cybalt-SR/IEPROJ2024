using Assets.Scripts.Controller;
using Assets.Scripts.Data.ScriptableObjects;
using Assets.Scripts.Gameplay.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class GlobalEffectManager : Manager_Base<GlobalEffectManager>
    {
        private readonly Dictionary<string, GlobalEffectController> globaleffects = new();

        public void Spawn(string id, Vector3 position, Vector3 direction)
        {
            if (globaleffects.ContainsKey(id) == false) {
                var neweffect = GlobalEffectDictionary.Instance.Get(id);

                if (neweffect == null)
                    return;

                globaleffects.Add(id, Instantiate(neweffect.gameObject, this.transform).GetComponent<GlobalEffectController>());
            }

            globaleffects[id].Spawn(position, direction);
        }
    }
}
