using UnityEngine;
using System.Collections;

public class TrollAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Walk()
	{
		gameObject.animation.Play ("Walk");
	}

	public void StopWalk()
	{
		gameObject.animation.Play ("Idle_01");
	}
}
