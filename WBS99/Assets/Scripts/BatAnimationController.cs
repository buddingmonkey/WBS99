using UnityEngine;
using System.Collections;

public class BatAnimationController : MonoBehaviour {

	public GameObject Bat;

	void EnableBat () {

		Bat.SetActive (true);
	}
	void DisableBat () {

		Bat.SetActive (false);
	}

}
