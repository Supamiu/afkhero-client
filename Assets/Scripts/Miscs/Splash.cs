using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
	//Seconds to wait on splash
	int delay = 2;
	//Just a private field to store actual splash end time
	float endTime;

	// Use this for initialization
	void Start () {
		this.endTime = Time.time + delay;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > endTime) {
			OpenMenu ();
		}
	}

	void OpenMenu(){
		SceneManager.LoadScene ("Menu");
	}
}
