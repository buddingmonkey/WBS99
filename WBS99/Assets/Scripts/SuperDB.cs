using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CityInfo{
	public string cityName;
	public string superTitle;
	public List<Superstition> super;
}

public class SuperDB : ScriptableObject {
	public List<CityInfo> cities = new List<CityInfo>();
}
