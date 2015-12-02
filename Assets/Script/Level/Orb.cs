using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {
	GameScore gScore;
	GameObject game;
	// Use this for initialization
	void Start () {
		game = GameObject.Find ("GameScore");
		gScore = game.GetComponent<GameScore>();
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 50.0f);
	}
	
	void OnTriggerEnter(Collider other){
		
		if (other.name == "Player") {
			gScore.score++;
			
			Destroy(gameObject);
			
		}
	}
	
}
