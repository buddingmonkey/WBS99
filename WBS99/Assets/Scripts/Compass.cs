using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Compass : MonoBehaviour {

	[SerializeField]
	private Color chickenColor;

	[SerializeField]
	private Color stadiumColor;

	public Transform player;

	[SerializeField]
	private Transform arrow;

	private List<GameObject> arrows = new List<GameObject>();
	const int maxArrows = 20;
	const float radius = 0.65f;
	const float yOffset = -0.2f;

	private float startTime;
	private Material mat;
	private Vector3 lastPos;

	private Transform stadium;

	// Use this for initialization
	void Start () {
		stadium = GameObject.Find ("StadiumForTiling").transform;
		if (stadium == null) {
			Debug.Log ("CAN'T FIND StadiumForTiling");
		}
		mat = arrow.GetComponentInChildren<Renderer> ().sharedMaterial;
		startTime = Time.time;
		for (int i=0; i<maxArrows; i++) {
			GameObject a = GameObject.Instantiate (arrow).gameObject;
			a.transform.parent = transform;
			a.SetActive (false);
			arrows.Add (a);
		}
		arrow.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Time.time - startTime > 5) {
			bool moved = (lastPos != player.position);
			Color c = mat.color;
			c.a = Mathf.Lerp (c.a, moved ? 0 : 1, 0.1f);
			mat.color = c;

			transform.position = player.position;

			PointAtChickens ();
		}
		lastPos = player.position;
	}

	void PointAtChickens() {
		Vector3 pos = transform.position;
		int i = 0;
		foreach (var chick in GameObject.FindGameObjectsWithTag ("Chicken")) {
			Vector3 dir = chick.transform.position - pos;
			GameObject a = arrows [i];
			var p = pos + (dir.normalized * radius);
			a.transform.position = new Vector3(p.x, p.y + yOffset, p.z);
			float angle = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg + 90;
			a.transform.rotation = Quaternion.AngleAxis (angle, Vector3.up);
			a.SetActive (true);
			i++;
		}

		if (i == 0) {
			PointAtStadium ();
			return;
		}

		for (; i < arrows.Count; i++) {
			arrows [i].SetActive (false);
		}

		Color c = mat.color;
		c.r = chickenColor.r;
		c.g = chickenColor.g;
		c.b = chickenColor.b;
		mat.color = c;
	}

	void PointAtStadium() {
		Vector3 pos = transform.position;
		Vector3 dir = stadium.transform.position - pos;
		GameObject a = arrows [0];
		var p = pos + (dir.normalized * radius);
		a.transform.position = new Vector3(p.x, p.y + yOffset, p.z);
		float angle = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg + 90;
		a.transform.rotation = Quaternion.AngleAxis (angle, Vector3.up);
		a.SetActive (true);

		for (int i = 1; i < arrows.Count; i++) {
			arrows [i].SetActive (false);
		}

		Color c = mat.color;
		c.r = stadiumColor.r;
		c.g = stadiumColor.g;
		c.b = stadiumColor.b;
		mat.color = c;
	}
}
