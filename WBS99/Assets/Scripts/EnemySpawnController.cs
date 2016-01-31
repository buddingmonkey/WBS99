using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnController : MonoBehaviour {

	public GameObject Enemy;
	private GameObject player;
	int poolSize = 5;
	private Stack<Transform> enemyPool = new Stack<Transform> ();
	List<Vector3> spawnLocations = new List<Vector3>();

	public GameController gameController;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();

		GameObject[] spawnGOs = GameObject.FindGameObjectsWithTag("EnemySpawnLocation");
		for(int i = 0; i< spawnGOs.Length; i++)
		{
			//Debug.Log (spawnGOs[i].transform.position);
			spawnLocations.Add(spawnGOs[i].transform.position);
		}

		Vector3 locationOfSpawner = FindClosestSpawnLocation();
		for (int i = 0; i < poolSize; i++)
		{
			InstantiateObject (locationOfSpawner);
		}
	}

	public Transform Spawn(){
//		if (enemyPool.Count < 1) {
//			InstantiateObject(locationOfSpawner);
//		}

		Vector3 locationOfSpawner = FindClosestSpawnLocation();
		var t = enemyPool.Pop ();
		t.rotation = this.transform.rotation;
		t.position = locationOfSpawner;
		t.gameObject.SetActive (true);

		return t;
	}
	public void DestroyObject(Transform t){
		t.gameObject.SetActive (false);
		enemyPool.Push (t);
		StartCoroutine(WaitThenCreateNewEnemy());
	}

	private Transform InstantiateObject(Vector3 locationOfSpawner){
		Transform t = Instantiate (Enemy.transform);
		t.parent = this.transform;
		t.position = locationOfSpawner;
		//Debug.Log (t.position);
		//enemyPool.Push (t);
		return t;
	}

		IEnumerator WaitThenCreateNewEnemy()
		{
			yield return new WaitForSeconds(2);
			Transform t = Spawn();
			t.gameObject.SetActive(true);
		}


	private Vector3 FindClosestSpawnLocation(){
		float tempDistance;
		float maxDistance = 10000;
		Vector3 closestSpawnerLocation = new Vector3();

//		Debug.Log (closestSpawnerLocation);
//		Debug.Log (spawnLocations.Count);
		for(int i = 0; i < spawnLocations.Count; i++)
		{
			//Debug.Log ("ASKLDJLAKS");
			tempDistance = Vector3.Distance(player.transform.position, spawnLocations[i]);
			if(tempDistance < maxDistance)
			{
				maxDistance = tempDistance;
				closestSpawnerLocation = spawnLocations[i];
			}
		}
		//Debug.Log (closestSpawnerLocation);
		return closestSpawnerLocation;
	}

}
