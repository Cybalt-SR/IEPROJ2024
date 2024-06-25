using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controller.Attachments
{
    public class TriggerSphere : MonoBehaviour
    {
        [SerializeField] private UnitController triggerer;
        [SerializeField] private List<GameObject> inside;

        private void OnTriggerStay(Collider other)
        {
            if (inside.Contains(other.gameObject) == false)
                inside.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (inside.Contains(other.gameObject))
                inside.Remove(other.gameObject);
        }

        private void Update()
        {
            foreach (GameObject go in inside)
                foreach (var iface in go.GetComponents<IOnPlayerNear>())
                    iface.OnPlayerNear(triggerer);
        }
    }
}