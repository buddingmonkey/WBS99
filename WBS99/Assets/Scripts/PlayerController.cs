using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	int health = 5;
	public GameObject bat;
	Animator batAnimator;
	public float speed = 6.0F;
	public float gravity = 20.0F;
	bool invulnerable;

	private float idleTime = 0;

	private float rotationSpeed = 10f;

	[SerializeField]
	private Transform wadeModel;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	public Animator animator;

	public Transform cameraTransform;

	[SerializeField]
	private AudioSource footsteps;

	[SerializeField]
	private Transform compasPrefab;
	private Compass compass;

	void Start() {
		// add compass
		GameObject.Instantiate(compasPrefab).GetComponent<Compass>().player = transform;
	}

	void OnEnable(){
		// Store reference to attached component
		controller = GetComponent<CharacterController>();
		batAnimator = bat.GetComponent<Animator>();
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

		if(Input.GetButtonDown("swing"))
		{
			StartCoroutine(SwingBat());
		}

		if (moveDirection.sqrMagnitude > 1f) {
			wadeModel.rotation = Quaternion.Slerp (
				wadeModel.rotation,
				Quaternion.LookRotation (moveDirection),
				Time.deltaTime * rotationSpeed
			);

			wadeModel.rotation = Quaternion.Euler (0, wadeModel.eulerAngles.y, wadeModel.eulerAngles.z);
			idleTime += 0;
			if (!footsteps.isPlaying) {
				footsteps.Play ();
			}
		} else {
			if (footsteps.isPlaying) {
				footsteps.Stop ();
			}
			idleTime += Time.deltaTime;
		}

		animator.SetFloat ("velocity", moveDirection.sqrMagnitude);
		animator.SetFloat ("idleTime", idleTime);
	}

	IEnumerator SwingBat()
	{

	//bat.SetActive(true);
	//batAnimator.Play("Swing");
		animator.Play ("baseball_hit_Mirrored");
		//return;
	yield return new WaitForSeconds(0.1f);
//	whil
//	yield return new WaitForSeconds(batAnimator.)
//	batAnimator.Stop();
//	bat.SetActive(false);
	}

	public IEnumerator DoDamage()
	{
		if(!invulnerable)
		{
			health -= 1;
		}
		else
		{
			return false;
		}
		//make invulnerable for 1 second
		invulnerable = true;
		yield return new WaitForSeconds(1);
		invulnerable = false;
	}

}