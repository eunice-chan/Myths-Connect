using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically adds required components if the GameObject doesn't have them
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TileCreator : MonoBehaviour
{
    public static int size = 50;
    // Latitude
    public static float x;
    // Longitude
	public static float y;

    List<GameObject> NPCPrefabs = new List<GameObject>();

    static int minNPC = 0;
    static int maxNPC = 3;
    GameObject[] NPCPool = new GameObject[maxNPC];
	GameObject player;

    static System.Random rand = new System.Random();

    void Start()
    {
		NPCPrefabs.Add((GameObject)Resources.Load("Prefabs/NPC"));

		player = GameObject.Find("Player");
		Vector3 playerPosition = player.transform.position;
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

    void InstantiateNPCs() {
        for (int i = 0; i < maxNPC; i++)
        {
            NPCPool[i] = Instantiate(NPCPrefabs[rand.Next(NPCPrefabs.Count)], this.transform);
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
            NPCPool[i].transform.position = new Vector3(XCoord, 0, YCoord) + this.transform.position + this.transform.parent.transform.position;
            print(NPCPool[i].transform.position);
            for (int j = 0; j < takenSpots.Count; j++)
            {
                while (Vector3.Distance(NPCPool[i].transform.position, takenSpots[j]) < tolerance)
                {
                    XCoord = rand.Next(size) - size / 2;
                    YCoord = rand.Next(size) - size / 2;
                    NPCPool[i].transform.position = new Vector3(XCoord, 0, YCoord) + this.transform.position;
                }
            }
            takenSpots.Add(NPCPool[i].transform.position);
			NPCPool[i].SetActive(true);
        }
    }

    public void DeactivateNPCs()
	{
		foreach(GameObject NPC in NPCPool)
		{
			NPC.SetActive(false);
		}

	}
}
