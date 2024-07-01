using Assets.Scripts.Data;
using Assets.Scripts.Manager;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class UnitController : MonoBehaviour
{
    private Rigidbody mRigidbody;
    private CapsuleCollider mCapsuleCollider;
    public CapsuleCollider Collider { get { return mCapsuleCollider; } }
    //formerly private
    [SerializeField] public GunData mGun;
    public GunData Gun { get { return mGun; } }

    //Moving
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float speed_base = 1;
    public float Speed { get => speed_base; }
    public Vector3 MoveDir { get; protected set; }

    //Looking
    [SerializeField] private Transform shooting_reference;
    public Vector3 AimDir { get; private set; }
    private float lateral_distance;
    //shooting
    public int shots_before_reload = 0; // formerly private
    public bool reloading = false; // formerly private
    private float time_last_shot = 0;
    public float time_last_reload = 0; // formerly private

    protected virtual void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mCapsuleCollider = GetComponent<CapsuleCollider>();
    }
    protected virtual void Start()
    {
        var shooting_reference_delta = shooting_reference.transform.position;
        shooting_reference_delta.y = 0;
        lateral_distance = shooting_reference_delta.magnitude;
    }

    protected void AimAt(Vector3 pos)
    {
        var delta = transform.InverseTransformPoint(pos);
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
        if (shots_before_reload == 0)
        {
            reloading = true;
            return;
        }

        if (time_last_shot >= 1.0f / mGun.shots_per_second)
        {
            ProjectileManager.Instance.Shoot(shooting_reference.transform.position, AimDir.normalized, this);
            time_last_shot = 0;
            shots_before_reload--;
        }
    }

    protected virtual void Update()
    {
        time_last_shot += Time.deltaTime;

        if (reloading)
        {
            time_last_reload += Time.deltaTime;

            if (time_last_reload >= mGun.reload_time)
            {
                reloading = false;
                shots_before_reload = mGun.clip_size;
                time_last_reload = 0;
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (mRigidbody.velocity.sqrMagnitude < Speed * Speed)
        {
            mRigidbody.AddForce(acceleration * Time.fixedDeltaTime * MoveDir, ForceMode.VelocityChange);
        }
    }
}
