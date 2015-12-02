using UnityEngine;
using System.Collections;

public class SpawnParent : MonoBehaviour {

	public int spawnCount = 0;
	public int maxGround;
	public bool spawn = true;
	public int breath;
	public int spawnedbreak;
	public int maxOb;
	public int maxSpawned;
	public bool breather = false;
	public GameObject[] grounds;
	public GameObject defaultGround;
	public int leftTurns = 0;
	public int rightTurns = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeScale >= 1.8f) {
			maxSpawned = 0;
		}
		else if (Time.timeScale >= 1.2f) {
			maxSpawned = 1;
		}

		if (spawnedbreak >= maxSpawned){
			breather = false;
			spawnedbreak = 0;
		}

		if (breath >= maxOb){
			breather = true;
			breath = 0;
		}

		if (maxGround > spawnCount ){
			spawn = true;
		}


	}
}
