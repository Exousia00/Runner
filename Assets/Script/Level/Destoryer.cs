using UnityEngine;
using System.Collections;

public class Destoryer : MonoBehaviour {

	SpawnParent spawnParent;
	GameObject game;


	void Start () {

		game = GameObject.Find ("Game");
		spawnParent = game.GetComponent<SpawnParent>();

	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			Destroy (gameObject, 5);
			spawnParent.spawnCount--;
		}

	}


}
