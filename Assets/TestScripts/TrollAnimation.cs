using UnityEngine;
using System.Collections;

public class TrollAnimation : MonoBehaviour {

	public void Walk()
	{
		gameObject.animation.Play ("Walk");
	}

	public void StopWalk()
	{
		gameObject.animation.Play ("Idle_01");
	}

	void OnCollisionEnter(Collision collision)
	{
		rigidbody.velocity = new Vector3 (rigidbody.velocity.x, 0, rigidbody.velocity.z);
	}
}
