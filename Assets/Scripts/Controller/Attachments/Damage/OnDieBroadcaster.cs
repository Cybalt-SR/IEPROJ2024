using Assets.Scripts.Data.Pickup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(HealthObject))]
    public class OnDieBroadcaster : MonoBehaviour
    {
        private HealthObject mHealthObject;
        [SerializeField] private string _killed_;

        private void Awake()
        {
            mHealthObject = GetComponent<HealthObject>();
        }

        // Start is called before the first frame update
        void Start()
        {
            mHealthObject.SubscribeOnDie(From =>
            {
                if(From != null)
                    ActionWaiter.Broadcast(From.BroadcastId + "_killed_" + _killed_, transform);
            });
        }
    }
}
