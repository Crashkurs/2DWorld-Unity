using UnityEngine;
using System.Collections;

public class TrollAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (5, 11, 5);
		Vector3 vec = gameObject.transform.position;
		vec += new Vector3 (0, 4, 2);
		GameObject.Find ("Main Camera").transform.position = vec;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vec = gameObject.transform.position;
		vec.y = GameObject.Find ("Main Camera").transform.position.y;
		vec += new Vector3 (0, 0, 4);
		GameObject.Find ("Main Camera").transform.position = vec;
	}

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
