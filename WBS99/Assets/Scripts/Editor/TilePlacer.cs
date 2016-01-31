using UnityEngine;
using UnityEditor;
using System.Collections;

public class TilePlacerPopup : EditorWindow {
	public Transform tile;
	public GameObject grassTile;
	bool isEditing = false;
	public int index = 0;
	const int sizeOfArray = 50;
	GameObject Level;
	Transform[,] tiles = new Transform[sizeOfArray,sizeOfArray]; 

	[MenuItem("Tools/TilePlacer")]

	static void Init() {
		EditorWindow window = GetWindow(typeof(TilePlacerPopup));
		window.Show();
	}

	void OnEnable(){
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}

	void OnDisable(){
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
	}


	void OnGUI() {

		tile = (Transform)EditorGUILayout.ObjectField("Brush",tile, typeof(Transform), false);

		isEditing = GUILayout.Toggle(isEditing, "EditMode");
		if(GUILayout.Button("GenerateLevel"))
		{
			GenerateNewLevel();
		}
	}



	void GenerateNewLevel()
	{

		Object o = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Tiles/Prefabs/3D Road Tiles/roadTile_163.prefab");
		Level = new GameObject("Level");

		Vector3 pos = new Vector3 (0, 0, 0);
		GameObject temptile;
		for (int i = 0; i < sizeOfArray; i++)
		{
			for (int j = 0; j < sizeOfArray; j++)
			{
				temptile = PrefabUtility.InstantiatePrefab (o) as GameObject;
				temptile.transform.position = pos; 
				tiles[i,j] = temptile.transform;
				pos = new Vector3 (pos.x+3, pos.y,pos.z);
				temptile.transform.parent = Level.transform;
			}
			pos = new Vector3 (0, pos.y,pos.z+3);
		}
	}


	void OnSceneGUI(SceneView sceneView)
	{
		if(isEditing && tile != null)
		{
			if(Event.current.type == EventType.Layout)
			{
				HandleUtility.AddDefaultControl(0);
			}
			if(Event.current.type == EventType.MouseDown)
			{
				Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);					

				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
				{
					Vector3 newTilePosition = hit.point;
					if(hit.collider.gameObject.tag == "Tile" ||hit.collider.gameObject.tag == "Building")
					{
						if (tile.tag == "Tile" || tile.tag == "Building")
						{
							Vector3 pos = hit.collider.transform.position;
							Quaternion rot = hit.collider.transform.rotation;
							if (tile.transform.GetChild (0).localScale != Vector3.one)
							{
								pos = new Vector3 (pos.x+ (3*(tile.transform.GetChild (0).localScale.x-1)), pos.y, pos.z+(3*(tile.transform.GetChild (0).localScale.z-1)));
								int xToArray = (int)hit.collider.transform.position.x / 3;
								int zToArray = (int)hit.collider.transform.position.z / 3;
								for (int i = 0; i < tile.transform.GetChild (0).localScale.x; i++)
								{				
									for (int j = 0; j < tile.transform.GetChild (0).localScale.z; j++)
									{
										DestroyImmediate (tiles [zToArray + j, xToArray+i].gameObject);
									}
								}
							}
							else
							{
								DestroyImmediate (hit.collider.gameObject);
							}
							Transform go;
							go = (Transform)PrefabUtility.InstantiatePrefab (tile);
							go.transform.position = pos; 
							if (Level == null)
							{
								Level = GameObject.Find ("Level");
							}
							go.transform.parent = Level.transform; 
							go.transform.rotation = rot;
						} else if (tile.tag == "Collectable" || tile.tag == "Nature")
						{
							Vector3 pos = hit.collider.transform.position;
							pos = new Vector3 (pos.x+1.5f, pos.y + 1, pos.z+1.5f);
							Quaternion rot = hit.collider.transform.rotation;
							Transform go;
							go = (Transform)PrefabUtility.InstantiatePrefab (tile);
							go.transform.position = pos; 
							go.transform.rotation = rot;

						} else
						{
							Debug.LogWarning ("WHAT ARE YOU DOING");
						}
					}
				}
			}	
		}
	}

}
//public class TilePlacer : EditorWindow
//
//{    
//	string myString = "Hello World";
//	bool groupEnabled;
//	bool myBool = true;
//	float myFloat = 1.23f;
//
//	// Add menu item named "My Window" to the Window menu
//	[MenuItem("Window/TilePlacer")]
//	public static void ShowWindow()
//	{
//	//Show existing window instance. If one doesn't exist, make one.
//	EditorWindow.GetWindow(typeof(TilePlacer));
//	}
//
//	void OnGUI()
//	{
//	GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
//	myString = EditorGUILayout.TextField ("Text Field", myString);
//
//	groupEnabled = EditorGUILayout.BeginToggleGroup ("EditMode", groupEnabled);
//	int selected = 0;
//	string[] options = new string[]
//	{
//	"Option1", "Option2", "Option3", 
//	};
//	selected = EditorGUILayout.Popup("Prefab", selected, options); 
//	//myBool = EditorGUILayout.Toggle ("Toggle", myBool);
//	//myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
//	EditorGUILayout.EndToggleGroup ();
//	}
//}
