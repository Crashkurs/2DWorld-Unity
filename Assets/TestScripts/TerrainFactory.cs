using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainFactory : MonoBehaviour {

	void Start () {
		GameObject one = createTerrain (new Vector3 (0, 0, 0), "Images/Gras", new Vector3(100, 100, 100));
		GameObject two = createTerrain (new Vector3 (100, 0, 0), "Images/Gras", new Vector3(100, 100, 100));
		one.GetComponent<Terrain> ().SetNeighbors (null, null, two.GetComponent<Terrain> (), null);
		two.GetComponent<Terrain> ().SetNeighbors (one.GetComponent<Terrain> (), null, null, null);
		//createSeemlessNeighbours (one.GetComponent<Terrain> (), two.GetComponent<Terrain> (), 0.001f);
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
		GenerateHeights (terrain.GetComponent<Terrain> (), 5f);
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
		
		for (int i = 0; i <  terrain.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
			{
				float xCoord = (terrain.transform.position.x + terrain.terrainData.size.x * (i * 1f / terrain.terrainData.heightmapWidth));
				float zCoord = (terrain.transform.position.z + terrain.terrainData.size.z * (k * 1f / terrain.terrainData.heightmapHeight));
				heights[i, k ] = Mathf.PerlinNoise(xCoord, zCoord) / terrain.terrainData.heightmapResolution * tileSize;

			}
		}

		terrain.terrainData.SetHeights(0, 0, heights);
	}

	private void createSeemlessNeighbours(Terrain terrain1, Terrain terrain2, float stepSize)
	{
		TerrainData t1 = terrain1.terrainData;
		TerrainData t2 = terrain2.terrainData;

		float[,] heights1 = t1.GetHeights (0, 0, t1.heightmapWidth, t1.heightmapHeight);
		float[,] heights2 = t2.GetHeights (0, 0, t2.heightmapWidth, t2.heightmapHeight);
		for (int x = 0; x < t1.heightmapWidth; x++)
		{
			heights1[x, t1.heightmapHeight-1] = heights2[x, 0];
			bool fits = false;
			int pointer = t1.heightmapHeight-1;
			while(!fits)
			{
				try{
					if(heights1[x, pointer] - stepSize > heights1[x, pointer-1])
					{
						heights1[x, pointer - 1] = heights1[x, pointer] - stepSize;
						pointer--;
					}else{
						if(heights1[x, pointer] + stepSize < heights1[x, pointer-1])
						{
							heights1[x, pointer - 1] = heights1[x, pointer] + stepSize;
							pointer--;
						}else{
							fits = true;
						}
					}
				}catch(IndexOutOfRangeException e)
				{
					Debug.Log("Pointer: " + pointer);
					return;
				}
			}
		}
		t1.SetHeights (0, 0, heights1);
	}
}
