using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 TargetLocation {
		get{ return targetLocation;}
				set{
						targetLocation = value;
			GetComponent<Rotation> ().RotationGoal = targetLocation;
		}
	}

	public Vector3 targetLocation;

	public float movementSpeed = 1f;

	public bool isMoving = false;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyboardSettings.getMappedKey(KeyboardSettings.KeyName.MoveForward))) 
		{
			targetLocation = Vector3.zero;
			MoveForward();
		}
		if (Input.GetKeyUp (KeyboardSettings.getMappedKey(KeyboardSettings.KeyName.MoveBackward)))
		{
			GetComponent<TrollAnimation> ().StopWalk ();
			isMoving = false;
		}
		if (targetLocation != Vector3.zero) {
						if (Vector3.Distance (transform.position, targetLocation) > 0.1f) {
								Rotation rotation = GetComponent<Rotation> ();
								if (rotation.isRotating) {

								} else {
										isMoving = true;
										MoveForward ();
								}
						} else {
								transform.position = targetLocation;
								GetComponent<TrollAnimation> ().StopWalk ();
								isMoving = false;
						}
				}
	}

	private void MoveForward()
	{
		GetComponent<TrollAnimation>().Walk();
		transform.Translate(Vector3.forward.normalized * movementSpeed * Time.deltaTime);
	}
}
