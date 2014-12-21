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

	public float movementSpeed = 0.4f;

	public bool isMoving = false;
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, targetLocation) > 0.05f) 
		{
			Rotation rotation = GetComponent<Rotation>();
			if(rotation.isRotating)
			{

			}else{
					isMoving = true;
					GetComponent<TrollAnimation>().Walk();
					transform.Translate(Vector3.forward.normalized * movementSpeed * Time.deltaTime);
			}
		}else{
			transform.position = targetLocation;
			GetComponent<TrollAnimation>().StopWalk();
		}
	}
}
