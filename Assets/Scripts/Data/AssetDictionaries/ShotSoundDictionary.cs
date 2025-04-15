using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dictionary/Shot Sound")]
public class ShotSoundDictionary : SingletonDictionary<AudioClip, ShotSoundDictionary>
{
}