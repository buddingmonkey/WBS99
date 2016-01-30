using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class Prefabinator {

	[MenuItem("Tools/Prefabinator")]
	private static void Prefabinate() {
		string[] folders = { 
			//			"Assets/Tiles/3D Road Tiles", 
			//			"Assets/Tiles/Nature"
			"Assets/Tiles/SimpleTown/Prefabs/Buildings"
		};
		foreach (var folder in folders) {
			string group = Path.GetFileName (folder);
			RunWithFolder (folder, group);
		}
	}

	[MenuItem("Tools/Prefabinator - Buildings")]
	private static void PrefabinateBuildings() {
		string[] folders = { 
			"Assets/Tiles/SimpleTown/Prefabs/Buildings"
		};
		foreach (var folder in folders) {
			string group = Path.GetFileName (folder);
			var info = new DirectoryInfo(folder);

			foreach (FileInfo file in info.GetFiles("*.prefab")) {
				string path = AssetPath (file.FullName);

				GameObject baseObj;
				if (Path.GetFileName (path).IndexOf ("House") > 0  || Path.GetFileName (path).IndexOf ("Garage") > 0) {
					baseObj= LoadHouseBase();
				} else {
					baseObj= LoadBuildingBase();
					continue;
				}

				Object o = AssetDatabase.LoadAssetAtPath<GameObject> (path);
				var go = (GameObject)GameObject.Instantiate (o);
				go.AddComponent<BoxCollider> ();
				go.transform.position = new Vector3 (-1.5f, 0.62f, -1.5f);
				go.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				go.transform.parent = baseObj.transform;

				baseObj.name = Path.GetFileNameWithoutExtension (file.Name);
				baseObj.tag = "Building";
				UnityEditor.PrefabUtility.CreatePrefab (PrefabPath(group, file.FullName), baseObj);
				GameObject.DestroyImmediate (baseObj);
			}
		}
	}

	[MenuItem("Tools/Prefabinator - Vehicles")]
	private static void PrefabinateVehicles() {
		string[] folders = { 
			"Assets/Tiles/SimpleTown/Prefabs/Vehicles"
		};
		foreach (var folder in folders) {
			string group = Path.GetFileName (folder);
			var info = new DirectoryInfo(folder);

			foreach (FileInfo file in info.GetFiles("*.prefab")) {
				string path = AssetPath (file.FullName);

				if (Path.GetFileName (path).IndexOf ("seperate") > 0) {
					continue;
				}

				Object o = AssetDatabase.LoadAssetAtPath<GameObject> (path);
				var go = (GameObject)GameObject.Instantiate (o);
				go.AddComponent<BoxCollider> ();
				go.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				go.tag = "Car";

				UnityEditor.PrefabUtility.CreatePrefab (PrefabPath(group, file.FullName), go);
				GameObject.DestroyImmediate (go);
			}
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

	private static GameObject LoadBuildingBase() {
		Object o = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Tiles/Prefabs/3D Road Tiles/roadTile_002.prefab");
		return (GameObject)GameObject.Instantiate (o);
	}

	private static GameObject LoadHouseBase() {
		Object o = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Tiles/Prefabs/3D Road Tiles/roadTile_163.prefab");
		return (GameObject)GameObject.Instantiate (o);
	}
}
