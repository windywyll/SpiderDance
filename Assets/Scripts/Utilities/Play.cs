using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("LB_0") || Input.GetButtonDown("Start_0") || Input.GetButtonDown("RB_0") || Input.GetButtonDown("X_0") || Input.GetButtonDown("Y_0") || Input.GetButtonDown("B_0") || Input.GetButtonDown("A_0"))
        {
            SceneManager.LoadScene("Level1");
        }


	}
}
