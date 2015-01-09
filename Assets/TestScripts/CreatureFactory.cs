using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureFactory : MonoBehaviour {

	private Dictionary<string, GameObject> prefabs;

	void Start()
	{
		prefabs = new Dictionary<string, GameObject> ();
		GameObject obj = createTroll(new Vector3(52.454f, 12.438f, 41.506f), new Quaternion(0, 180, 0, 0));
		GameObject obj2 = createTroll(new Vector3(55.454f, 12.438f, 41.506f), new Quaternion(0, 180, 0, 0));
		obj.GetComponent<Seeker> ().StartPath (transform.position, transform.position + new Vector3 (20, 0, 20), null);
		GameObject.Find ("Main Camera").GetComponent<SmoothFollow> ().target = obj.transform;
		GameObject.Find ("Main Camera").GetComponent<BirdCamera> ().panToTarget(obj);
	}

	public GameObject createTroll(Vector3 position, Quaternion rotation)
	{
		GameObject obj = createCreature ("Prefab/Troll", position, rotation);
		obj.name = "Troll";
		return obj;
	}

	public GameObject createCreature(string modelPath, Vector3 position, Quaternion rotation)
	{
		GameObject prefab;
		if (prefabs.ContainsKey (modelPath)) 
		{
			prefabs.TryGetValue(modelPath, out prefab);
		} else {
			prefab = Resources.Load (modelPath) as GameObject;
			prefabs.Add(modelPath, prefab);
		}
		GameObject obj = Instantiate(prefab) as GameObject;
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		return obj;
	}
}
