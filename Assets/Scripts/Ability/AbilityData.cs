using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityData
{
    [SerializeField] private string abilityID;
    [SerializeField] private string effectName;
    [SerializeField] private Sprite effectIcon;

    [SerializeField] private float cooldown;

    [TextArea(1, 10)]
    [SerializeField] private string tooltip;

    [TextArea(1, 20)]
    [SerializeField] private string longDescription;


    public string AbilityID { get { return abilityID; } }
    public string EffectName { get { return effectName; } }
    public Sprite EffectIcon { get { return effectIcon; } }
    public string Tooltip { get { return tooltip; } }
    public string LongDescription { get { return longDescription; } }
    public float Cooldown { get { return cooldown; } }

}
