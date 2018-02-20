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

	public Text tempTextUI;

	Rigidbody thisRb;
	bool isOnGround;
	bool isMoving;

	void Start () {
		thisRb = transform.GetComponent<Rigidbody> ();
	}	

	void Update () {
		CheckOnGround ();
		isMoving = CheckMoving ();
		DebugLog ();
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

	void MakeSubObjRotate()
	{
		
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


}
