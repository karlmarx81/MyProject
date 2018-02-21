using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	public float maxSpeed;
	public float acceleration;
	public float jumpForce;
	public float movingVelThreshold = 0.5f;

	public Transform body;
	public Transform torso;
	public float characterSlerpSpeed = 30f;
	public float subModelslerpSpeed = 30f;
	public Vector2 torsoPitchMinMax = new Vector2 (300f, 50f);

	public Text tempTextUI;

	private Vector3 _headingDirVec;
	[HideInInspector]
	public Vector3 HeadingDirVec
	{
		get { 
			return _headingDirVec;
		}

		set { 
			_headingDirVec = value;
		}
	}

	Rigidbody thisRb;
	bool isOnGround;
	bool isMoving;

	void Start () {
		thisRb = transform.GetComponent<Rigidbody> ();
	}	

	void Update () {
		CheckOnGround ();
		isMoving = CheckMoving ();
		MakeRotate (_headingDirVec);
		MakeTorsoRotate ();
		DebugLog ();
		DebugRay ();
	}

	void FixedUpdate()
	{
		float _forwardAccel = Input.GetAxis ("Vertical");
		float _strafeAccel = Input.GetAxis ("Horizontal");
		float jumpAmount = Input.GetAxis ("Jump");

		MakeMove (_forwardAccel, _strafeAccel);
		MakeJump (jumpAmount);
	}

	void MakeMove(float forwardInput, float strafeInput)
	{
		float _forwardAccel = forwardInput * acceleration * Time.fixedDeltaTime;
		float _strafeAccel = strafeInput * acceleration * Time.fixedDeltaTime;

		thisRb.AddForce (transform.forward * _forwardAccel);
		thisRb.AddForce (transform.right * _strafeAccel);	

		if (thisRb.velocity.magnitude > maxSpeed) {
			thisRb.velocity = thisRb.velocity.normalized * maxSpeed;
		}
	}

	public void MakeRotate(Vector3 rotateVec)
	{
		Quaternion targetRot = Quaternion.Euler (0, rotateVec.y, 0);
		if (isMoving == true) {
			Quaternion newRot = Quaternion.Slerp (transform.rotation, targetRot, characterSlerpSpeed * Time.deltaTime);
			transform.rotation = newRot;
		}
	}

	void MakeTorsoRotate()
	{	
		//this method is no use for now, but worth for dig it.
		Vector3 horElement = torso.forward - (Vector3.Dot (torso.forward, Vector3.up) * Vector3.up); //make forward vector which follows y axis of torso's local direction https://forum.unity.com/threads/limiting-x-rotation.5834/
		float angleDiff = Vector3.Angle (horElement, torso.forward);

		float pitch = _headingDirVec.x;

		if (pitch > 270f) //heading direction is upward
		{
			if (pitch < torsoPitchMinMax.x) {
				pitch = torsoPitchMinMax.x;
			}
		}
		else if (pitch > 0f && pitch < 90f) //heading direction is downward
		{
			if (pitch > torsoPitchMinMax.y) {
				pitch = torsoPitchMinMax.y;
			}
		}

		Vector3 torsoDirVec = new Vector3(pitch, _headingDirVec.y, 0f);
		torso.transform.rotation = Quaternion.Euler (torsoDirVec);
	}

	void MakeJump(float jumpAmount)
	{
		if (isOnGround == true) {
			thisRb.AddForce (Vector3.up * jumpAmount * jumpForce, ForceMode.Impulse);
		}
	}

	void CheckOnGround ()
	{
		float checkDist = 1.5f;
		Debug.DrawRay (transform.position, Vector3.down * checkDist, Color.red);
		Ray ray = new Ray (transform.position, -Vector3.up);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, checkDist)) {			
			if (hit.collider.gameObject.tag == "Ground") {
				isOnGround = true;
			}
		} else
			isOnGround = false;
	}

	bool CheckMoving()
	{
		if (thisRb.velocity.magnitude > movingVelThreshold) {
			return true;
		} else
			return false;
	}

	void DebugLog()
	{
		tempTextUI.text = "Velocity: " + Mathf.RoundToInt(thisRb.velocity.magnitude).ToString() + ", isOnGround: " + isOnGround.ToString();
	}

	void DebugRay()
	{		
		Vector3 rayDir = Quaternion.Euler (_headingDirVec) * transform.forward * 10f;
		Debug.DrawRay (transform.position, rayDir, Color.red);
	}

}
