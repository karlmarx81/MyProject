using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject character;
	public float camHeight;
	public float camDistance;
	public float camHorOffset;
	public float camSpd;
	public float rotationSmoothTime = 1f;
	public Vector2 horRotationMinMax = new Vector2 (-80f, 60f);


	CharacterMovement controller;
	float xInputAmount;
	float yInputAmount;
	Vector3 rotationSmoothVelocity;

	void Start () {
		controller = character.GetComponent<CharacterMovement> ();
	}
	

	void Update () {
		xInputAmount += Input.GetAxis ("CommonX");
		yInputAmount += Input.GetAxis ("CommonY");
		yInputAmount = Mathf.Clamp (yInputAmount, horRotationMinMax.x, horRotationMinMax.y);
		Vector2 camControlVector = new Vector2 (xInputAmount, yInputAmount);

		controller.MakeRotate (transform.rotation.eulerAngles);

		CameraFollow (camControlVector); //convert this statement to delegate pattern
	}

	void CameraFollow (Vector2 rotateVec)
	{
		Vector3 targetRotVec = Vector3.SmoothDamp (transform.rotation.eulerAngles, new Vector3 (rotateVec.y, rotateVec.x, 0), ref rotationSmoothVelocity, 0f);
		transform.eulerAngles = targetRotVec;
		transform.position = controller.transform.position - transform.forward * camDistance + transform.up * camHeight + transform.right * camHorOffset;

	}
}
