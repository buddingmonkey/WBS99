using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private GameObject player;
    private NavMeshAgent agent;
    public float speed = 1.0f;
 
    // Use this for initialization
    void Start () {
   
   //TODO: Remove FIND!
        player = GameObject.FindGameObjectWithTag ("Player");
 
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
}
