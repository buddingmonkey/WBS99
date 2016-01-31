using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	[SerializeField]
	private SuperDB superDB;
	private bool starting = false;

	public void OnStartPress() {
		if (starting) {
			return;
		}

		starting = true;
		int cityIndex = Random.Range (0, superDB.cities.Count);
		CityInfo city = superDB.cities [cityIndex];

		GameMetrics.Instance.cityIndex = cityIndex;
		GameMetrics.Instance.cityName = city.cityName;

		SceneManager.LoadSceneAsync (city.cityName);
	}


	public void Update() {
		if (Input.anyKeyDown) {
			OnStartPress ();
		}
	}

}
