using UnityEngine;
using System.Collections;

public class TerrainFactory : MonoBehaviour {

	public Material material;

	void Start () {
		createTerrain (new Vector3 (-16, 0, -16), "Images/Gras");
	}

	public GameObject createTerrain(Vector3 position, string texturePath)
	{
		GameObject terrain;

		TerrainData terrainData = new TerrainData ();
		const int size = 513;
		terrainData.heightmapResolution = size;
		terrainData.size = new Vector3 (2000, 600, 2000);
		terrainData.heightmapResolution = 512;
		terrainData.baseMapResolution = 1024;
		terrainData.RefreshPrototypes ();

		SplatPrototype[] spalts = terrainData.splatPrototypes;
		Texture2D tex = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
		for (int x = 0; x < spalts.Length; x++)
		{
			spalts[x].texture = tex;
		}

		terrain = Terrain.CreateTerrainGameObject (terrainData);
		terrain.transform.position = position;
		terrain.transform.rotation = Quaternion.identity;
		terrain.name = "Chunk1";

		return terrain;
	}
}
