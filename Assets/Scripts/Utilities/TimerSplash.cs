using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerSplash : MonoBehaviour {

    float startTimer, delayTimer;

	// Use this for initialization
	void Start () {
        delayTimer = 1.5f;
        startTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (startTimer + delayTimer < Time.time)
            SceneManager.LoadScene("Menu");
	}
}
