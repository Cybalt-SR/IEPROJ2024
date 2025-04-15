using Assets.Scripts.Controller;
using Assets.Scripts.Data.AssetDictionaries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dictionary/Util Sound")]
public class UtilSoundDictionary : SingletonDictionary<AudioClip, UtilSoundDictionary>
{
}