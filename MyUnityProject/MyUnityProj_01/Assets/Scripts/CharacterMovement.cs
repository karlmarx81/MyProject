using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	public float maxSpeed;
	public float acceleration;
	public float jumpForce;

	public Transform body;
	public Transform torso;
	public Camera cam;

	public Text tempTextUI;

	Rigidbody thisRb;
	bool isOnGround;

	void Start () {
		thisRb = transform.GetComponent<Rigidbody> ();
		cam = Camera.main;
	}	

	void Update () {
		CheckOnGround ();
		CameraFollow ();
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

	void MakeJump(float jumpAmount)
	{
		thisRb.AddForce (Vector3.up * jumpAmount, ForceMode.Impulse);
	}

	void CameraFollow ()
	{
		cam.transform.position = transform.position + transform.up * 2f - transform.forward * 2f;
	}

	void CheckOnGround ()
	{
		float checkDist = 1f;
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

	void DebugLog()
	{
		tempTextUI.text = "Velocity: " + Mathf.RoundToInt(thisRb.velocity.magnitude).ToString() + ", isOnGround: " + isOnGround.ToString();
	}


}
