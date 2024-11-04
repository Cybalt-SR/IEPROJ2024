using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(ParticleSystem))]
    public class GlobalEffectController : MonoBehaviour
    {
        private ParticleSystem mParticleSystem;

        private void Awake()
        {
            mParticleSystem = GetComponent<ParticleSystem>();
        }

        public void Spawn(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            transform.forward = direction;
            mParticleSystem.Play();
        }
    }
}