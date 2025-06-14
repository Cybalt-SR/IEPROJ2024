using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamageIndicator : MonoBehaviour
{
    public Renderer mesh;
    [SerializeField] public Material damageMaterial;
    private Material originalMaterial;
    private float timer;
    bool Damaged = false;

    // Start is called before the first frame update
    void Start()
    {
        this.mesh = GetComponent<Renderer>();
        this.originalMaterial = this.mesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Damaged)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0.0f;
                this.Damaged = false;
                this.mesh.material = this.originalMaterial;
            }
        }
    }

    public void changeColor()
    {
        this.timer = 0.35f; 
        this.mesh.material = this.damageMaterial;
        this.Damaged = true;
    }
}
