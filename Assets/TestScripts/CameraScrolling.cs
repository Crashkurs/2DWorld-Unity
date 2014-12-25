using UnityEngine;
using System.Collections;

public class CameraScrolling : MonoBehaviour {

	public GameObject target;
	
	void Start() {
	}
	
	void LateUpdate() {
		transform.position = target.transform.position + -2 * target.transform.rotation.eulerAngles.normalized;
		transform.rotation = target.transform.rotation;
		
		//transform.LookAt(target.transform);
	}
}
