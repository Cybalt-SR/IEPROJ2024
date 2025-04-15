using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments.Animation
{
    public class Shrinker : MonoBehaviour
    {
        [SerializeField] private float shrink_ratio;
        [SerializeField] private float shrink_speed;
        [SerializeField] private Vector3 target_scale;


        public void SetRatio(float ratio)
        {
            shrink_ratio = ratio;
        }
        private void Update()
        {
            shrink_ratio = Mathf.Lerp(shrink_ratio, 1, Time.deltaTime * shrink_speed);
        }

        private void FixedUpdate()
        {
            this.transform.localScale = Vector3.Lerp(Vector3.one, target_scale, shrink_ratio);
        }
    }
}
