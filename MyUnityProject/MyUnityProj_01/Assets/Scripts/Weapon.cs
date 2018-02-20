using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Projectile projectile;
	public Transform muzzleNode;

	Projectile spawnedProjectile;

	void Start () {
		if (muzzleNode == null) {
			muzzleNode = transform.Find ("Node_Fire");
		}
	}

	void Update () {
		
	}

	public void FireProjectile ()
	{		
		spawnedProjectile = Instantiate (projectile, muzzleNode.position, muzzleNode.rotation) as Projectile;
	}
}
