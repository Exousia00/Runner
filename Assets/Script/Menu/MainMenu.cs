using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	GameObject player;
	GameObject died;
	Forward frwd;
	float frwdMovementSpeed;
	float tScale;
	GameScore gameScore;
	public bool GameOver;
	public bool Pause;

	public bool helper = false;
	GameObject controls;
	GameObject goControls;
	GameObject death;

	GameObject pauseControl;

	void Start () {


		controls = GameObject.Find ("Controls");
		controls.SetActive (false);

		if (Application.loadedLevelName == "TESTER"){
			player = GameObject.Find("Player/Camera");
			died = GameObject.Find("Player");
			death = GameObject.Find("warrior2swords");
			frwd = player.GetComponent<Forward>();
			gameScore = player.GetComponent<GameScore>();
			frwdMovementSpeed = frwd.speed;

			goControls = GameObject.Find ("GameOver_Controls");
			goControls.SetActive (false);

			pauseControl = GameObject.Find("Pause_Button");
			pauseControl.SetActive (Application.isMobilePlatform);

			Cursor.visible = false;
		}



	}

	void Update () {

		if (Application.loadedLevelName == "TESTER"){
			GameScene ();
		}


		controls.SetActive (helper);


	}

	public void GameScene () {
		if ( death == null){
			goControls.SetActive(true);
			Cursor.visible = true;
		}
		else if ( Input.GetKeyDown(KeyCode.Escape)){
			HelpInfo();
		}
	}


	public void StartGame (){

		Application.LoadLevel ("TESTER");
	}

	public void HelpInfo (){

		if (helper) {
			helper = false;
			if (Application.loadedLevelName == "TESTER"){
				Time.timeScale = tScale;
				frwd.speed = frwdMovementSpeed;
				Cursor.visible = false;
			}
		} else{
			helper = true;
			if (Application.loadedLevelName == "TESTER"){
				Cursor.visible = true;
				tScale = Time.timeScale;
				Time.timeScale = 0.0f;
				frwdMovementSpeed = frwd.speed;
				frwd.speed = 0.0f;
				
			}
		}
	}

	public void QuitGame (){

		Application.Quit ();

	}

	public void Menu (){

		Application.LoadLevel ("Menu");

	}
}
