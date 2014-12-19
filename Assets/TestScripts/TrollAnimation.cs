using UnityEngine;
using System.Collections;

public class TrollAnimation : MonoBehaviour {

	private float timeToChange = 3;

	private int animationSize;

	private string[] animationNames;

	// Use this for initialization
	void Start () {
		animationSize = gameObject.animation.GetClipCount ();

		animationNames = new string[animationSize];
		int counter = 0;
		foreach (AnimationState state in gameObject.animation) 
		{
			animationNames[counter++] = state.name;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeToChange -= Time.deltaTime;
		if (timeToChange <= 0) {
			int rand = Random.Range(0, gameObject.animation.GetClipCount()-1);
			gameObject.animation.Play(animationNames[rand]);
			timeToChange = 3;
						
				}
	}
}
