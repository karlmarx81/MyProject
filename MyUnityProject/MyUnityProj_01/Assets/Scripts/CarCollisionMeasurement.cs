using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarCollisionMeasurement : MonoBehaviour {

	public Color color = Color.red;
	public float rayLength = 3f;
	public Text textUI;
	public float addForceMultiplier;
	public float relativeUpwardModifier;

	Vector3 collisionNormal;
	Vector3 relativeVel;
	float opponentMass;
	string opponentName;

	Rigidbody thisRb;

	void Start () {
		thisRb = this.gameObject.GetComponent<Rigidbody> ();
	}	

	void Update () {
		float scalarValue = (int)Vector3.Dot (collisionNormal, relativeVel);
		//float scalarValue = (int)relativeVel.magnitude;
		float damage = opponentMass * scalarValue;

		if (collisionNormal != Vector3.zero) {
			//Debug.DrawRay (transform.position, collisionNormal * rayLength, color); //collisionNormal Draw
			Debug.DrawRay (transform.position, relativeVel * rayLength, color); //relativeVelocity Draw
			textUI.text = "Force: " + scalarValue.ToString () + "\nName: " + opponentName + "\nDamage: " + damage;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player") {
			collisionNormal = col.contacts [0].normal;
			relativeVel = col.relativeVelocity;
			opponentName = col.gameObject.name;
			opponentMass = col.rigidbody.mass;

			thisRb.AddForce (new Vector3(col.relativeVelocity.x, col.relativeVelocity.y + relativeUpwardModifier, col.relativeVelocity.z) * addForceMultiplier, ForceMode.Impulse);
		}
	}
}
