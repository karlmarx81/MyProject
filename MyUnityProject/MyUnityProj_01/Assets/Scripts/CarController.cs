using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public List<AxleInfo> axleInfos;

	public float maxDriveTorque;
	public float maxBrakeTorque;
	public float maxSteering;

	//Rigidbody thisRb;

	// Use this for initialization
	void Start () {
		//thisRb = gameObject.GetComponent<Rigidbody> ();
		for (int i = 0; i < axleInfos.Count; i++) {
			axleInfos [i].leftWheel.ConfigureVehicleSubsteps (5, 12, 15);
			axleInfos [i].rightWheel.ConfigureVehicleSubsteps (5, 12, 15);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float accel = maxDriveTorque * Input.GetAxis ("Vertical") * Time.fixedDeltaTime;
		float brake = Input.GetAxis("Vertical") < 0 ? maxBrakeTorque * Input.GetAxis ("Vertical") * Time.fixedDeltaTime : 0f;	
		float angular = maxSteering * Input.GetAxis ("Horizontal") * Time.fixedDeltaTime;


		for (int i = 0; i < axleInfos.Count; i++) {
			if (axleInfos [i].isDriveWheel == true) {
				
				axleInfos [i].leftWheel.motorTorque = accel + brake;
				axleInfos [i].rightWheel.motorTorque = accel + brake;


			}

			if (axleInfos [i].isSteeringWheel == true) {
				axleInfos [i].leftWheel.steerAngle = angular;
				axleInfos [i].rightWheel.steerAngle = angular;
			}
		}
	}
}

[System.Serializable]
public class AxleInfo
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool isDriveWheel;
	public bool isSteeringWheel;
}
