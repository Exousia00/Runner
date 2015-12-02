using UnityEngine;
using System.Collections;

public class SpawnGround : MonoBehaviour {

	SpawnParent spawnParent;
	GameObject game;
	bool spawned = false;
	int rand;
	GameObject parent;
	TurnPlayer turn;
	GameObject go;


	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
		spawnParent = game.GetComponent<SpawnParent>();
		parent = gameObject.transform.parent.gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if (spawnParent.spawn == true && spawned == false){
			rand = Random.Range (0, spawnParent.grounds.Length);

			if ( spawnParent.leftTurns <= 0 && rand == 10){
				spawnParent.leftTurns++;
				spawnParent.rightTurns--;
			}
			else if (spawnParent.rightTurns <= 0 && rand == 11){
				spawnParent.leftTurns--;
				spawnParent.rightTurns++;
			}

			FloorInst();
			spawnParent.spawnCount++;
			spawnParent.spawn = false;
			spawned = true;
			Destroy(gameObject, 1.0f);
		}
	}

	void FloorInst () {

		if (spawnParent.breather) 
		{
			if (parent.GetComponent<TurnPlayer>())
			{TurnRotation();}
			else{
				RotObject(0);
				spawnParent.spawnedbreak++;
			}
			return;
		} else {
			if (parent.GetComponent<TurnPlayer>())
			{TurnRotation();}
			else{
				spawnParent.breath++;
				go = (GameObject)Instantiate (Resources.Load (spawnParent.grounds [rand].name), transform.position, Quaternion.identity);
				go.transform.rotation =  Quaternion.Euler(parent.transform.rotation.eulerAngles.x, parent.transform.rotation.eulerAngles.y , parent.transform.rotation.eulerAngles.z);
				LeftTurnPos (go);
			}	
		}

	}

	void RotObject (int i){
	
		go = (GameObject)Instantiate (Resources.Load (spawnParent.defaultGround.name), transform.position, Quaternion.identity);
		go.transform.rotation =  Quaternion.Euler(parent.transform.rotation.eulerAngles.x, parent.transform.rotation.eulerAngles.y - i, parent.transform.rotation.eulerAngles.z);
		LeftTurnPos (go);

	}

	void LeftTurnPos (GameObject other){
		
		if ( other.name == "GroundTurnL" || other.name == "GroundTurnL(Clone)"){
			other.transform.rotation = Quaternion.Euler( 0 , parent.transform.rotation.eulerAngles.y + 90, 0);
		}
	}

	void TurnRotation(){

		turn = parent.GetComponent<TurnPlayer>();

		if (turn.turnDir == LEFT){
			RotObject(-90);
		}
		else if (turn.turnDir == RIGHT){
			RotObject(90);
		}
		spawnParent.spawnedbreak++;
	}



    public TurnPlayer.TurnDirection LEFT { get; set; }

    public TurnPlayer.TurnDirection RIGHT { get; set; }
}
