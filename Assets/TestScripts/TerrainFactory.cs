using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TerrainFactory : MonoBehaviour {

	public static float chunkSize{
		get{ return chunkSizeVector.x; }
		set{ chunkSizeVector = new Vector3(value, value, value); }
	}

	private static Vector3 chunkSizeVector;

	void Start () {
		chunkSize = 100f;
		GameObject one = createTerrain (new Vector3 (0, 0, 0), "Images/Gras", 5f, 10f);
		GameObject two = createTerrain (new Vector3 (100, 0, 0), "Images/Gras", 5f, 10f);
		GameObject three = createTerrain (new Vector3 (0, 0, 100), "Images/Gras", 5f, 10f);
		GameObject four = createTerrain (new Vector3 (100, 0, 100), "Images/Gras", 5f, 10f);
		one.GetComponent<Terrain> ().SetNeighbors (null, null, two.GetComponent<Terrain> (), null);
		two.GetComponent<Terrain> ().SetNeighbors (one.GetComponent<Terrain> (), null, null, null);
		//adjustHeightForTransition (one.GetComponent<Terrain> (), two.GetComponent<Terrain> (), three.GetComponent<Terrain> (), four.GetComponent<Terrain> ());
		//createSeemlessNeighbours (one.GetComponent<Terrain> (), two.GetComponent<Terrain> (), 0.001f);
	}

	public GameObject createTerrain(Vector3 position, string texturePath, float hillHeight, float hillSize)
	{
		GameObject terrain;

		TerrainData terrainData = new TerrainData ();
		const int size = 512;
		terrainData.heightmapResolution = size;
		terrainData.size = chunkSizeVector;
		terrainData.heightmapResolution = 512;
		terrainData.baseMapResolution = 1024;

		terrain = Terrain.CreateTerrainGameObject (terrainData);
		terrain.transform.position = position;
		terrain.transform.rotation = new Quaternion(0, 90, 0, 0);
		terrain.name = "Chunk1";
		loadTexture (terrain.GetComponent<Terrain> (), texturePath);
		terrain.GetComponent<Terrain> ().heightmapPixelError = 1f;
		GenerateHeights (terrain.GetComponent<Terrain> (), hillHeight, hillSize);
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

	public void GenerateHeights(Terrain terrain, float hillHeight, float hillSize)
	{
		float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];
		
		for (int i = 0; i <  terrain.terrainData.heightmapWidth; i++)
		{
			for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
			{
				float xCoord = (terrain.transform.position.z + terrain.terrainData.size.z * (i+i * 1f/(terrain.terrainData.heightmapWidth-1)) / terrain.terrainData.heightmapWidth);
				float zCoord = (terrain.transform.position.x + terrain.terrainData.size.x * (k+k * 1f/(terrain.terrainData.heightmapHeight-1)) / terrain.terrainData.heightmapHeight);
				heights[i, k ] = 0.1f + SimplexNoise.Noise(xCoord / hillSize, zCoord / hillSize) * hillHeight / terrain.terrainData.heightmapResolution;
			}
		}

		terrain.terrainData.SetHeights(0, 0, heights);
	}

	public void adjustHeightForTransition(Terrain t1, Terrain t2, Terrain t3, Terrain t4)
	{
		createSeemlessNeighbours (t1, t2, 2f);
		createSeemlessNeighbours (t1, t3, 2f);
		createSeemlessNeighbours (t3, t4, 2f);
		createSeemlessNeighbours (t2, t4, 2f);
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
