using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePoolCreator : MonoBehaviour
{
    public GameObject tilePrefab;
    float x;
    float y;
    GameObject[] tilePool;

    void Start()
    {
        int size = TileCreator.size;
        x = TileCreator.x;
        y = TileCreator.y;

        // Tile positions:
        // |UL|U|UR|
        // |L |C| R|
        // |DL|D|DR|
        // Create each tile as a child of this game object
        GameObject UL_tile = Instantiate(tilePrefab, new Vector3(x-size, 0, y+size), Quaternion.identity, this.transform);
        GameObject U_tile = Instantiate(tilePrefab, new Vector3(x, 0, y+size), Quaternion.identity, this.transform);
        GameObject UR_tile = Instantiate(tilePrefab, new Vector3(x+size, 0, y+size), Quaternion.identity, this.transform);
        GameObject R_tile = Instantiate(tilePrefab, new Vector3(x+size, 0, y), Quaternion.identity, this.transform);
        GameObject C_tile = Instantiate(tilePrefab, this.transform);
        GameObject L_tile = Instantiate(tilePrefab, new Vector3(x-size, 0, y), Quaternion.identity, this.transform);
        GameObject DL_tile = Instantiate(tilePrefab, new Vector3(x-size, 0, y-size), Quaternion.identity, this.transform);
        GameObject D_tile = Instantiate(tilePrefab, new Vector3(x, 0, y-size), Quaternion.identity, this.transform);
        GameObject DR_tile = Instantiate(tilePrefab, new Vector3(x+size, 0, y-size), Quaternion.identity, this.transform);

        tilePool = new GameObject[]
        {
            UL_tile, U_tile, UR_tile,
            R_tile, C_tile, L_tile,
            DL_tile, D_tile, DR_tile

        };
    }

    void Update()
    {
        // Check if we are in the center tile's region
        if (!InRegion(tilePool[4]))
        {

        }
    }

    bool InRegion(GameObject tile)
    {
        //Fetch the Collider from the GameObject this script is attached to
        Collider meshCollider = tile.GetComponent<Collider>();
        //Assign the point to be that of the Transform you assign in the Inspector window
        Vector3 playerPoint = CameraController.player.transform.position;
        //If the first GameObject's Bounds contains the Transform's position, output a message in the console
        if (meshCollider.bounds.Contains(playerPoint))
        {
            return true;
        }
        return false;
    }
}
