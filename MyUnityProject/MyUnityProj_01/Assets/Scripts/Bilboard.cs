using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		//transform.LookAt (Camera.main.transform);
		transform.rotation = Camera.main.transform.rotation;
	}
}
