using UnityEngine;
using System.Collections;

public class RoadBeef : MonoBehaviour {

    private NavMeshAgent agent;
	private RoadBeefSpawnController spawner;
	private float range = 100;
	private Vector3 point;

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
		spawner = GameObject.Find("BeefSpawn").GetComponent<RoadBeefSpawnController>();
		agent = GetComponent<NavMeshAgent>();

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
		if (other.tag == "Bat") {
			DestroySelf ();

			// tell game controller
			spawner.gameController.HitRoadBeef();
		}
	}

	public void DestroySelf () {
		spawner.DestroyObject (transform);
	}

}
