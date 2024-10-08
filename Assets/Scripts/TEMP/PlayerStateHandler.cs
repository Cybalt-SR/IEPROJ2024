using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{
    public bool isInvisible { get; private set; }
    public bool canMove = true;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float defaultSpriteAlpha = 1f;
    [SerializeField] private float invisibleSpriteAlpha = 0.6f;
    [SerializeField] private string ghostLayerName;
    [SerializeField] private string defaultLayerName = "Default";

    //temp
    public void InvokeInvisibility(float duration)
    {
        void setInvisibility(bool value)
        {
            isInvisible = value;
            HUDEffectManager.Instance.SetInvisibilityOverlay(value);
            Color c = sprite.color;
            c.a = value ? invisibleSpriteAlpha : defaultSpriteAlpha;
            sprite.color = c;  
            gameObject.layer = LayerMask.NameToLayer(value ? ghostLayerName : defaultLayerName);
        }

        IEnumerator Stealth(float duration)
        {
            setInvisibility(true);
            yield return new WaitForSeconds(duration);
            setInvisibility(false);
        }

        StartCoroutine(Stealth(duration)); 
    }


}   
