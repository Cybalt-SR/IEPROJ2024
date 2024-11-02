using Assets.Scripts.Controller;
using Assets.Scripts.Gameplay.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager.Main
{
    public class PlayerManager : Manager_Base<PlayerManager>
    {
        [SerializeField] private List<PlayerController> playerControllers = new List<PlayerController>();

        protected override void Awake()
        {
            base.Awake();

            playerControllers = FindObjectsByType<PlayerController>(FindObjectsSortMode.None).ToList();
        }

        public static PlayerController GetByName(string name)
        {
            return Instance.playerControllers.Find(p => p.PlayerId == name);
        } 
    }
}
