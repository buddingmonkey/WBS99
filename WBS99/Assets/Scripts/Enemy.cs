using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private GameObject player;
	private PlayerController playerController;
    private NavMeshAgent agent;
	private EnemySpawner spawner;

    // Use this for initialization
    void Start () {
		spawner = transform.parent.GetComponent<EnemySpawner> ();

   //TODO: Remove FIND!
        player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController>();
 
        if (!player) {
            Debug.Log ("ERROR could not find Player!");
        }
        
          agent = GetComponent<NavMeshAgent>();
    }
   
    // Update is called once per frame
    void Update () {
 
        if (!player) {
            return;
        }

	if(Vector3.Distance(player.transform.position, transform.position) < 1)
	{
		StartCoroutine(playerController.DoDamage());
	}
 
          agent.destination = player.transform.position; 
        // var distance = Vector3.Distance( player.transform.position, transform.position);
 
        // if ( distance < 100  )  
        // {  
        //     //Debug.Log ("player is close");
        //     var delta = player.transform.position - transform.position;
        //     delta.Normalize();
        //     var moveSpeed = speed * Time.deltaTime;
        //     transform.position = transform.position + (delta * moveSpeed);
        //     transform.LookAt(player.transform);
        // }
 
        // else    
        // {
        //     Debug.Log("not close yet " + distance);
        // }
    }


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bat") {
			DestroySelf ();

			// tell game controller
			spawner.gameController.HitEnemy();
		}
	}

	public void DestroySelf () {
		spawner.DestroyObject (transform);
	}

}
