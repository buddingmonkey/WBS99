﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;
using System.Linq;

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

	[SerializeField]
	private List<Spawner> collectibleSpawners;

	[SerializeField]
	private BoggsFollow boggsFollow;

	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private SuperDB superDB;

	[SerializeField]
	private Transform resultsPanelPrefab;
	[SerializeField]
	private ResultsPanel resultsPanel;

	[SerializeField]
	private Transform hudPrefab;
	[SerializeField]
	private Hud hud;

	public GameState gameState { get; private set; }

	public float gameTime { get; private set; }

	private Round currentRound;

	// Use this for initialization
	void Start () {
		if (hud == null) {
			hud = Instantiate (hudPrefab).GetComponentInChildren<Hud>();
			hud.gameController = this;
		}
		if (resultsPanel == null) {
			resultsPanel = Instantiate (resultsPanelPrefab).GetComponentInChildren<ResultsPanel>();
			resultsPanel.gameController = this;
		}
		StartRound ();
		mainCamera.GetComponent<AudioSource> ().enabled = false;
	}

	void StartRound(){
		gameState = GameState.Flying;
		var superstitions = GameMetrics.Instance.GetSuperstitions ();
		if (superstitions.Count == 0) {
			superstitions.Add (Superstition.CreateChicken (1));
		}

		currentRound = new Round ();

		/* Example code for adding a supersitition
		var s = new Superstition ();
		s.maxValue = 2;
		s.value = 1;
		s.score = .5f;
		s.weight = .5f;

		currentRound.superstitions.Add (s);
		*/

		resultsPanel.generateSuperstitionsList (IntroClosed);
		SpawnPlayer ();
	}

	void IntroClosed() {
		var cam = GameObject.Find ("StartCamera");
		if (cam != null) {
			cam.SetActive (false);
		}
		mainCamera.GetComponent<AudioSource> ().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GameState.Flying) {
			gameState = GameState.City;
		} else if (gameState == GameState.City) {
			gameTime += Time.deltaTime;
		}
	}

	public Round GetCurrentRound() {
		return currentRound;
	}

	private void SpawnPlayer(){
		gameState = GameState.City;

		player = playerSpawner.Spawn ();
		player.GetComponent<PlayerController> ().cameraTransform = mainCamera.transform;
		boggsFollow.enabled = true;
		boggsFollow.target = player;
	}

	public Transform GetPlayer() {
		return player;
	}

	public void PlayBall(){
		// Do some baseball shit in here
		Debug.Log("BASEBALL!");
		currentRound.PlayBall ();

		boggsFollow.enabled = false;

		playerSpawner.DestroyObject(player);

		if (GameMetrics.Instance.totalHits > GameConstants.winningHits) {
			Debug.Log ("Winner");
		} else if (GameMetrics.Instance.battingAverage < GameConstants.losingAverage && GameMetrics.Instance.battingAverage > .005f) {
			SceneManager.LoadSceneAsync ("GameOver");
		} else {
			resultsPanel.generateRoundResults (currentRound, GenerateSupersititions);
		}
	}

	public void GotChicken(string name) {
		currentRound.numChickens++;
		currentRound.lastChicken = gameTime;
		Debug.Log ("Got a Chicken");
	}

	public void GotBaseball(string name) {
		currentRound.numBalls++;
		currentRound.lastBall = gameTime;
		Debug.Log ("Got a Baseball");
	}

	public void GotLetter(char letter) {
		currentRound.letters += letter;
		currentRound.lastLetter = gameTime;
		Debug.LogFormat ("Got a Letter {0}", letter);
	}

	public void GotBeer(string name) {
		currentRound.numBeers++;
		currentRound.lastBeer = gameTime;
		Debug.Log ("Got a Beer");
	}

	public void HitEnemy() {
		currentRound.numEnemies++;
		currentRound.lastEnemy = gameTime;
		Debug.Log ("Hit a Enemy");
	}

	public void HitRoadBeef() {
		currentRound.numCows++;
		currentRound.lastCow = gameTime;
		Debug.Log ("Hit DAT ROAD BEEF BOIIIIIIII!!!!");
	}

	public void GenerateSupersititions(){
		var round = GameMetrics.Instance.lastRound;
		if (round == null) {
			Debug.LogError ("No last round logged! Probably called this function before PlayBall()");
			return;
		}
		int nSupers = round.superstitions.Count;
		if (round.superstitions.Count < 3) {
			// first time? add 3
			GenerateSuperstition(round, GameMetrics.Instance.GetSuperstitions ());
			GenerateSuperstition(round, GameMetrics.Instance.GetSuperstitions ());
			GenerateSuperstition(round, GameMetrics.Instance.GetSuperstitions ());
		} else if (GameMetrics.Instance.gamesPlayedInCity == 1) {
			// returned to city from another? add 1
			GenerateSuperstition(round, GameMetrics.Instance.GetSuperstitions ());
		}
		resultsPanel.generateNewSupersitionsPage (nSupers, NextRound);

	}

	private void GenerateSuperstition(Round round, List<Superstition> superstitions) {
		CityInfo city = superDB.cities [GameMetrics.Instance.cityIndex];
		List<Superstition> current = GameMetrics.Instance.GetSuperstitions ();
		var unused = city.super.Where ( s => !current.Any(s2 => s2.type == s.type && s2.metric == s.metric));
		if (unused.Count() == 0)
			return;

		Superstition super = unused.ElementAt(Random.Range (0, unused.Count()));
		switch (super.type) {
		case Superstition.Type.Chicken:
			if (super.metric == SuperMetrics.pickup) {
				super.maxValue = 1;
				super.checkValue = round.numChickens % 10;
			} else {
				// set time
			}
			break;
		case Superstition.Type.Beer:
			if (super.metric == SuperMetrics.pickup) {
				super.maxValue = 1;
				super.checkValue = round.numBeers % 10;
			} else {
				// set time
			}
			break;
		case Superstition.Type.Baseballs:
			if (super.metric == SuperMetrics.pickup) {
				super.maxValue = 1;
				super.checkValue = round.numBalls % 10;
			} else {
				// set time
			}
			break;
		case Superstition.Type.Cows:
			if (super.metric == SuperMetrics.pickup) {
				super.maxValue = 1;
				super.checkValue = round.numCows;
			} else {
				// set time
			}
			break;
		case Superstition.Type.Time:
			// TODO add time superstitions
			return;

			super.maxValue = 1;
			super.checkValue = Mathf.RoundToInt (round.timeToStadium) % 10;
			break;
		case Superstition.Type.Letters:
			// nothing to do
			// TODO support letters superstition
			return;
		}

		current.Add (super);
	}

	public void NextRound() {
		
		GameMetrics.Instance.gamesPlayedInCity++;
		if (GameMetrics.Instance.gamesPlayedInCity >= GameConstants.gamesPerCity) {
			GameMetrics.Instance.cityIndex = (GameMetrics.Instance.cityIndex + 1) % superDB.cities.Count;
		}
		string name = superDB.cities [GameMetrics.Instance.cityIndex].cityName;
		GameMetrics.Instance.cityName = name;
		Prime31.ZestKit.ZestKit.instance.stopAllTweens();
		SceneManager.LoadSceneAsync (name);
	}
}
