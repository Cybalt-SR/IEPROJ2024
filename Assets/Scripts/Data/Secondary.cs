using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Abilities;

[CreateAssetMenu(fileName ="Secondary Gun", menuName ="Game/Weapons/Secondary")]
public class Secondary : ScriptableObject
{

    [SerializeField] private string secondaryName;

    [TextArea(1,3)] 
    [SerializeField] private string toolTip;

    [SerializeField] private Image secondaryIcon;

    [SerializeField] private Ability shotEffect;
    [SerializeField] private Ability secondaryAbility;

}
