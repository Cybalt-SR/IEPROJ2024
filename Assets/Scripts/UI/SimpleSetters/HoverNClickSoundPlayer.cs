using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverNClickSoundPlayer : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioSource speakers;
    [SerializeField] private string enter_util_audio_id;
    [SerializeField] private string click_util_audio_id;

    public void OnPointerClick(PointerEventData eventData)
    {
        speakers.PlayOneShot(UtilSoundDictionary.Instance.Get(click_util_audio_id));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        speakers.PlayOneShot(UtilSoundDictionary.Instance.Get(enter_util_audio_id));
    }
}
