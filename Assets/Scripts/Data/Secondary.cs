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


    public string SecondaryName { get { return secondaryName; } }
    public string ToolTip { get { return toolTip; } }
    public Image SecondaryIcon { get { return secondaryIcon; } }

    public Ability ShotEffect { get { return shotEffect; } }

    public Ability SecondaryAbility { get { return secondaryAbility; } }

   


}
