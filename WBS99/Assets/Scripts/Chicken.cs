﻿using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour {

	[SerializeField]
	private float range = 100;
	GameController gameController;
	private NavMeshAgent agent;
	private Vector3 point;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip squwak;

	[SerializeField]
	private ParticleSystem feathers;

	[SerializeField]
	private GameObject chickenModel;

	//private Spawner spawner;

	bool RandomPoint(Vector3 center, float range, out Vector3 result) {
		for (int i = 0; i < 30; i++) {
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}

	protected bool pathComplete()
	{
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}

		return false;
	}

	void FindNewPoint ()
	{
		while (!RandomPoint (transform.position, range, out point)) {
		}
		//TODO: get rid of this debug
		agent.destination = point;
	}

	// Use this for initialization
	void Start () {
		//spawner = transform.parent.GetComponent<Spawner> ();
		agent = GetComponent<NavMeshAgent>();
		gameController = GameObject.Find ("GameController").GetComponent<GameController>();
		//agent.nextPosition = spawner.transform.position;
		//transform.position = spawner.transform.position;
		FindNewPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (point, Vector3.up, Color.blue, 1.0f);
		if(pathComplete()){
			FindNewPoint();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			DestroySelf ();

			audioSource.PlayOneShot (squwak);

			//feathers.Stop ();
			feathers.loop = false;

			feathers.maxParticles = 1000;

			var em = feathers.emission;
			em.type = ParticleSystemEmissionType.Time;
			em.rate = new ParticleSystem.MinMaxCurve (250);

			em.enabled = true;

			feathers.startSpeed = 10;

			feathers.gravityModifier = .5f;
			//feathers.startLifetime = 1;
			//feathers.Play ();

			// tell game controller
			gameController.GotChicken(gameObject.name);
		}
	}

	public void OnEnable(){
		chickenModel.SetActive (true);
	}

	public void DestroySelf () {
		chickenModel.SetActive (false);
		//spawner.DestroyObject (transform);
		Invoke("Deactivate", 2f);
	}

	private void Deactivate(){
		this.gameObject.SetActive (false);
	}

}


