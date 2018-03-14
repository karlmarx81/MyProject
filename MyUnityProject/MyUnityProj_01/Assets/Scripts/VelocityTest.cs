using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTest : MonoBehaviour {

	public float velocityMag;
	public Vector3 applyDirection;

	Rigidbody thisRb;
	Vector3 initPos;

	void Start () {
		thisRb = GetComponent<Rigidbody> ();
		initPos = this.transform.position;
	}	

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			thisRb.velocity = applyDirection.normalized * velocityMag;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			thisRb.velocity = Vector3.zero;
			transform.position = initPos;
			transform.rotation = Quaternion.identity;
		}
	}
}
