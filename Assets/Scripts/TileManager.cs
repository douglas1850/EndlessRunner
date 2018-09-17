using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject[] tilePrefabs; //array of prefabs that can be spawned

	private Transform playerTransform; //keep track of player position

	private float spawnZ = -8.0f; //original spawn, future tile spawns added to this
	private float spawnX = 2.0f;
	private float tileLength = 16.0f;
	private float safeZone = 17.0f;

	private int tilesOnScreen = 7;
	private int lastTileIndex = 0;

	private List<GameObject> activeTiles;

	// Use this for initialization
	void Start () {
		activeTiles = new List<GameObject>();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform; //track player movement

		for (int i = 0; i < tilesOnScreen; i++) { //if there's less than 7 tiles on screen, spawn more
			if (i < 2) {
				SpawnTile (0); //spawn floor without obstacles for first two floors
			} else {
				SpawnTile ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength)) { //delete tiles after player passes them
			SpawnTile ();
			DeleteTile ();
		}
	}

	//spawn a tile
	private void SpawnTile(int prefabIndex = -1)
	{
		GameObject go;
		if (prefabIndex == -1) {
			go = Instantiate (tilePrefabs [RandomPrefabIndex ()]) as GameObject;
		} else {
			go = Instantiate (tilePrefabs [prefabIndex]) as GameObject;
		}
		go.transform.SetParent (transform);
		go.transform.position = Vector3.left * spawnX;
		go.transform.position = Vector3.forward * spawnZ;

		spawnZ += tileLength;

		activeTiles.Add (go); //add to list so it can be deleted later
	}

	private void DeleteTile()
	{
		Destroy (activeTiles [0]);
		activeTiles.RemoveAt (0);
	}

	private int RandomPrefabIndex()
	{
		if (tilePrefabs.Length <= 1)
			return 0;

		int randomIndex = lastTileIndex;
		while (randomIndex == lastTileIndex) {
			randomIndex = Random.Range (0, tilePrefabs.Length);
		}

		lastTileIndex = randomIndex;
		return randomIndex;
	}
}
