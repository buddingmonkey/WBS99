using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SuperDB))]
public class SuperDBEditor : Editor {

	public override void OnInspectorGUI() {
		base.DrawDefaultInspector ();
		var superDB = (SuperDB) target;
	}
}
