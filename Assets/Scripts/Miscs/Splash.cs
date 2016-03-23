using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
	//Seconds to wait on splash
	[Header("Splash duration")]
	public float duration = 2;

	// Use this for initialization
	void Start () {
		Invoke ("OpenMenu", (float)duration);
	}

	void OpenMenu(){
		SceneManager.LoadScene ("Menu");
	}
}
