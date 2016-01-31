using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BuildingXRay : MonoBehaviour {
	private GameController gameController;
	private List<Renderer> whatsInTheWay = new List<Renderer>();
	private List<Renderer> whatWasInTheWay = new List<Renderer>();

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastPlayer ();
	}

	void RaycastPlayer() {
		var player = gameController.GetPlayer();
		if (player == null) {
			return;
		}

		Camera camera = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();

		var dir = player.transform.position - camera.transform.position;
		float dist = dir.magnitude;
		foreach (RaycastHit info in Physics.RaycastAll(camera.transform.position, dir, dist)) {
			if (info.collider.name.Length > 8 && info.collider.name.Substring (0, 8) == "Building") {
				var r = info.collider.transform.GetComponent<MeshRenderer> ();
				whatsInTheWay.Add (r);
				if (!whatWasInTheWay.Exists( obj => obj == r)) {
					transparentize (r);
				}
			}
		}
		foreach (var r in whatWasInTheWay.Except(whatsInTheWay)) {
			unTransparentize (r);
		}
		List<Renderer> t = whatWasInTheWay;
		t.Clear ();
		whatWasInTheWay = whatsInTheWay;
		whatsInTheWay = t;
	}

	private void transparentize(Renderer r) {
		Material mat = r.materials [0];
		Color c = mat.color;
		c.a = 0.15f;
		mat.color = c;

		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		mat.SetInt("_ZWrite", 0);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.EnableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = 3000;
	}

	private void unTransparentize(Renderer r) {
		Material mat = r.materials [0];
		Color c = mat.color;
		c.a = 1;
		mat.color = c;

		mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
		mat.SetInt("_ZWrite", 1);
		mat.DisableKeyword("_ALPHATEST_ON");
		mat.DisableKeyword("_ALPHABLEND_ON");
		mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		mat.renderQueue = -1;

	}
}
