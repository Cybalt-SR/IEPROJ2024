using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip footstep_clip;
    [SerializeField] private AudioSource speakers;
    [SerializeField] private float volume_scale = 1.0f;

    private void OnEnable()
    {
        speakers.PlayOneShot(footstep_clip, volume_scale);
    }
}
