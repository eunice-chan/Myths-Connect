using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

// Automatically adds required components if the GameObject doesn't have them
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TileCreator : MonoBehaviour
{
    public static int size = 30;
    // Latitude
    public static float x;
	int maxLat = 90;
    // Longitude
	public static float y;
	int maxLong = 180;
	float margin;

	GameObject player;
	Vector3 playerPosition;

	GameObject NPC;
	static int minNPC = 0;
    static int maxNPC = 3;
    GameObject[] NPCPool = new GameObject[maxNPC];


	static System.Random rand = new System.Random();
    int res = 100;

    void Start()
    {
		NPC = (GameObject)Resources.Load("Prefabs/NPC");

		player = GameObject.Find("Player");
		playerPosition = player.transform.position;

		StartCoroutine(SetTexture());

		margin = ((float) size) / 2;
		print(margin);
		x = playerPosition.x - margin;
		y = playerPosition.z - margin;

		Mesh mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.name = "Procedural Ground";

        // The number of vertices needed to make a quad that has [size] tiles
        Vector3[] vertices =
        {
            new Vector3(x, 0, y),
            new Vector3(x + size, 0, y),
            new Vector3(x, 0, y + size),
            new Vector3(x + size, 0, y + size)
        };
        Vector2[] uv = 
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        // Add the vertices to the mesh
        mesh.vertices = vertices;
        // UV coordinates allow the material to be displayed correctly
        // To fit the grid, divide the position of the vertex by the grid dimensions
        mesh.uv = uv;



        // Create the triangles connecting the vertices
        // Order triangles clockwise
        int[] triangles = 
        {
            // Lower left triangle
            0, 2, 1,
            // Upper right triangle
            2, 3, 1
        };

        // Add the vertices to the mesh
        mesh.triangles = triangles;
        // Normals are perpendicular to the GameObject
        // Recalculate the normals so the shading would look correct
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;

		InstantiateNPCs();
		ActivateNPCs();
    }

	void InstantiateNPCs()
	{
		for (int i = 0; i < maxNPC; i++)
		{
			NPCPool[i] = Instantiate(NPC, this.transform);
			NPCPool[i].transform.position = NPCPool[i].transform.position + new Vector3(0, 0.5f, 0);
			NPCPool[i].SetActive(false);
		}
	}

	public void ActivateNPCs()
	{
		int NumNPC = rand.Next(minNPC, maxNPC + 1);
		int tolerance = 2;
		List<Vector3> takenSpots = new List<Vector3>();
		takenSpots.Add(player.transform.position);
		for (int i = 0; i < NumNPC; i++)
		{
			int XCoord = rand.Next(size) - size / 2;
			int YCoord = rand.Next(size) - size / 2;
			NPCPool[i].transform.position = new Vector3(XCoord, NPCPool[i].transform.position.y, YCoord) + this.transform.position + this.transform.parent.transform.position;
			for (int j = 0; j < takenSpots.Count; j++)
			{
				while (Vector3.Distance(NPCPool[i].transform.position, takenSpots[j]) < tolerance)
				{
					XCoord = rand.Next(size) - size / 2;
					YCoord = rand.Next(size) - size / 2;
					NPCPool[i].transform.position = new Vector3(XCoord, NPCPool[i].transform.position.y, YCoord) + this.transform.position + this.transform.parent.transform.position;
				}
			}
			takenSpots.Add(NPCPool[i].transform.position);
			NPCPool[i].SetActive(true);
		}
	}

    public void DeactivateNPCs()
	{
		foreach(GameObject n in NPCPool)
		{
			n.SetActive(false);
		}

	}

	public IEnumerator SetTexture()
	{
		margin = ((float)size) / 2;
		float max_y = playerPosition.z + this.transform.position.x + margin;
		float max_x = playerPosition.x + this.transform.position.z + margin;

		while (max_y < -maxLong)
		{
			max_y += maxLong * 2;

		}
		while (max_y > maxLong)
		{
			max_y -= maxLong * 2;

		}
		while (max_x < -maxLat)
		{
			max_x += maxLat * 2;

		}
		while (max_x > maxLat)
		{
			max_x -= maxLat * 2;

		}

		float min_y = max_y - size;
		float min_x = max_x - size;

        // This stretches the tiles to hide the issues.
		//if (min_y < -180) { min_y = -180; }
		//if (min_x < -90) { min_x = -90; }

		//if (max_y > 180) { max_y = 180; }
		//if (max_x > 90) { max_x = 90; }

		string BL = min_y + "," + min_x;
		string UR = max_y + "," + max_x;

		string url = "http://www2.demis.nl/wms/wms.asp?Service=WMS&WMS=BlueMarble&Version=1.1.0&Request=GetMap&BBox=" + BL + "," + UR + "&SRS=EPSG:4326&Width=" + res + "&Height=" + res + "&Layers=Earth%20Image&Format=image/png";
		print(url);

		UnityWebRequest img = UnityWebRequestTexture.GetTexture(url);
		yield return img.SendWebRequest();

		if (img.isNetworkError || img.isHttpError)
		{
			Debug.Log(img.error);
		}
		else
		{
			this.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)img.downloadHandler).texture;
		}
	}
}
