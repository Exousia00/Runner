using UnityEngine;
using System.Collections;

public class OrbSpawner : MonoBehaviour {


	public GameObject[] oSpawn = new GameObject[3];
	public GameObject orb;
	OrbSpawner orbSpawner;
	int rand;
	int i = 0;
	bool spawned = false;
	// Use this for initialization
	void Start () {
		orbSpawner = GetComponent<OrbSpawner>();
		orb = (GameObject)Resources.Load ("Orb");
	}
	
	// Update is called once per frame
	void Update () {

		PickSpawner();

	}

	void Spawned() {

		rand = Random.Range (0, 2);

		GameObject go = (GameObject)Instantiate (Resources.Load (orb.name), oSpawn[rand].transform.position, Quaternion.identity);

	}

	void PickSpawner () {

		foreach (Transform child in transform)
		{
			if (child.name == "OrbSpawner" && i <=  (oSpawn.Length - 1)){
				oSpawn[i] = child.gameObject;
				//Debug.Log ("oSpawn: " + oSpawn[i].name);
				i++;
			}
			else if ( !spawned ){ 
				Spawned(); 
				spawned = true;
				for( int g = 0; g <= (oSpawn.Length - 1); g++){
					Destroy(oSpawn[g], 0.5f);
				}
				Destroy(orbSpawner, 1.0f);
			}

		}

	}
}
