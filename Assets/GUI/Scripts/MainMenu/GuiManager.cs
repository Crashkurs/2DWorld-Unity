using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	public void CloseGame()
	{
		Application.Quit ();
	}

	public void LoadMultiplayerMenu()
	{
		Application.LoadLevel ("MultiplayerMenu");
	}

	public void LoadOptionsMenu()
	{
		Application.LoadLevel ("OptionsMenu");
	}

	public void LoadSingleplayerMenu()
	{
		Application.LoadLevel ("SingleplayerMenu");
	}

	public void loadMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

	public void loadSingleplayerGame()
	{
		Application.LoadLevel ("Game");
		Debug.Log ("Lade Einzelspieler...");
	}

	public void loadMultiplayerGameAsServer()
	{
		Application.LoadLevel ("Game");
		Debug.Log ("Erstelle Server für Multiplayer");
	}

	public void loadMultiplayerGameAsClient()
	{
		Application.LoadLevel ("Game");
		Debug.Log ("Verbinde zu Server für Multiplayer");
	}
}
