using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour, ISingleton<CameraController>
    {
        private Camera m_Camera;
        public Camera Camera { get { return m_Camera; } }
        private void Awake()
        {
            ISingleton<CameraController>.Instance = this;
            m_Camera = GetComponent<Camera>();
        }
    }
}