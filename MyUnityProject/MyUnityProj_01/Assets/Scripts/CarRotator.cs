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

	void Start () {
		
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
			if (Input.GetKey(KeyCode.C)) {
				transform.Rotate (Vector3.forward, Time.deltaTime * -rollAdjustSpd, Space.Self);
			}
			if (Input.GetKey(KeyCode.Z)) {
				transform.Rotate (Vector3.forward, Time.deltaTime * rollAdjustSpd, Space.Self);
			}

			Vector3 newRotVec = new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
			Quaternion newRot = Quaternion.Euler (newRotVec);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, rollAdjustSpd);

			Debug.Log ("z roll is: " + transform.localRotation.eulerAngles.z + ", angleDif value is: ");

		}
	}
}
