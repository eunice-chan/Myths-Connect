﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePoolCreator : MonoBehaviour
{
	GameObject tilePrefab;
	GameObject[] tilePool;
	GameObject player;

	// Latitude
	float x = 0;
	// Longitude
	float y = 0;
	int size;

	Vector3 ULVector;
	Vector3 UVector;
	Vector3 URVector;
	Vector3 LVector;
	Vector3 CVector;
	Vector3 RVector;
	Vector3 DLVector;
	Vector3 DVector;
	Vector3 DRVector;

	void Start()
	{
		size = TileCreator.size;
		tilePrefab = (GameObject)Resources.Load("Prefabs/GroundTile");

		player = GameObject.Find("Player");
		this.transform.position = player.transform.position;

		ULVector = new Vector3(x - size, 0, y + size);
		UVector = new Vector3(x, 0, y + size);
		URVector = new Vector3(x + size, 0, y + size);
		LVector = new Vector3(x - size, 0, y);
		CVector = new Vector3(0, 0, 0);
		RVector = new Vector3(x + size, 0, y);
		DLVector = new Vector3(x - size, 0, y - size);
		DVector = new Vector3(x, 0, y - size);
		DRVector = new Vector3(x + size, 0, y - size);

		// Tile positions:
		// |UL|U|UR|
		// |L |C| R|
		// |DL|D|DR|
		// Create each tile as a child of this game object
		GameObject ULtile = Instantiate(tilePrefab, ULVector, Quaternion.identity, this.transform);
		GameObject Utile = Instantiate(tilePrefab, UVector, Quaternion.identity, this.transform);
		GameObject URtile = Instantiate(tilePrefab, URVector, Quaternion.identity, this.transform);
		GameObject Ltile = Instantiate(tilePrefab, LVector, Quaternion.identity, this.transform);
		GameObject Ctile = Instantiate(tilePrefab, CVector, Quaternion.identity, this.transform);
		GameObject Rtile = Instantiate(tilePrefab, RVector, Quaternion.identity, this.transform);
		GameObject DLtile = Instantiate(tilePrefab, DLVector, Quaternion.identity, this.transform);
		GameObject Dtile = Instantiate(tilePrefab, DVector, Quaternion.identity, this.transform);
		GameObject DRtile = Instantiate(tilePrefab, DRVector, Quaternion.identity, this.transform);

		tilePool = new GameObject[]
		{
			ULtile,
			Utile,
			URtile,
			Ltile,
			Ctile,
			Rtile,
			DLtile,
			Dtile,
			DRtile

		};
	}

	void Update()
	{
		// Look at which tile the player is on
		int i = 0;
		bool moving = true;
		for (; i < tilePool.Length; i++)
		{
			if (tilePool[i] != null && InRegion(tilePool[i]))
			{
				moving = false;
				break;
			}
		}

		// Check if the tile is the center tile (tilePool index 4)
		if (i != 4)
		{
			// Checks to make sure the player didn't just exit the for-loop because the player was moving
			if (!moving)
			{
				x = tilePool[i].transform.position.x;
				y = tilePool[i].transform.position.z;

				ULVector = new Vector3(x - size, 0, y + size);
				UVector = new Vector3(x, 0, y + size);
				URVector = new Vector3(x + size, 0, y + size);
				LVector = new Vector3(x - size, 0, y);
				RVector = new Vector3(x + size, 0, y);
				DLVector = new Vector3(x - size, 0, y - size);
				DVector = new Vector3(x, 0, y - size);
				DRVector = new Vector3(x + size, 0, y - size);

				// The player is not in the center region
				// Make new region the center and move tiles around that
				GameObject newCenter = tilePool[i];
				GameObject pastUL = tilePool[0];
				GameObject pastU = tilePool[1];
				GameObject pastUR = tilePool[2];
				GameObject pastL = tilePool[3];
				GameObject pastC = tilePool[4];
				GameObject pastR = tilePool[5];
				GameObject pastDL = tilePool[6];
				GameObject pastD = tilePool[7];
				GameObject pastDR = tilePool[8];

				// New C tile
				tilePool[4] = newCenter;
				TileCreator[] toChange;

				switch (i)
				{
					// Player on UL tile
					case 0:
						toChange = new TileCreator[] {pastDR.GetComponent<TileCreator>(),
													  pastR.GetComponent<TileCreator>(),
													  pastUR.GetComponent<TileCreator>(),
													  pastD.GetComponent<TileCreator>(),
													  pastDL.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UL tile
						tilePool[0] = pastDR;
						// New U tile
						tilePool[1] = pastR;
						// New UR tile same
						// pastUR
						// New L tile
						tilePool[3] = pastD;
						// New DL tile same
						// pastDL

						// Move to the correct location
						tilePool[0].transform.position = ULVector;
						tilePool[1].transform.position = UVector;
						tilePool[2].transform.position = URVector;
						tilePool[3].transform.position = LVector;
						tilePool[6].transform.position = DLVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New R tile
						tilePool[5] = pastU;
						// New D tile
						tilePool[7] = pastL;
						// New DR tile
						tilePool[8] = pastC;
						break;
					// Player on U tile
					case 1:
						toChange = new TileCreator[] {pastDL.GetComponent<TileCreator>(),
													  pastD.GetComponent<TileCreator>(),
													  pastDR.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UL tile
						tilePool[0] = pastDL;
						// New U tile
						tilePool[1] = pastD;
						// New UR tile
						tilePool[2] = pastDR;

						// Move to the correct location
						tilePool[0].transform.position = ULVector;
						tilePool[1].transform.position = UVector;
						tilePool[2].transform.position = URVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New L tile
						tilePool[3] = pastUL;
						// New R tile
						tilePool[5] = pastUR;
						// New DL tile
						tilePool[6] = pastL;
						// New D tile
						tilePool[7] = pastC;
						// New DR tile
						tilePool[8] = pastR;
						break;
					// Player on UR tile
					case 2:
						toChange = new TileCreator[] {pastUL.GetComponent<TileCreator>(),
													  pastL.GetComponent<TileCreator>(),
													  pastDL.GetComponent<TileCreator>(),
													  pastD.GetComponent<TileCreator>(),
													  pastDR.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UL tile same
						// pastUL
						// New U tile
						tilePool[1] = pastL;
						// New UR tile
						tilePool[2] = pastDL;
						// New R tile
						tilePool[5] = pastD;
						// New DR tile same
						// pastDR

						// Move to the correct location
						tilePool[0].transform.position = ULVector;
						tilePool[1].transform.position = UVector;
						tilePool[2].transform.position = URVector;
						tilePool[5].transform.position = RVector;
						tilePool[8].transform.position = DRVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New L tile
						tilePool[3] = pastU;
						// New DL tile
						tilePool[6] = pastC;
						// New D tile
						tilePool[7] = pastR;
						break;
					// Player on L tile
					case 3:
						toChange = new TileCreator[] {pastUR.GetComponent<TileCreator>(),
													  pastR.GetComponent<TileCreator>(),
													  pastDR.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UL tile
						tilePool[0] = pastUR;
						// New L tile
						tilePool[3] = pastR;
						// New DL tile
						tilePool[6] = pastDR;

						// Move to the correct location
						tilePool[0].transform.position = ULVector;
						tilePool[3].transform.position = LVector;
						tilePool[6].transform.position = DLVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New U tile
						tilePool[1] = pastUL;
						// New UR tile
						tilePool[2] = pastU;
						// New R tile
						tilePool[5] = pastC;
						// New D tile
						tilePool[7] = pastDL;
						// New DR tile
						tilePool[8] = pastD;
						break;
					// Player on R tile
					case 5:
						toChange = new TileCreator[] {pastUL.GetComponent<TileCreator>(),
													  pastL.GetComponent<TileCreator>(),
													  pastDL.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UR tile
						tilePool[2] = pastUL;
						// New R tile
						tilePool[5] = pastL;
						// New DR tile
						tilePool[8] = pastDL;

						// Move to the correct location
						tilePool[2].transform.position = URVector;
						tilePool[5].transform.position = RVector;
						tilePool[8].transform.position = DRVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New UL tile
						tilePool[0] = pastU;
						// New U tile
						tilePool[1] = pastUR;
						// New L tile
						tilePool[3] = pastC;
						// New DL tile
						tilePool[6] = pastD;
						// New D tile
						tilePool[7] = pastDR;
						break;
					// Player on DL tile
					case 6:
						toChange = new TileCreator[] {pastUL.GetComponent<TileCreator>(),
													  pastU.GetComponent<TileCreator>(),
													  pastUR.GetComponent<TileCreator>(),
													  pastR.GetComponent<TileCreator>(),
													  pastDR.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UL tile
						// past UL
						// New L tile
						tilePool[3] = pastU;
						// New DL tile same
						tilePool[6] = pastUR;
						// New D tile
						tilePool[7] = pastR;
						// New DR tile same
						// pastDR

						// Move to the correct location
						tilePool[0].transform.position = ULVector;
						tilePool[3].transform.position = LVector;
						tilePool[6].transform.position = DLVector;
						tilePool[7].transform.position = DVector;
						tilePool[8].transform.position = DRVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New U tile
						tilePool[1] = pastL;
						// New UR tile
						tilePool[2] = pastC;
						// New R tile
						tilePool[5] = pastD;
						break;
					// Player on D tile
					case 7:
						toChange = new TileCreator[] {pastUL.GetComponent<TileCreator>(),
													  pastU.GetComponent<TileCreator>(),
													  pastUR.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New DL tile
						tilePool[6] = pastUL;
						// New D tile
						tilePool[7] = pastU;
						// New DR tile
						tilePool[8] = pastUR;

						// Move to the correct location
						tilePool[6].transform.position = DLVector;
						tilePool[7].transform.position = DVector;
						tilePool[8].transform.position = DRVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New U tile
						tilePool[0] = pastL;
						// New UR tile
						tilePool[1] = pastC;
						// New L tile
						tilePool[2] = pastR;
						// New R tile
						tilePool[3] = pastDL;
						// New DL tile
						tilePool[5] = pastDR;
						break;
					// Player on DR tile
					case 8:
						toChange = new TileCreator[] {pastU.GetComponent<TileCreator>(),
													  pastUL.GetComponent<TileCreator>(),
													  pastL.GetComponent<TileCreator>(),
													  pastUR.GetComponent<TileCreator>(),
													  pastDL.GetComponent<TileCreator>()};

						// Remove NPCs on tiles to be moved.
						foreach (TileCreator tile in toChange) { tile.DeactivateNPCs(); }

						// New UR tile
						// pastUR
						// New R tile
						tilePool[5] = pastU;
						// New DR tile
						tilePool[8] = pastUL;
						// New D tile
						tilePool[7] = pastL;
						// New DL tile same
						// pastDL

						// Move to the correct location
						tilePool[2].transform.position = URVector;
						tilePool[5].transform.position = RVector;
						tilePool[6].transform.position = DLVector;
						tilePool[7].transform.position = DVector;
						tilePool[8].transform.position = DRVector;

						// Add image texture to blank ground.
						foreach (TileCreator tile in toChange) { StartCoroutine(tile.SetTexture()); }

						// Place new NPCs on moved tiles.
						foreach (TileCreator tile in toChange) { tile.ActivateNPCs(); }

						//Rename tiles
						// New DL tile
						tilePool[0] = pastC;
						// New D tile
						tilePool[1] = pastR;
						// New DR tile
						tilePool[3] = pastD;
						break;
					default:
						break;

				}
			}
		}
	}

	bool InRegion(GameObject tile)
	{
		// Fetch the Collider from the GameObject this script is attached to
		Collider meshCollider = tile.GetComponent<Collider>();
		// Assign the point to be that of the Transform you assign in the Inspector window
		Vector3 playerPoint = player.transform.position;
		// See if on top of the mesh
		playerPoint.y = 0;
		// If the first GameObject's Bounds contains the Transform's position, return true
		if (meshCollider.bounds.Contains(playerPoint))
		{
			return true;
		}
		// Else, false
		return false;
	}
}
