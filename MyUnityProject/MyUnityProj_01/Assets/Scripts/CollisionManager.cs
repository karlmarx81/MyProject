using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour {

	public GameObject car1;
	public GameObject car2;
	public float car1Velocity;
	public float car2Velocity;
	public float rotateSpd;
	public Text car1text;
	public Text car2text;

	Rigidbody car1rb;
	Rigidbody car2rb;
	Vector3 originalPosOfCar1;
	Vector3 originalPosOfCar2;
	Quaternion originalRotOfCar1;
	Quaternion originalRotOfCar2;

	void Start () {
		car1rb = car1.GetComponent<Rigidbody> ();
		car2rb = car2.GetComponent<Rigidbody> ();

		originalPosOfCar1 = car1.transform.position;
		originalPosOfCar2 = car2.transform.position;
		originalRotOfCar1 = car1.transform.rotation;
		originalRotOfCar2 = car2.transform.rotation;
	}	

	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			car1.transform.Rotate (new Vector3 (0, -rotateSpd * Time.deltaTime, 0));
		}

		if (Input.GetKey (KeyCode.D)) {
			car1.transform.Rotate (new Vector3 (0, rotateSpd * Time.deltaTime, 0));
		}

		if (Input.GetKey (KeyCode.W)) {
			car1Velocity += 20f * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.S)) {
			car1Velocity -= 20f * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			car2.transform.Rotate (new Vector3 (0, -rotateSpd * Time.deltaTime, 0));
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			car2.transform.Rotate (new Vector3 (0, rotateSpd * Time.deltaTime, 0));
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			car2Velocity += 20f * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			car2Velocity -= 20f * Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			car1rb.velocity = car1.transform.forward * car1Velocity;
			car2rb.velocity = car2.transform.forward * car2Velocity;
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			Time.timeScale = 0.1f;
		} else {
			Time.timeScale = 1f;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {			

			car1.transform.position = originalPosOfCar1;
			car2.transform.position = originalPosOfCar2;
			car1.transform.rotation = originalRotOfCar1;
			car2.transform.rotation = originalRotOfCar2;
		}

		UpdateUI ();
	}

	void UpdateUI()
	{
		car1text.text = car1Velocity.ToString ();
		car2text.text = car2Velocity.ToString ();
	}
}
