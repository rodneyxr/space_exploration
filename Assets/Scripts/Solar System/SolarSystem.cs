using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolarSystem : MonoBehaviour {

	private Planet[] planets;

	void Start() {
		planets = FindObjectsOfType<Planet> ();
	}
	
	void Update () {
	
	}

}
