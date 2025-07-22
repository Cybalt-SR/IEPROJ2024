using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DirectionalAnimator3D : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Vector3 _aimdir;
    private Vector3 _movedir;
    private bool moved_this_frame;

    [SerializeField] bool use_animator = true;
    [SerializeField] float aimmove_angle = 0;
    [SerializeField] float turn_speed = 10;
    [SerializeField] float time_shaking = 0;
    [SerializeField] float shake_strength = 0.2f;

    private void Awake()
    {
        var possibleanim = GetComponent<Animator>();

        if(possibleanim != null)
			_animator = possibleanim;
	}

    private void Update()
    {
        if (time_shaking > 0)
        {
            time_shaking -= Time.deltaTime;
            this.transform.localPosition = Random.insideUnitCircle * shake_strength;
        }
        if (time_shaking < 0)
        {
            time_shaking = 0;
            this.transform.localPosition = Vector3.zero;
        }
    }

    public void Shake()
    {
        time_shaking += 0.05f;
    }

    public void OnMoveEvent(Vector3 movedir)
    {
        moved_this_frame = true;

        movedir = Quaternion.AngleAxis(-CameraController.Instance.Camera.transform.eulerAngles.y, Vector3.up) * movedir;
        _movedir = movedir;

        aimmove_angle = Vector3.SignedAngle(_aimdir, _movedir, Vector3.up);

        float[] divs = {
            -45,
            45,
            135,
            -135,
        };

        bool isforward = divs[0] < aimmove_angle && aimmove_angle < divs[1];
        bool isright = divs[1] < aimmove_angle && aimmove_angle < divs[2];
        bool isbackward = divs[2] < aimmove_angle || aimmove_angle < divs[3];
        bool isleft = divs[3] < aimmove_angle && aimmove_angle < divs[0];

        if (use_animator)
        {
            _animator.SetBool("moving", true);

            _animator.SetBool("forward", isforward);
            _animator.SetBool("backward", isbackward);
            _animator.SetBool("left", isleft);
            _animator.SetBool("right", isright);
        }
    }

    private void LateUpdate()
	{
		if(moved_this_frame == false && use_animator)
        {
			//_animator.SetBool("moving", false);

			//_animator.SetBool("forward", false);
            //_animator.SetBool("backward", false);
            //_animator.SetBool("left", false);
            //_animator.SetBool("right", false);
        }
    }

    public void OnAimEvent(Vector3 aimdir)
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(aimdir), turn_speed * Time.deltaTime);

        var camtransdir = Quaternion.AngleAxis(-CameraController.Instance.Camera.transform.eulerAngles.y, Vector3.up) * aimdir;
        _aimdir = camtransdir;
    }

    public void OnShootEvent(int shoottype)
    {
        var triggername = "shoot" + shoottype;
		_animator.SetTrigger(triggername);
	}
}
