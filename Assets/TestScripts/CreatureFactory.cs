using UnityEngine;
using System.Collections;

public class CreatureFactory : MonoBehaviour {

	void Start()
	{
		createTroll(new Vector3(0, 20, 0), new Quaternion(0, 180, 0, 0));
	}

	public GameObject createTroll(Vector3 position, Quaternion rotation)
	{
		GameObject obj = createCreature ("Earthborn Troll/Troll", position, rotation);
		obj.name = "Troll";
		obj.AddComponent ("TrollAnimation");
		obj.AddComponent ("Rotation");
		obj.AddComponent ("Movement");
		obj.AddComponent ("Rigidbody");
		obj.AddComponent ("BoxCollider");
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
