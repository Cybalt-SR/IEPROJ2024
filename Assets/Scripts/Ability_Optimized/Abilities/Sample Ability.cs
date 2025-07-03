using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AbilityOP;

[CreateAssetMenu(fileName ="Sample Ability", menuName = "Ability Optimized/Abilities/Sample/Sample 1", order = 1)]
public class SampleAbility_data : AbilityData { }

public class SampleAbility : Ability
{
    protected override IEnumerator Active()
    {
        Debug.Log("Sample Ability Invoked");

        var cube = GameObject.Instantiate(Resources.Load("The Debug Cube") as GameObject);
        var rb = cube.GetComponent<Rigidbody>();
        var controller = Owner.GetComponent<UnitController>();


        Vector3 norm_aim_dir = controller.AimDir.normalized;
        norm_aim_dir.y = 0.5f;

        Vector3 hook_spawn_pos = Owner.transform.position + norm_aim_dir;
        hook_spawn_pos.y += Owner.GetComponent<Collider>().bounds.center.y;

        cube.transform.position = hook_spawn_pos;
        cube.AddComponent<Destroyer>();


        rb.AddForce(controller.AimDir.normalized * 10, ForceMode.Impulse);

        //yield return new WaitForSeconds(2);

        yield return null;
        Debug.Log("Sample Ability Executed");

    }

}


public class Destroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(e());
    }

    IEnumerator e()
    {
        float time = 0;

        gameObject.GetComponent<Collider>().isTrigger = true;

        while (time < 1.75f)
        {
            var scale = transform.localScale;
            scale *= 1 + Time.deltaTime * 2;
            transform.localScale = scale;
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}