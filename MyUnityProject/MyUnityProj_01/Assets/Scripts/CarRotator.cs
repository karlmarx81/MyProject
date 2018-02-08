using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotator : MonoBehaviour {

	public float rotMaxSpeed;
	public float rotAccel;
	public float rotNaturalDeAccel;
	public float rollAdjustSpd;
	public bool isRotYawGlobal;
	public bool isAutoRollAdjust;

	float currentHorRotSpd;
	float currentVerRotSpd;
	float currentRollSpd;

	[HideInInspector]
	public bool isFacingGround;

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay (transform.position, -transform.up * 10f);

		Gizmos.color = Color.blue;
		Gizmos.DrawRay (transform.position, transform.forward * 2f);

		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, transform.right * 2f);
	}

	void Update () {
		float horInput = Input.GetAxis ("Horizontal");	
		float verInput = Input.GetAxis ("Vertical");

		currentHorRotSpd += horInput * rotAccel;
		currentVerRotSpd += verInput * rotAccel;

		if (horInput == 0) {						
			currentHorRotSpd = Mathf.Lerp (currentHorRotSpd, 0, rotNaturalDeAccel);
		}

		if (verInput == 0) {
			currentVerRotSpd = Mathf.Lerp (currentVerRotSpd, 0, rotNaturalDeAccel);
		}

		currentHorRotSpd = Mathf.Clamp (currentHorRotSpd, -rotMaxSpeed, rotMaxSpeed);
		currentVerRotSpd = Mathf.Clamp (currentVerRotSpd, -rotMaxSpeed, rotMaxSpeed);

		if (isRotYawGlobal == true) {
			transform.Rotate (Vector3.up, Time.deltaTime * currentHorRotSpd, Space.World);	
		} else {
			transform.Rotate (Vector3.up, Time.deltaTime * currentHorRotSpd);	
		}
		transform.Rotate (Vector3.right, Time.deltaTime * currentVerRotSpd);

		if (isAutoRollAdjust == true) {
			if (Input.GetKey (KeyCode.C)) {
				transform.Rotate (Vector3.forward, Time.deltaTime * -rollAdjustSpd, Space.Self);
			}
			if (Input.GetKey (KeyCode.Z)) {
				transform.Rotate (Vector3.forward, Time.deltaTime * rollAdjustSpd, Space.Self);
			}
		}

		Ray ray = new Ray (transform.position, transform.up * -1f); //Cast ray to the ground and check the car's downward is facing ground
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.tag == "Ground")
				isFacingGround = true;			
		} else
			isFacingGround = false;

		if (isAutoRollAdjust == true) {	
			AdjustRoll ();
		}
	}

	void AdjustRoll()
	{
		Vector3 newRotVec;
		if (isFacingGround == true) {
			newRotVec = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0); //if car's downward is facing ground, adjust roll's target value is 0
		} else {
			newRotVec = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180); //else target value is 180
		}
		Quaternion newRot = Quaternion.Euler (newRotVec);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, rollAdjustSpd);

		Debug.Log ("z roll is: " + transform.localRotation.eulerAngles.z + ", angleDif value is: ");
	}
}
