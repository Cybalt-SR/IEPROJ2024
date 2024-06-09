using UnityEngine;

namespace Assets.Scripts.Controller
{
    [RequireComponent(typeof(ParticleSystem))]
    public class EffectController : PooledBehaviour<EffectController>
    {
        private ParticleSystem mParticleSystem;

        public bool IsDone
        {
            get
            {
                return mParticleSystem.isPlaying == false;
            }
        }

        private void Awake()
        {
            mParticleSystem = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            mParticleSystem.Stop();
            mParticleSystem.Clear();
            mParticleSystem.Play();
        }
    }
}
