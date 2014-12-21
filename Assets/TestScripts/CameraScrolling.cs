using UnityEngine;
using System.Collections;

public class CameraScrolling : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vec = transform.position;
		vec.y += Input.GetAxis ("Mouse ScrollWheel") * -5;
		transform.position = vec;
	}
}
