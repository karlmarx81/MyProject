using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AdjustCube))]
[CanEditMultipleObjects]
public class AdjustCubeIns : Editor {

	void OnEnable()
	{		
	}

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button ("Adjust Pos/Rot")) {
			foreach (var obj in Selection.gameObjects) {
				AdjustCube objScript = obj.GetComponent<AdjustCube> ();						
				objScript.ChangePosNRot ();
			}
		}

		if (GUILayout.Button ("Undo Pos/Rot")) {
			foreach (var obj in Selection.gameObjects) {
				AdjustCube objScript = obj.GetComponent<AdjustCube> ();						
				objScript.UndoChangePosNRot ();
			}
		}
	}
}
