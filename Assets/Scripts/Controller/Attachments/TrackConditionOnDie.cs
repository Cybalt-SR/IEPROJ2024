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
    public class TrackConditionOnDie : MonoBehaviour
    {
        private HealthObject mHealthObject;
        [SerializeField] private string condition_name;
        [SerializeField] private int condition_threshold = 1;

        private void Awake()
        {
            mHealthObject = GetComponent<HealthObject>();
        }

        // Start is called before the first frame update
        void Start()
        {
            mHealthObject.SubscribeOnDie(projectile =>
            {
                AreaManager.TrackOrCreate(condition_name, condition_threshold);
            });
        }
    }
}
