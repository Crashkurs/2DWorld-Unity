using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public Vector3 RotationGoal {
				get { 
						return rotationGoal;
				}
				set {
						rotationGoal = new Vector3(value.x, transform.position.y, value.z) - transform.position;;
						isRotating = true;
				}
		}

	private Vector3 rotationGoal;
	public float rotationSpeed = 2f;

	public bool isRotating = false;
	
	// Update is called once per frame
	void Update () {
			//if (isRotating) {

						Quaternion targetRotation = Quaternion.LookRotation (RotationGoal, Vector3.up);
						transform.rotation = targetRotation;
						isRotating = false;
				//}
			/*transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			if(Mathf.Abs(transform.rotation.y - targetRotation.y) <= 0.1f)
			{
				transform.rotation = targetRotation;
			}*/
		
		if (Input.GetKey (KeyboardSettings.getMappedKey(KeyboardSettings.KeyName.MoveLeft))) {
			isRotating = false;
			Quaternion rot = transform.rotation;
			rot.y -= rotationSpeed * Time.deltaTime;
			transform.rotation = rot;
		}
		if (Input.GetKey (KeyboardSettings.getMappedKey(KeyboardSettings.KeyName.MoveRight))) {
			isRotating = false;
			Quaternion rot = transform.rotation;
			rot.y += rotationSpeed * Time.deltaTime;
			transform.rotation = rot;
		}
	}
}
