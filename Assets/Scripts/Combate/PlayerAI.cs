using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerAI : MonoBehaviour
{
    public int damage;

    public float fireTime;
    public float _firetime;

	internal bool isAttacking;
    public float range;
	internal bool isAiming;

    public Animator myAnimator;

	internal List<Color> playerColors;
	internal Color selectedColor;

	private GameObject Pivot;
	private Vector3 pivotTransform;
	private Vector3 _pivotTransform;
	private Vector3 _pivotTransformBoos;

	private float currentFrame;
	private float _currentFrame;
	private float framesDuration;

	public ColorManager colorManager;
	public AudioSource playerAudio;
	public AudioClip shootSound;

    internal Quaternion lookDirection;
    internal Vector3 instantiatePosition;

    public float paintCharges;

	public playerController PlayerController;

    public GameObject crossAim;

    void Awake(){
	
		Pivot = GameObject.FindGameObjectWithTag ("Camera").transform.GetChild (0).gameObject;

	}

    void Start()
    {
		playerColors = new List<Color> ();

        isAiming = false;
        _firetime = fireTime;
		isAttacking = false;

		pivotTransform = new Vector3 (0.17f,2.83f,-5.08f);
		_pivotTransform = new Vector3 (0.79f,2.42f,-2.03f);
		_pivotTransformBoos = new Vector3 (0.36f,2.42f,-3.523f);

		currentFrame = 0;
		_currentFrame = 15;
		framesDuration = 0.25f;
    }
    // Update is called once per frame
    void Update()
    {
		RangeWeapon ();
    }

    public void RangeWeapon()
    {
        CrossHair();

        #region Shoot Bullet

        if (paintCharges > 0)
        {
            if (_firetime >= 0)
            {
                _firetime--;
            }

            if (_firetime <= 0 && Input.GetButton("Fire1") && isAiming)
            {
                myAnimator.SetBool("IsShooting", true);
                paintCharges -= 1;
                Fire();
                _firetime = fireTime;
            }
        }

        #endregion

        #region Aim

        if (Input.GetMouseButtonDown (1))
        {
			myAnimator.SetBool ("Aim",true);
			_currentFrame = 0;
			isAiming = true;
		}

		else if (Input.GetMouseButtonUp (1))
        {
            myAnimator.SetBool ("Aim",false);
			currentFrame = 0;
			isAiming = false;
		}

		if (isAiming)
        {
			if(currentFrame <= framesDuration)
			{
				
				Pivot.transform.localPosition = new Vector3(Easing.QuartEaseIn(currentFrame, pivotTransform.x, (_pivotTransform.x - pivotTransform.x), framesDuration),
					Easing.QuartEaseIn(currentFrame, pivotTransform.y, (_pivotTransform.y - pivotTransform.y), framesDuration),
					Easing.QuartEaseIn(currentFrame, pivotTransform.z, (_pivotTransform.z - pivotTransform.z), framesDuration));        
				currentFrame+= Time.deltaTime;

			}
		}
		else if  (!isAiming)
        {
			if(_currentFrame <= framesDuration)
			{
				
				Pivot.transform.localPosition = new Vector3(Easing.QuartEaseOut(_currentFrame, _pivotTransform.x, (pivotTransform.x - _pivotTransform.x), framesDuration),
					Easing.QuartEaseOut(_currentFrame, _pivotTransform.y, (pivotTransform.y - _pivotTransform.y), framesDuration),
					Easing.QuartEaseOut(_currentFrame, _pivotTransform.z, (pivotTransform.z - _pivotTransform.z), framesDuration));        
				_currentFrame+= Time.deltaTime;

			}
		}

        #endregion
    }

    void CrossHair()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hitInfo = new RaycastHit();

        if (isAiming && Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            crossAim.SetActive(true);

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                lookDirection = Quaternion.LookRotation(hitInfo.normal * -1);
                instantiatePosition = hitInfo.point + (hitInfo.normal * 0.01f);
                crossAim.transform.position = instantiatePosition;
                crossAim.transform.localRotation = lookDirection;
            }
        }
        else if (!Physics.Raycast(ray, out hitInfo, Mathf.Infinity) || !isAiming)
        {
            crossAim.SetActive(false);
        }
    }

    void Fire()
    {
        GameObject obj = PoolingObjectScript.current.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

		playerAudio.PlayOneShot (shootSound);
		}
}
