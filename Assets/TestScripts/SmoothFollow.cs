using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
	// The target we are following
	public Transform target;

	// The distance in the x-z plane to the target
	public float distance = 5f;

	// the height we want the camera to be above the target
	public float height = 1f;

	public float zoomSpeed = 1f;

	public float maxZoom = 1.5f;
	public float minZoom = 0.5f;


	float rotationHeight = 1f;
	float heightDamping = 2.0f;
	float rotationDamping = 3.0f;

	private Camera cameraScript;

	void Start()
	{
		cameraScript = GetComponent<Camera> ();
	}

	void LateUpdate () {
		// Early out if we don't have a target
		if (!target)
			return;

		//zoom
		float zoom = Input.GetAxis ("Mouse ScrollWheel");
		if((zoom * zoomSpeed * 10f * Time.deltaTime > 0 && cameraScript.orthographicSize > minZoom) || (zoom * zoomSpeed * 10f * Time.deltaTime < 0 && cameraScript.orthographicSize < maxZoom))
			cameraScript.orthographicSize -= zoom * zoomSpeed * 10f * Time.deltaTime;

		if (Input.GetMouseButton (1)) 
		{
			float v = Input.GetAxis ("Mouse X");
			float h = Input.GetAxis ("Mouse Y");
			transform.RotateAround(target.transform.position, Vector3.up, v * 10f);
			transform.RotateAround(target.transform.position, target.transform.forward, h * 10f);
			/*if(target.transform.position.z > transform.position.z)
			{
			}else{
				transform.RotateAround(target.transform.position, -target.transform.forward, h * 10f);
			}*/
			//transform.position += target.GetComponent<Movement>().LastMovement;
			transform.LookAt (target);
		} else {
			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;
			
			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			
			// Set the height of the camera
			Vector3 pos = transform.position;
			pos.y = currentHeight;
			transform.position =  pos;
			Ray ray = new Ray(target.transform.position, transform.position - target.transform.position);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, distance))
				transform.position = hit.point;

			// Always look at the target
			transform.LookAt (target);
		}
	}
}