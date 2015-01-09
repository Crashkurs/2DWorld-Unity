using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class TerrainFactory : MonoBehaviour {

	public static float chunkSize{
		get{ return chunkSizeVector.x; }
		set{ chunkSizeVector = new Vector3(value, value, value); }
	}

	private static Vector3 chunkSizeVector;
	private AstarPath pathFinder;

	void Start () {
		chunkSize = 100f;
		GameObject one = createTerrain (new Vector3 (0, 0, 0), "Images/Grass & rocks/diffuse", 5f, 10f);
		GameObject two = createTerrain (new Vector3 (100, 0, 0), "Images/Grass 02/diffuse", 15f, 25f);
		GameObject three = createTerrain (new Vector3 (0, 0, 100), "Images/Lava/diffuse", 5f, 10f);
		GameObject four = createTerrain (new Vector3 (100, 0, 100), "Images/Gras", 5f, 10f);
		generatePlane (one.GetComponent<Terrain>(), new Vector3 (100, 0, 100), 40);
		TerrainStitcher.stitch (one.GetComponent<Terrain> (), two.GetComponent<Terrain> (), 15);
		TerrainStitcher.stitch (one.GetComponent<Terrain> (), three.GetComponent<Terrain> (), 15);
		TerrainStitcher.stitch (two.GetComponent<Terrain> (), four.GetComponent<Terrain> (), 15);
		TerrainStitcher.stitch (four.GetComponent<Terrain> (), three.GetComponent<Terrain> (), 15);
		//one.GetComponent<TerrainToolkit> ().FractalGenerator (0.4f, 1f);
		one.GetComponent<Terrain> ().SetNeighbors (null, null, two.GetComponent<Terrain> (), null);
		two.GetComponent<Terrain> ().SetNeighbors (one.GetComponent<Terrain> (), null, null, null);
		
		generatePath(one.GetComponent<Terrain>(), new Vector3(0, 0, 0), new Vector3(30, 0, 20), 5f);

		pathFinder = GameObject.Find ("PathFinding").GetComponent<AstarPath>();
		pathFinder.Scan ();
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
		terrain.layer = 8;
		loadTexture (terrain.GetComponent<Terrain> (), texturePath);
		loadTexture(terrain.GetComponent<Terrain> (), "Images/Ground & rocks 02/diffuse");
		terrain.GetComponent<Terrain> ().heightmapPixelError = 1f;
		GenerateHeights (terrain.GetComponent<Terrain> (), hillHeight, hillSize);
		terrain.AddComponent ("ClickMovement");
		terrain.AddComponent<TerrainToolkit> ();

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
				heights[i, k ] = 0.1f + Mathf.PerlinNoise(xCoord / hillSize, zCoord / hillSize) * hillHeight / terrain.terrainData.heightmapResolution;
			}
		}

		terrain.terrainData.SetHeights(0, 0, heights);
	}

	public void generatePlane(Terrain terrain, Vector3 center, int size)
	{
		TerrainData data = terrain.terrainData;
		float[,] heights = data.GetHeights (0, 0, data.heightmapWidth, data.heightmapHeight);
		float startHeight = heights [(int)center.x, (int)center.z];

		//Calculate average height in quadrat size
		float height = 0f;
		for(int x = (int)(center.x - size/2); x < (int)(center.x + size/2); x++)
		{
			for(int z = (int)(center.z - size/2); z < (int)(center.z + size/2); z++)
			{
				height += heights[x, z];
			}
		}
		height = height/(size * size);
		for(int x = (int)(center.x - size/2); x < (int)(center.x + size/2); x++)
		{
			for(int z = (int)(center.z - size/2); z < (int)(center.z + size/2); z++)
			{
				heights[x,z] = height;
			}
		}
		data.SetHeights (0, 0, heights);
	}

	public void generateMountain(Terrain terrain, Vector3 center, int size, float height)
	{
		TerrainData data = terrain.terrainData;
		float[,] heights = data.GetHeights (0, 0, data.heightmapWidth, data.heightmapHeight);
	}
	
	public void generatePath(Terrain terrain, Vector3 startPoint, Vector3 endPoint, float width)
	{
		TerrainData data = terrain.terrainData;
		float[, ,] textures = data.GetAlphamaps(0, 0, data.alphamapWidth, data.alphamapHeight);
		Vector3 dir = (endPoint - startPoint).normalized;
		Vector3 vec = startPoint;
		while(vec != endPoint)
		{
			if(vec.x < 0 || vec.x >= data.alphamapWidth || vec.z < 0 || vec.z >= data.alphamapHeight)
			{
				vec = endPoint;
				break;
			}
			for(int x = (int)(vec.x - width/2); x < (int)(vec.x + width/2); x++)
			{
				for(int z = (int)(vec.z - width/2); z < (int)(vec.z + width/2); z++)
				{
					if(x < 0 || x >= data.alphamapWidth || z < 0 || z >= data.alphamapHeight)
						break;
					textures[x, z, 0] = 0f;
					textures[x, z, 1] = 1f;
				}
			}
			vec += dir;
			
		}
		data.SetAlphamaps(0, 0, textures);
	}
}
