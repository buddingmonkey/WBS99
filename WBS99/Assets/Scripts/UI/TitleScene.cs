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
		GameMetrics.Instance.cityIndex = cityIndex;

		CityInfo city = superDB.cities [cityIndex];
		SceneManager.LoadSceneAsync (city.cityName);
	}
}
