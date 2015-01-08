using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class GameManager : MonoBehaviour
{

	public List<GameObject> selectedUnits = new List<GameObject> ();
	
	public GridGraph graph;

	private Camera camera;
	
	private float timer = 1f;

	// Use this for initialization
	void Start ()
	{
		selectedUnits = new List<GameObject> ();
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		graph = AstarPath.active.astarData.gridGraph;
		switchToRTSMode();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftAlt)) 
		{
			if (Configuration.Properties.isRPGMode) 
			{
				switchToRTSMode ();
			} else {
				switchToRPGMode ();
			}
		}
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			graph.center = camera.transform.position;
			graph.center.y = 0;
			AstarPath.active.Scan();
			timer = 1f;
			Debug.Log("Graph updated");
		}
	}

	public void addSelectedUnit (GameObject unit)
	{
		if (!selectedUnits.Contains (unit)) 
		{
			selectedUnits.Add (unit);
		}
	}

	public void clearSelectedUnits ()
	{
		selectedUnits.Clear ();
	}

	public List<GameObject> getSelectedUnits ()
	{
		return selectedUnits;
	}

	public void switchToRPGMode ()
	{
		Configuration.Properties.isRPGMode = true;
		camera.GetComponent<BirdCamera>().enabled = false;
		camera.GetComponent<SmoothFollow>().enabled = true;
		camera.isOrthoGraphic = true;
		camera.transform.rotation = Quaternion.Euler(40, 180, 0);
		camera.orthographicSize = 1f;
		clearSelectedUnits();
	}

	public void switchToRTSMode ()
	{
		Configuration.Properties.isRPGMode = false;
		camera.GetComponent<BirdCamera>().enabled = true;
		camera.GetComponent<SmoothFollow>().enabled = false;
		camera.GetComponent<BirdCamera>().panToTarget(camera.GetComponent<SmoothFollow>().target.gameObject);
		camera.isOrthoGraphic = true;
		camera.orthographicSize = 2f;
		camera.transform.rotation = Quaternion.Euler(60, 180, 0);
	}
}
