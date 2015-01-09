using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private CharacterController controller;

	private Animator animator;

	private Vector3 targetPoint;

	public float movementSpeed = 1f;
	
	public float turnSpeed = 3f;

	public float jumpSpeed = 1f;

	public bool isMoving;

	private Vector3 movement =  Vector3.zero;

	void Start()
	{
		movement.y = Physics.gravity.y / 1.4f;
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
		isMoving = false;
	}
	
	void Update() {
		if (!controller)
			return;
		movement = Vector3.zero;
		if (targetPoint != Vector3.zero && isMoving) 
		{
            RotateTowards (targetPoint - transform.position);
			float distance = Vector3.Distance (transform.position, targetPoint);
			if (distance <= 0.1f) 
			{
				isMoving = false;
				targetPoint = Vector3.zero;
			}else{
				movement = transform.forward * movementSpeed;
				if(distance < movement.magnitude * Time.deltaTime)
				{
					movement = movement.normalized * distance;
				}
			}
		}
		
		if(movement.magnitude > 0f)
		{
			animator.SetFloat("Speed", 1f);
		}else{
			animator.SetFloat("Speed", 0f);
		}
		if(!controller.isGrounded)
			movement.y = Physics.gravity.y / 10.4f;

		controller.Move (movement * Time.deltaTime);

	}

	public void MoveOrderToPoint(Vector3 target)
	{
		isMoving = true;
		targetPoint = target;
	}
    
    protected virtual void RotateTowards (Vector3 dir) {
        
        if (dir == Vector3.zero) return;
        
        Quaternion rot = transform.rotation;
        Quaternion toTarget = Quaternion.LookRotation (dir);
        
		rot = Quaternion.Slerp (rot,toTarget,turnSpeed*Time.deltaTime);
        Vector3 euler = rot.eulerAngles;
        euler.z = 0;
        euler.x = 0;
        rot = Quaternion.Euler (euler);
        
        transform.rotation = rot;
    }
}
