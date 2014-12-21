using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainFactory : MonoBehaviour {

	public Material material;

	void Start () {
		createTerrain (new Vector3 (-16, 0, -16), "Images/Gras", new Vector3(100, 100, 100));
	}

	public GameObject createTerrain(Vector3 position, string texturePath, Vector3 terrainSize)
	{
		GameObject terrain;

		TerrainData terrainData = new TerrainData ();
		const int size = 513;
		terrainData.heightmapResolution = size;
		terrainData.size = terrainSize;
		terrainData.heightmapResolution = 512;
		terrainData.baseMapResolution = 1024;

		terrain = Terrain.CreateTerrainGameObject (terrainData);
		terrain.transform.position = position;
		terrain.transform.rotation = Quaternion.identity;
		terrain.name = "Chunk1";
		loadTexture (terrain.GetComponent<Terrain> (), texturePath);
		GenerateHeights (terrain.GetComponent<Terrain> (), 1f);
		terrain.AddComponent ("ClickMovement");
		return terrain;
	}

	private void loadTexture(Terrain terrain, string texturePath)
	{
		SplatPrototype newSplat = new SplatPrototype();
		newSplat.texture = Resources.Load(texturePath, typeof(Texture2D)) as Texture2D;
		newSplat.tileSize = new Vector2( 1, 1 );
		newSplat.tileOffset = Vector2.zero;

		List<SplatPrototype> terrainTextureList = new List< SplatPrototype >();
		foreach(SplatPrototype protoType in terrain.terrainData.splatPrototypes)
		{
			terrainTextureList.Add(protoType);
		}

		terrainTextureList.Add (newSplat);
		terrain.terrainData.splatPrototypes = terrainTextureList.ToArray ();
		terrain.terrainData.RefreshPrototypes ();
		terrain.Flush ();
	}

	public void GenerateHeights(Terrain terrain, float tileSize)
	{
		float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];
		
		for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
			{
				heights[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize)/10.0f;
			}
		}
		
		terrain.terrainData.SetHeights(0, 0, heights);
	}
}
