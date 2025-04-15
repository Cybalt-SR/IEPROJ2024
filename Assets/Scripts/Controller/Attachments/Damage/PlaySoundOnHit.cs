using Assets.Scripts.Controller.Attachments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileHittable), typeof(AudioSource))]
public class PlaySoundOnHit : MonoBehaviour
{
    private ProjectileHittable mProjectileHittable;
    private AudioSource mAudioSource;

    [SerializeField] private string audio_hit_id;
    [SerializeField] private float volume_scale = 1.0f;

    private void Awake()
    {
        mProjectileHittable = GetComponent<ProjectileHittable>();
        mAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mProjectileHittable.Subscribe(other =>
        {
            mAudioSource.PlayOneShot(HitSoundDictionary.Instance.Get(audio_hit_id), volume_scale);
        });
    }
}
