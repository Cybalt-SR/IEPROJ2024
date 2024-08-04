using Assets.Scripts.Data.Pickup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controller.Attachments
{
    [RequireComponent(typeof(OverloadHealthObject))]
    public class LoadLoseSceneOnDie : MonoBehaviour
    {
        private OverloadHealthObject mOverloadHealthObject;

        private void Awake()
        {
            mOverloadHealthObject = GetComponent<OverloadHealthObject>();
        }

        // Start is called before the first frame update
        void Start()
        {
            mOverloadHealthObject.SubscribeOnDie(projectile =>
            {
                SceneManager.LoadScene("lose");
            });
        }
    }
}
