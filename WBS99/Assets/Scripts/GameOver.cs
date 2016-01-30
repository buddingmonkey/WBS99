using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	[SerializeField]
	private Text stats;

	void Start(){
		stats.text = string.Format ("{0}\n{1}\n{2}\n{3:.000}", 
			GameMetrics.Instance.totalAtBats, 
			GameMetrics.Instance.totalHits,
			GameMetrics.Instance.totalAtBats - GameMetrics.Instance.totalHits,
			GameMetrics.Instance.battingAverage);
	}
}
