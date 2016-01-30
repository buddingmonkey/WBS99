using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState{
	Flying,
	Baseball,
	City,
	Pausied,
	GameOver
}

public class GameController : MonoBehaviour {

	[SerializeField]
	private Spawner playerSpawner;

	private Transform player;

	[SerializeField]
	private List<Spawner> enemySpawners;

	public GameState gameState { get; private set; }

	// Use this for initialization
	void Start () {
		gameState = GameState.Flying;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GameState.Flying) {
			gameState = GameState.City;
			SpawnPlayer ();
		}
	}

	private void SpawnPlayer(){
		player = playerSpawner.Spawn ();
	}

	public void PlayBall(){
		// Do some baseball shit in here
		playerSpawner.DestroyObject(player);
		Debug.Log("BASEBALL!");
		gameState = GameState.Flying;
	}
}
