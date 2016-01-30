using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int health = 5;
	public float speed = 6.0F;
	public float gravity = 20.0F;
	bool invul;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

	public Transform cameraTransform;

	void OnEnable(){
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
	}

	void Update() 
	{
		// Character is on ground (built-in functionality of Character Controller)
		if (controller.isGrounded) 
		{
			Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
			forward.y = 0f;
			forward = forward.normalized;
			Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);


			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");

			// Use input up and down for direction, multiplied by speed
			moveDirection = (h * right + v * forward).normalized;
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}
		// Apply gravity manually.
		moveDirection.y -= gravity * Time.deltaTime;
		// Move Character Controller
		controller.Move(moveDirection * Time.deltaTime);
	}


	public IEnumerator DoDamage()
	{
		if(!invul)
		{
			health -= 1;
		}
		else
		{
			return false;
		}
		//make invulnerable for 1 second
		invul = true;
		yield return new WaitForSeconds(1);
		invul = false;
	}
}