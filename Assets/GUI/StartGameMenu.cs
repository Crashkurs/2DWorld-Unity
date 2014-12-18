using UnityEngine;
using System.Collections;

public class StartGameMenu : MonoBehaviour {

	void OnMouseUpAsButton()
	{
		Application.LoadLevel ("SelectGameMode");
	}
}
