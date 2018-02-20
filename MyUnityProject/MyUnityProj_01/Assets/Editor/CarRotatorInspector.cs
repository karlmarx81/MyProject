using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CarRotator))]
public class CarRotatorInspector : Editor {

	public override void OnInspectorGUI()
	{
		base.DrawDefaultInspector ();

		CarRotator myScript = (CarRotator)target;
		Vector3 myCoord = new Vector3 (myScript.transform.rotation.eulerAngles.x, myScript.transform.rotation.eulerAngles.y, myScript.transform.rotation.eulerAngles.z);
		EditorGUILayout.Vector3Field ("Local Rotation", myCoord);

	}
}
