using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CarControllerNetwork : NetworkBehaviour {

	public List<AxleInfo> axleInfos;

	public float maxDriveTorque;
	public float maxBrakeTorque;
	public float maxSteering;

	public float cameraHeight;
	public float cameraDistance;

	Transform cameraTransform;

	[SyncVar]
	public Color carColor;

	void Start () {
		if (!isLocalPlayer) {			
			return;
		}

		for (int i = 0; i < axleInfos.Count; i++) {
			axleInfos [i].leftWheel.ConfigureVehicleSubsteps (5, 12, 15);
			axleInfos [i].rightWheel.ConfigureVehicleSubsteps (5, 12, 15);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{		
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

		if (Input.GetKeyDown (KeyCode.Tab)) {										
			ChangeColor ();
		}	
	}

	void Update()
	{		
		if (cameraTransform != null) {
			cameraTransform.position = transform.position + new Vector3(0,cameraHeight,cameraDistance);
			cameraTransform.LookAt (this.transform);	
		}

		GetComponent<MeshRenderer> ().material.color = carColor;
	}

	public override void OnStartLocalPlayer()
	{	
		cameraTransform = Camera.main.transform;
	}

	void ChangeColor()	
	{		
		if (isLocalPlayer) {
			Color col = new Vector4 (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), 1);
			CmdChangeColor (col);
		}
	}

	[Command]
	void CmdChangeColor(Color col)
	{	
		carColor = col;
	}

	//[ClientRpc]
	//void RpcReceiveColor()
	//{
	//	GetComponent<MeshRenderer>().material.color = carColor;
	//}
}