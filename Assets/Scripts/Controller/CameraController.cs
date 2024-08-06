using Assets.Scripts.Library;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoSingleton<CameraController>
    {
        private Camera m_Camera;
        public Camera Camera { get { return m_Camera; } }
        override protected void Awake()
        {
            base.Awake();

            m_Camera = GetComponent<Camera>();
        }

        public bool PointInCameraView(Vector3 point)
        {
            bool Is01(float a)
            {
                return a > 0 && a < 1;
            }

            Vector3 viewport = Camera.WorldToViewportPoint(point);
            bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
            bool inFrontOfCamera = viewport.z > 0;

            return inCameraFrustum && inFrontOfCamera;
        }
    }
}