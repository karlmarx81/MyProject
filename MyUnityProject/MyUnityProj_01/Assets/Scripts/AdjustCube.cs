using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCube : MonoBehaviour {

	public Vector3 lastPos;
	public Quaternion lastRot;

	void OnDrawGizmos()
	{
//		Gizmos.color = Color.red;
//		Gizmos.DrawRay (transform.position, -transform.up);
//
//		Gizmos.color = Color.blue;
//		Ray ray = new Ray (transform.position, transform.up * -1f);
//		RaycastHit hit;
//
//		if (Physics.Raycast (ray, out hit)) {
//			Gizmos.DrawRay (transform.position, hit.normal);
//		}
	}

	public void ChangePosNRot()
	{
		lastPos = transform.position;
		lastRot = transform.rotation;

		transform.position = transform.position + transform.up * 5f;

		Ray ray = new Ray (transform.position, transform.up * -1f);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.CompareTag ("Ground")) {
				Vector3 position = hit.point;
				Vector3 direction = hit.normal;

				transform.position = position;
				transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

				//Debug.Log (transform.name + ", hit Name: " + hit.transform.name + ", hit point: " + hit.point + ",hit normal: " + hit.normal);
			}
		}
	}

	public void UndoChangePosNRot()
	{
		transform.position = lastPos;
		transform.rotation = lastRot;
	}
}
