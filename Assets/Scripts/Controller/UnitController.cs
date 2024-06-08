using Assets.Scripts.Data;
using Assets.Scripts.Manager;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    [SerializeField] private GunData Gun;

    //Moving
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float speed_base = 1;
    public float Speed { get => speed_base; }
    public Vector3 MoveDir { get; protected set; }

    //Looking
    [SerializeField] private Transform shooting_reference;
    public Vector3 AimDir { get; private set; }
    private float lateral_distance;

    protected virtual void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    protected virtual void Start()
    {
        var shooting_reference_delta = shooting_reference.transform.position;
        shooting_reference_delta.y = 0;
        lateral_distance = shooting_reference_delta.magnitude;
    }

    protected void AimAt(Vector3 pos)
    {
        var delta = pos - transform.position;
        Vector3 normalized_delta = Vector3.zero;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.z))
        {
            normalized_delta.x = Mathf.Sign(delta.x);
            normalized_delta.z = 0;
        }
        else
        {
            normalized_delta.z = Mathf.Sign(delta.z);
            normalized_delta.x = 0;
        }

        Vector3 ref_pos = normalized_delta;
        ref_pos *= lateral_distance;
        ref_pos.y = shooting_reference.transform.localPosition.y;
        shooting_reference.transform.localPosition = ref_pos;

        var true_from = shooting_reference.transform.position;

        var true_dir = pos - true_from;
        true_dir.y = 0;

        AimDir = true_dir;
    }

    protected void Fire()
    {
        ProjectileManager.Instance.Shoot(shooting_reference.transform.position, AimDir.normalized, this.Gun);
    }

    private void FixedUpdate()
    {
        if (m_rigidbody.velocity.sqrMagnitude < Speed * Speed)
        {
            m_rigidbody.AddForce(acceleration * Time.fixedDeltaTime * MoveDir, ForceMode.VelocityChange);
        }
    }
}
