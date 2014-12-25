using UnityEngine;
using System.Collections;

public class BirdCamera : MonoBehaviour {

	public float moveSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 posChange = Vector3.zero;
		Vector3 mousePos = Input.mousePosition;

		if (mousePos.x <= 10)
						posChange.x -= moveSpeed * Time.deltaTime;
		if (mousePos.x >= Screen.width - 10)
						posChange.x += moveSpeed * Time.deltaTime;
		if (mousePos.y <= 10)
						posChange.z -= moveSpeed * Time.deltaTime;
		if (mousePos.y >= Screen.height - 10)
						posChange.z += moveSpeed * Time.deltaTime;

		transform.position += posChange;
	}

	public void panToTarget(GameObject target)
	{
		StartCoroutine ("panTarget", target.transform);
	}

	IEnumerator panTarget(Transform trans)
	{
		Vector3 targetPos = trans.position;
		targetPos.y += 2f;
		float t = 0f;
		while(t <= Vector3.Distance (transform.position, targetPos) / moveSpeed)
		{
			transform.position = Vector3.Lerp(transform.position, targetPos, t);
			t += Time.deltaTime;
			yield return null;
		}
	}
}
