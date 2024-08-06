using Assets.Scripts.Data;
using Assets.Scripts.Manager;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(AudioSource))]
public class UnitController : MonoBehaviour
{
    protected Rigidbody mRigidbody;
    private CapsuleCollider mCapsuleCollider;
    private AudioSource mAudioSource;
    public CapsuleCollider Collider { get { return mCapsuleCollider; } }

    [SerializeField] private GunData mBaseGun;
    private bool gunchanged = true;
    public bool GunChanged { get { return gunchanged; } set => gunchanged = value; }
    protected virtual void UpdateFinalGun() { mFinalGun = mBaseGun; }
    protected GunData mFinalGun;
    public GunData Gun
    {
        get
        {
            if(this.gunchanged)
                UpdateFinalGun();

            gunchanged = false;

            return mFinalGun;
        }
    }

    //Moving
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float speed_base = 1;
    public float Speed { get => speed_base; }
    [SerializeField] private Vector3 movedir;
    public Vector3 MoveDir {
        get => movedir;
        protected set{
            OnMove.Invoke(value);
            movedir = value;
        }
    }

    //Looking
    [SerializeField] protected Transform shooting_reference;
    [SerializeField] private Vector3 aimdir;
    public Vector3 AimDir
    {
        get => aimdir;
        private set
        {
            OnAim.Invoke(value);
            aimdir = value;
        }
    }
    private float lateral_distance;
    //shooting
    [SerializeField] private string team_id = "";
    public string TeamId => team_id;
    [SerializeField] private bool clip_full_at_start = true;
    private int shots_before_reload = 0;
    public int Shots_before_reload { get { return shots_before_reload; } }

    private bool reloading = false; // formerly private
    public bool Reloading {  get { return reloading; } }

    private float time_last_shot = 0;

    public float time_last_reload = 0; // formerly private
    public float Time_last_reload {  get { return time_last_reload; } }

    [SerializeField] private UnityEvent<Vector3> OnMove;
    [SerializeField] private UnityEvent<Vector3> OnAim;

    protected virtual void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mCapsuleCollider = GetComponent<CapsuleCollider>();
        mAudioSource = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
        if (clip_full_at_start)
            shots_before_reload = Gun.clip_size;

        var shooting_reference_delta = shooting_reference.transform.localPosition;
        shooting_reference_delta.y = 0;
        lateral_distance = shooting_reference_delta.magnitude;

        UpdateFinalGun();
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
            if(reloading == false && Gun.play_reload)
                mAudioSource.PlayOneShot(UtilSoundDictionary.Instance.Get(Gun.start_reload_sound_id));

            reloading = true;
            return;
        }

        if (time_last_shot >= 1.0f / Gun.shots_per_second)
        {
            ProjectileManager.Shoot(shooting_reference.transform.position, AimDir.normalized, this);
            mAudioSource.PlayOneShot(ShotSoundDictionary.Instance.Get(Gun.sound_id));
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

            if (time_last_reload >= Gun.reload_time)
            {
                if (reloading == true && Gun.play_reload)
                    mAudioSource.PlayOneShot(UtilSoundDictionary.Instance.Get(Gun.end_reload_sound_id));

                reloading = false;
                shots_before_reload = Gun.clip_size;
                time_last_reload = 0;
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (mRigidbody.velocity.sqrMagnitude < Speed * Speed)
        {
            if (MoveDir.sqrMagnitude > 1.1 || MoveDir.sqrMagnitude < 0.9)
                MoveDir = MoveDir.normalized;

            mRigidbody.AddForce(acceleration * Time.fixedDeltaTime * MoveDir, ForceMode.VelocityChange);

            var vel = mRigidbody.velocity;
        }
    }
}
