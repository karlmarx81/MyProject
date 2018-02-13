using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CarCollisionMeasurementNet : NetworkBehaviour {

	public Color color = Color.red;
	public float rayLength = 3f;
	public Text textUI;

	public float carVelocity;
	public float rotateSpd;

	public float addForceMultiplier;
	public float relativeUpwardModifier;

	Vector3 collisionNormal;
	Vector3 relativeVel;
	Vector3 originalPos;
	Quaternion originalRot;

	float opponentMass;
	string opponentName;

	Rigidbody thisRb;

	void Start () {
		thisRb = this.gameObject.GetComponent<Rigidbody> ();
		originalPos = transform.position;
		originalRot = transform.rotation;

		if (isLocalPlayer) {
			textUI = GameObject.Find ("CarText").GetComponent<Text> ();
		}
	}	

	void Update () {
		//float scalarValue = (int)Vector3.Dot (collisionNormal, relativeVel);
		//float damage = opponentMass * scalarValue;

		if (collisionNormal != Vector3.zero) {
			Debug.DrawRay (transform.position, collisionNormal * rayLength, color); //collisionNormal Draw
			Debug.DrawRay (transform.position, relativeVel * rayLength, color); //relativeVelocity Draw

//			if (textUI != null) {
//				textUI.text = "Force: " + scalarValue.ToString () + "\nName: " + opponentName + "\nDamage: " + damage;
//			}
		}

		if (!isLocalPlayer)
		{
			return;
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (new Vector3 (0, -rotateSpd * Time.deltaTime, 0));
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (new Vector3 (0, rotateSpd * Time.deltaTime, 0));
		}

		if (Input.GetKey (KeyCode.W)) {
			carVelocity += 20f * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.S)) {
			carVelocity -= 20f * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			thisRb.velocity = transform.forward * carVelocity;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Respawn ();
		}

		textUI.text = carVelocity.ToString ();
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player") {
			collisionNormal = col.contacts [0].normal;
			relativeVel = col.relativeVelocity;
			opponentName = col.gameObject.name;
			opponentMass = col.rigidbody.mass;

			thisRb.AddForce (new Vector3(col.relativeVelocity.x, col.relativeVelocity.y + relativeUpwardModifier, col.relativeVelocity.z) * addForceMultiplier, ForceMode.Impulse);
			Debug.Log ("Amplified");
		}
	}

	void Respawn()
	{
		if (isLocalPlayer)
		{
			thisRb.velocity = Vector3.zero;
			transform.position = originalPos;
			transform.rotation = originalRot;
		}
	}
}
