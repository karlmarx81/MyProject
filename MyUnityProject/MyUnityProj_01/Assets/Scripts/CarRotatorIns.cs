using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CarRotator))]
public class CarRotatorIns : Editor {

	CarRotator myScript;

	void OnEnable()
	{
		myScript = (CarRotator)target;
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
		EditorGUILayout.LabelField ("is facing ground", myScript.isFacingGround.ToString());
	}
}
