using Assets.Scripts.Controller;
using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioOnUnitNear : MonoBehaviour
{
    private AudioSource mAudioSource;

    [SerializeField] private string open_util_audio_id;
    [SerializeField] private string close_util_audio_id;

    private bool alr_open = false;

    private void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out UnitController player) == false)
            return;

        if (alr_open == false)
            mAudioSource.PlayOneShot(UtilSoundDictionary.Instance.Get(open_util_audio_id));

        alr_open = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.attachedRigidbody == null)
            return;

        if (other.attachedRigidbody.gameObject.TryGetComponent(out UnitController player) == false)
            return;

        if (alr_open == true)
            mAudioSource.PlayOneShot(UtilSoundDictionary.Instance.Get(close_util_audio_id));

        alr_open = false;
    }
}
