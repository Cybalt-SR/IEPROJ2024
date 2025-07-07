using Assets.Scripts.Controller.Attachments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthObject), typeof(AudioSource))]
public class PlaySoundOnDie : MonoBehaviour
{
	private HealthObject healthObject;
	private AudioSource mAudioSource;

	[SerializeField] private string audio_id = "enemy_die";
	[SerializeField] private float volume_scale = 1.0f;

	private void Awake()
	{
		healthObject = GetComponent<HealthObject>();
		mAudioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		healthObject.SubscribeOnDie(other =>
		{
			var sound = UtilSoundDictionary.Instance.Get(audio_id);
			if (!SoundManager.CheckPlayedRecently(sound))
			{
				mAudioSource.PlayOneShot(sound, volume_scale);
			}
		});
	}
}
