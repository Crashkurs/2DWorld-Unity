using UnityEngine;
using System.Collections;

public class ClickMovement : MonoBehaviour {

	private GameManager gameManager;

	void Start()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(1) && !Configuration.Properties.isRPGMode)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			collider.Raycast(ray, out hit, Mathf.Infinity);

			foreach(GameObject obj in gameManager.getSelectedUnits())
			{
				obj.GetComponent<PathMovement>().OrderMoveToPoint(hit.point);
			}
		}
	}

	void OnMouseDown()
	{
		gameManager.getSelectedUnits ().Clear ();
	}
}
