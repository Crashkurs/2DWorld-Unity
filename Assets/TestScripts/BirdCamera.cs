using UnityEngine;
using System.Collections;

public class BirdCamera : MonoBehaviour {

	public float moveSpeed = 3f;
	public float zoomSpeed = 1f;

	private static Quaternion resetRotation = Quaternion.Euler(60,180, 0);

	private Camera cameraScript;

	// Use this for initialization
	void Start () {
		transform.rotation = resetRotation;
		cameraScript = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 posChange = Vector3.zero;
		Vector3 mousePos = Input.mousePosition;
		Vector3 forward = transform.forward;
		forward.y = 0;

		if (mousePos.x <= 10)
			posChange += -transform.right * moveSpeed * Time.deltaTime * cameraScript.orthographicSize;
		if (mousePos.x >= Screen.width - 10)
			posChange += transform.right * Time.deltaTime * cameraScript.orthographicSize;
		if (mousePos.y <= 10)
			posChange += -forward * Time.deltaTime * cameraScript.orthographicSize;
		if (mousePos.y >= Screen.height - 10)
			posChange += forward * Time.deltaTime * cameraScript.orthographicSize;

		//Zoom
		float zoom = Input.GetAxis ("Mouse ScrollWheel");
		cameraScript.orthographicSize -= zoom * 30f * Time.deltaTime * zoomSpeed;

		transform.position += posChange;
		transform.position = new Vector3(transform.position.x, 30, transform.position.z);
	}

	public void panToTarget(GameObject target)
	{
		StartCoroutine ("panTarget", target.transform);
	}

	IEnumerator panTarget(Transform trans)
	{
		Vector3 targetPos = trans.position;
		Vector3 currentPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 0, Screen.height/2));
		Debug.Log(currentPos + " | " + targetPos);
		Vector3 dir = targetPos - currentPos;
		targetPos.y += 2f;
		float t = 0f;
		while(t <= moveSpeed)
		{
			transform.position = currentPos + dir * t / moveSpeed;
			t += Time.deltaTime;
			yield return null;
		}

	}
}
