using UnityEngine;
using System.Collections;

public class CreatureFactory : MonoBehaviour {

	void Start()
	{
		createTroll(new Vector3(52.454f, 10.438f, 41.506f), new Quaternion(0, 180, 0, 0));
		Debug.Log (Mathf.PerlinNoise (2, 3) + " . " + Mathf.PerlinNoise (2, 5));
	}

	public GameObject createTroll(Vector3 position, Quaternion rotation)
	{
		GameObject obj = createCreature ("Earthborn Troll/Troll", position, rotation);
		obj.name = "Troll";
		obj.AddComponent ("TrollAnimation");
		obj.AddComponent ("Rotation");
		obj.AddComponent ("Movement");
		obj.AddComponent ("Rigidbody");
		obj.GetComponent<Rigidbody> ().interpolation = RigidbodyInterpolation.Interpolate;
		obj.AddComponent ("BoxCollider");
		obj.GetComponent<BoxCollider> ().size = new Vector3 (0.227f, 0.344f, 0.175f);
		obj.GetComponent<BoxCollider> ().center = new Vector3 (-0.001f, 0.170f, -0.009f);
		obj.GetComponent<Movement> ().TargetLocation = new Vector3(0, 0, 1);
		return obj;
	}

	public GameObject createCreature(string modelPath, Vector3 position, Quaternion rotation)
	{
		GameObject prefab = Resources.Load(modelPath) as GameObject;
		GameObject obj = Instantiate(prefab) as GameObject;
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		return obj;
	}
}
