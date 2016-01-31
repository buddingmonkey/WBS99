using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	[SerializeField]
	private Transform objectPrefab;

	[SerializeField]
	private GameObject player;

	public List<Vector3> spawnLocations = new List<Vector3>();

	[SerializeField]
	private int poolSize = 5;

	private Stack<Transform> enemyPool = new Stack<Transform> ();

	public GameController gameController { get; set; }

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		GameObject[] tmp = GameObject.FindGameObjectsWithTag("EnemySpawnLocation");
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
		//spawnLocations.Clear();
//		Debug.Log (spawnLocations.Count);
//		for(int i = 0; i< tmp.Length; i++)
//		{
//			Debug.Log (tmp[i].transform.position);
//			spawnLocations.Add(tmp[i].transform.position);
//		}
//		Debug.Log (spawnLocations.Count);
//		Vector3 locationOfSpawner = FindClosestSpawnLocation();
//		Debug.Log (locationOfSpawner);
		for (int i = 0; i < poolSize; i++) {
			//Spawn (transform.position);
			InstantiateObject();
		}
	}
 	
	private Vector3 FindClosestSpawnLocation(){
		float tempDistance;
		float maxDistance = 10000;
		Vector3 closestSpawnerLocation = new Vector3();

		Debug.Log (closestSpawnerLocation);
		Debug.Log (spawnLocations.Count);
		for(int i = 0; i < spawnLocations.Count; i++)
		{
			Debug.Log ("ASKLDJLAKS");
			tempDistance = Vector3.Distance(player.transform.position, spawnLocations[i]);
			if(tempDistance < maxDistance)
			{
				maxDistance = tempDistance;
				closestSpawnerLocation = spawnLocations[i];
			}
		}
		Debug.Log (closestSpawnerLocation);
		return closestSpawnerLocation;
	}

	public Transform Spawn(Vector3 locationOfSpawner){
		if (enemyPool.Count < 1) {
			//InstantiateObject(locationOfSpawner);
		}

		var t = enemyPool.Pop ();
		t.position = locationOfSpawner;
		Debug.Log (locationOfSpawner, t.gameObject);
		Debug.Log (t.position, t.gameObject);
		//t.position = RandomLocationNearSpawner(locationOfSpawner);
		t.rotation = this.transform.rotation;
		t.position = this.transform.position;

		return t;
	}

	public void DestroyObject(Transform t){
//		t.gameObject.SetActive (false);
//		enemyPool.Push (t);
//		StartCoroutine(WaitThenCreateNewEnemy());
	}


	private Transform InstantiateObject(){
		Transform t = Instantiate (objectPrefab);
		t.parent = this.transform;
		enemyPool.Push (t);
		return t;
	}
//
//	private Transform InstantiateObject(Vector3 locationOfSpawner){
//		Transform t = Instantiate (objectPrefab);
//
//		//t.parent = this.transform;
//		enemyPool.Push (t);
//		return t;
//	}

//	private Vector3 RandomLocationNearSpawner(Vector3 locationOfSpawner)
//	{
//		locationOfSpawner =	locationOfSpawner+(Random.insideUnitSphere * 10);
//		locationOfSpawner = new Vector3(locationOfSpawner.x, 1, locationOfSpawner.z);
//		return locationOfSpawner;
//	}

//	IEnumerator WaitThenCreateNewEnemy()
//	{
//		yield return new WaitForSeconds(2);
//		Vector3 locationOfSpawner = FindClosestSpawnLocation();
//		Transform t = Spawn(locationOfSpawner);
//		t.gameObject.SetActive(true);
//	}
}
