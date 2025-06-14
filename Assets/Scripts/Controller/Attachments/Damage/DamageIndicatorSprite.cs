using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorSprite : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] public Color DamageColor = Color.red;
    private Color OriginalColor;
    private float timer;
    bool Damaged = false;
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.OriginalColor = this.sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Damaged)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0.0f;
                this.Damaged = false;
                this.sprite.color = this.OriginalColor;
            }
        }
    }

    public void changeColor()
    {
        this.timer = 0.35f;
        this.sprite.color = this.DamageColor;
        this.Damaged = true;
    }
}
