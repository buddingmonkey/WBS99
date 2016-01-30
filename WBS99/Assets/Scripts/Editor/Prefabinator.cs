using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class Prefabinator {

	[MenuItem("Tools/Prefabinator")]
	private static void Prefabinate() {
		string[] folders = { 
			"Assets/Tiles/3D Road Tiles", 
			"Assets/Tiles/Nature"
		};
		foreach (var folder in folders) {
			string group = Path.GetFileName (folder);
			RunWithFolder (folder, group);
		}
	}

	private static void RunWithFolder(string folder, string group) {
		var info = new DirectoryInfo(folder);

		foreach (FileInfo file in info.GetFiles("*.obj")) {
			string path = AssetPath (file.FullName);
			Object o = AssetDatabase.LoadAssetAtPath<GameObject> (path);
			var go = (GameObject)GameObject.Instantiate (o);
			AddMeshCollider (go, path);
			go.isStatic = true;
			UnityEditor.PrefabUtility.CreatePrefab (PrefabPath(group, file.FullName), go);
			GameObject.DestroyImmediate (go);
		}
	}

	private static void AddMeshCollider(GameObject go, string filename) {
		MeshFilter mf = go.GetComponentInChildren<MeshFilter> ();
		if (mf != null) {
			var mc = go.AddComponent<MeshCollider> ();
			mc.sharedMesh = mf.sharedMesh;
			mc.convex = true;

		} else {
			Debug.LogError ("Mesh Filter not found for " + Path.GetFileNameWithoutExtension (filename));
		}
	}

	private static string AssetPath(string path) {
		return "Assets" + path.Substring (Application.dataPath.Length);
	}

	private static string PrefabPath(string group, string path) {
		string dir = Application.dataPath + "/Tiles/Prefabs/" + group;
		if (!Directory.Exists(dir)) {
			Directory.CreateDirectory (dir);
		}
		return "Assets/Tiles/Prefabs/" + group + "/" + Path.GetFileNameWithoutExtension (path) + ".prefab";
	}
}
