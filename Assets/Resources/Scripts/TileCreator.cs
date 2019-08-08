using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

// Automatically adds required components if the GameObject doesn't have them
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TileCreator : MonoBehaviour
{
    public static int size = 50;
    // Latitude
    public static float x;
	int maxLat = 90;
    // Longitude
	public static float y;
	int maxLong = 180;

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

		float margin = ((float)size / 2);
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
		float xCoord = playerPosition.x + this.transform.position.z;
		float yCoord = playerPosition.z + this.transform.position.x;

		float UpperX = xCoord - size;
		float UpperY = yCoord - size;

		if (UpperX > maxLat || UpperY > maxLong)
		{
			if (UpperX > maxLat)
			{
				bool xNeg;
				if (UpperX > 0)
				{
					xNeg = false;
				}
				else
				{
					xNeg = true;
				}
				UpperX = Math.Abs(UpperX % maxLat);
				if (xNeg)
				{
					UpperX = -UpperX;
				}
			}
			if (UpperY > maxLong)
			{
				bool yNeg;
				if (UpperY > 0)
				{
					yNeg = false;
				}
				else
				{
					yNeg = true;
				}
				UpperY = Math.Abs(UpperY % maxLong);
				if (yNeg)
				{
					UpperY = -UpperY;
				}
			}
		}

		string bottomLeft = Math.Min(yCoord, UpperY) + "," + Math.Min(xCoord, UpperX);
		string upperRight = Math.Max(yCoord, UpperY) + "," + Math.Max(xCoord, UpperX);

		string url = "http://www2.demis.nl/wms/wms.asp?Service=WMS&WMS=BlueMarble&Version=1.1.0&Request=GetMap&BBox=" + bottomLeft + "," + upperRight + "&SRS=EPSG:4326&Width=" + res + "&Height=" + res + "&Layers=Earth%20Image&Format=image/png";
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
