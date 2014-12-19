using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyScrollbar : MonoBehaviour {

	public enum Difficulty
	{
		Anfänger,
		Fortgeschrittener,
		Experte,
		Meister,
		Wahnsinn
	}

	private int size;

	void Start()
	{
		size = System.Enum.GetValues (typeof(Difficulty)).Length;

		Scrollbar bar = gameObject.GetComponent<Scrollbar> ();
		bar.numberOfSteps = size;
		bar.value = Configuration.Properties.difficulty * 1.0f / (size-1);
		updateDifficultyText ();
	}

	public void DifficultyChanged()
	{
		Scrollbar bar = gameObject.GetComponent<Scrollbar> ();
		Configuration.Properties.difficulty = (int)(bar.value * (size-1));
		updateDifficultyText ();
	}

	private void updateDifficultyText()
	{
		Text text = gameObject.GetComponentInChildren<Text> ();
		text.text = (System.Enum.GetValues(typeof(Difficulty)).GetValue(Configuration.Properties.difficulty)).ToString();
	}
}
