using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically adds required components if the object doesn't have them
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class TileCreator : MonoBehaviour
{
    public static int size = 50;
	public static float x;
	public static float y;
	GameObject player;

	void Awake()
    {
		player = CameraController.player;
		Vector3 player_pos = player.transform.position;
		float margin = ((float)size / 2);
		x = player_pos.x - margin;
		y = player_pos.y - margin;

		Mesh mesh = GetComponent<MeshFilter>().mesh = new Mesh();
        mesh.name = "Procedural Ground";

        // The number of vertices needed to make a quad that has [size] tiles
        Vector3[] vertices =
        {
            new Vector3(x, 0, y),
            new Vector3(x+size, 0, y),
            new Vector3(x, 0, y+size),
            new Vector3(x+size, 0, y+size)
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
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
        };

        // Add the vertices to the mesh
        mesh.triangles = triangles;
        // Normals are perpendicular to the object
        // Recalculate the normals so the shading would look correct
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
