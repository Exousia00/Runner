using UnityEngine;
using System.Collections;

public class Obsticale : MonoBehaviour {

	Forward forward;
	Player pScript;
	GameObject parent;
	GameObject player;

	public bool slideUnder = false;

	// Use this for initialization
	void Start () {

		parent = GameObject.Find ("Player/Camera");
		player = GameObject.Find ("Player");
		forward = parent.GetComponent<Forward>();
		pScript = player.GetComponent<Player>();

	}


	void OnTriggerEnter(Collider other){
		
		
		if (other.name == "Player") {
			if (slideUnder && pScript.slide){
				return;
			}
			Destroy (GameObject.Find("warrior2swords"));
			Time.timeScale = 0.0f;
			forward.speed = 0.0f;
		}

	}

}
