using UnityEngine;
using System.Collections;

public class FollowDream : MonoBehaviour {

    [SerializeField]
    private Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = newPos;
    }
}
