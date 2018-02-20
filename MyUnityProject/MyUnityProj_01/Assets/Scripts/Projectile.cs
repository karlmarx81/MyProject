using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float flyingSpeed;
	public float maxDistance;

	float flightDistance;

	void Start () {
			
	}	

	void Update () {
		transform.Translate (Vector3.forward * flyingSpeed * Time.deltaTime);
		flightDistance += flyingSpeed * Time.deltaTime;

		if (flightDistance >= maxDistance) {
			Destroy (this.gameObject);
		}
	}
}
