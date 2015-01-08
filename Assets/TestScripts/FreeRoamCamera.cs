using UnityEngine;
using System.Collections;


public class FreeRoamCamera : MonoBehaviour {

	Vector3 rotateDragStart;
	float rotateSpeed = .2f;
	float translateSpeed = 3;

	public enum Button : int { Left = 0, Right = 1, Middle = 2, None = 3 }

	float xRotation = 0;
	float yRotation = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown((int)Button.Right)) {
			rotateDragStart = Input.mousePosition;
		}

		if(Input.GetMouseButton((int)Button.Right)) {
			xRotation = Input.mousePosition.x - rotateDragStart.x;
			yRotation = Input.mousePosition.y - rotateDragStart.y;
		} else {
			xRotation = 0;
			yRotation = 0;
		}


		transform.localRotation *= Quaternion.AngleAxis(xRotation*rotateSpeed*Time.deltaTime, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(yRotation*rotateSpeed*Time.deltaTime, Vector3.left);
		transform.localRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
		
		transform.position += transform.forward*translateSpeed*Input.GetAxis("Vertical")*Time.deltaTime;
		transform.position += transform.right*translateSpeed*Input.GetAxis("Horizontal")*Time.deltaTime;

		transform.position += new Vector3(0,Input.GetAxis("Mouse ScrollWheel")*translateSpeed,0);
	}
}
