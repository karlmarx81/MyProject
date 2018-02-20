using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	[System.Serializable]
	public struct WeaponInfo
	{
		public Transform attachmentNode;
		public Weapon weapon;
	}

	public WeaponInfo weaponInfos;

	Weapon spawnedWeapon;

	void Start () {
		Initialize (weaponInfos.weapon, weaponInfos.attachmentNode);
	}

	void Update () {
		GetFireInput ();
	}

	void Initialize (Weapon weapons, Transform attachNode)
	{
		spawnedWeapon = Instantiate (weapons, attachNode.position, attachNode.rotation) as Weapon;
		spawnedWeapon.transform.parent = weaponInfos.attachmentNode;
	}

	void GetFireInput ()
	{
		if (Input.GetButton ("Fire1")) {
			spawnedWeapon.FireProjectile ();
		}
	}
}
