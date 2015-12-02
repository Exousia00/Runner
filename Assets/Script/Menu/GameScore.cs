using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour {
	public int score = 0;
	int oldScore = 0;
	GameObject player;
	public AudioClip deathSound;
	public AudioClip orbSound;
	Text text;
	bool setActive = false;
	float dur = 50.0f;
	GameObject part;
	ParticleSystem parts;

	void Start ()
	{
		Application.targetFrameRate = 30;
		part = GameObject.Find("Score Particle");
		parts = part.GetComponent<ParticleSystem>();
		text = GetComponent<Text>();
		player = GameObject.Find ("Player");
	}
	

	void Update () {

		if (text == null) {
			return;
		}

		text.text = "Score: " + oldScore.ToString ();
		if (setActive){
			StartCoroutine("Stop");
			setActive = false;
			parts.emissionRate = 0;
		}
		if (!player) {
			GetComponent<AudioSource>().PlayOneShot(deathSound, 0.1f);
		}
		if (oldScore < score) {
			setActive = true;
			parts.emissionRate = dur;
			PlaySound();

			oldScore = score;


		}
	}



	void PlaySound () {

		GetComponent<AudioSource>().PlayOneShot (orbSound, 0.3f);

	}

	IEnumerator Stop() {

		yield return new WaitForSeconds(0.1f);

	}

}
