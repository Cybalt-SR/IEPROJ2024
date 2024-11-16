using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using Assets.Scripts.Controller;
using UnityEngine.AI;
using gab_roadcasting;
using static UnityEngine.Rendering.DebugUI;
using System.Net.Security;


public class SlimeGun_Shot : Ability
{

    [Header("References")]
    [SerializeField] private SlimeGun_Hook hook;
    [SerializeField] private Slime_Gun_Adhesive adhesive;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material slimeMaterial;

    [Header("Shot Configurations")]
    [SerializeField] private float pullforce = 40f;
    [SerializeField] private float damageReductionPercent = 40f;
    [SerializeField] private float grapplingMultiplier = 2f;

    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float maxTetherDistance = 25f;

    [SerializeField] private float max_grapple_duration = 8f;

    [SerializeField] private float impact_damage = 1f;
    bool isPulled = false;

    private PlayerController player;
    private Coroutine grapplingCoroutine = null;

    private void Start()
    {
        hook.gameObject.SetActive(false);
        player = PlayerController.GetFirst();
        adhesive.gameObject.SetActive(false);

        //temp
        hook.onGrapplerHook += () => SwitchPlayerMaterials(true);
        hook.onGrapplerUnhooked += () => SwitchPlayerMaterials(false);
    }

    private void OnDisable()
    {
        SetGrappleState(false);
    }

    protected override void Cast()
    {
        if (!hook.isLocked)
            LaunchGrappler();
        else TriggerGrapple();
    }



    private void TriggerGrapple()
    {
      
        IEnumerator Grapple()
        {

            float grapple_duration = 0f;

            SetGrappleState(true);

            //soft lock guard if navmesh's path locks the player in place
            var delta = hook.transform.position - player.transform.position;
            while (Vector3.SqrMagnitude(delta) > 3f * 3f && grapple_duration < max_grapple_duration)
            {
                delta = hook.transform.position - player.transform.position;
                grapple_duration += Time.deltaTime;
                player.TryGetComponent(out Rigidbody rb);
                rb.AddForce(delta.normalized * pullforce * Time.deltaTime, ForceMode.VelocityChange);

                yield return null;
            }

            foreach (var enemy in adhesive.CollisionList)
            {
                var hp = enemy.GetComponent<HealthObject>();
                hp.enabled = true;
                hp.TakeDamage(impact_damage, player);
            }

            EndGrappling();

        }
        grapplingCoroutine = StartCoroutine(Grapple());


    }

    private void LaunchGrappler()
    {
        var player = PlayerController.GetFirst();

        float heightOffset = player.GetComponent<Collider>().bounds.center.y;


        //Starting position of grappler
        Vector3 aimDir = player.AimDir.normalized * 3;
        aimDir.y = 0.5f;

        Vector3 affixedPosition = player.transform.position + aimDir;
        affixedPosition.y += heightOffset;

        hook.transform.position = affixedPosition;
        hook.gameObject.SetActive(true);

        var hookBody = hook.GetComponent<Rigidbody>();
        hookBody.AddForce(player.AimDir.normalized * projectileSpeed, ForceMode.Impulse);


        adhesive.gameObject.SetActive(true);
    }

    
    private void EndGrappling()
    {
        hook.gameObject.SetActive(false);
        hook.ClearLock();
        SetGrappleState(false);
        adhesive.gameObject.SetActive(false);
    }

    private void SetGrappleState(bool value)
    {
        isPulled = value;

        //temp
        if (player == null)
            return;

        player.GetComponent<PlayerStateHandler>().canMove = !value;
        player.gameObject.layer = LayerMask.NameToLayer(value ? "Ghost" : "Default");
    }

    private void SwitchPlayerMaterials(bool isHooked)
    {
        foreach (var sr in player.GetComponentsInChildren<SpriteRenderer>())
            sr.material = isHooked ? slimeMaterial : defaultMaterial;
    }
   

    protected override void Update()
    {
        base.Update();

        if (!hook.isLocked)
            return;

        if (!isPulled && Vector3.Distance(player.transform.position, hook.transform.position) > maxTetherDistance)
            EndGrappling();
    }


    //Damage Reduction

    protected override void Initialize()
    {
        void DamageReduction(Dictionary<string, object> p)
        {
            var damage = p["Damage"] as Wrapper<float>;

            float damageReductionRatio = damageReductionPercent;
            if (hook.isLocked)
                damageReductionRatio *= grapplingMultiplier;
            damageReductionRatio = Mathf.Clamp(damageReductionRatio, 0, 100);

            damage.value *= 1 - (damageReductionRatio / 100);

        }

        EventBroadcasting.AddListener(EventNames.PLAYER_EVENTS.ON_OVERLOAD_CHANGED, DamageReduction);
    }

  

}
