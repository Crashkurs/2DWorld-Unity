using UnityEngine;
using System.Collections;

public class ClickMovement : MonoBehaviour {

	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			collider.Raycast(ray, out hit, Mathf.Infinity);

			GameObject.Find("Troll").GetComponent<Movement>().TargetLocation = hit.point;
		}
	}
}
