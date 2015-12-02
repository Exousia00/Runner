using UnityEngine;

using System.Collections;

public class Forward : MonoBehaviour {

	public float speed;        			// over all speed	

	void Start () {
		Time.timeScale = 1.0f;
	}

	void Update () {

		transform.Translate(Vector3.right * (speed + Time.deltaTime) , Space.Self);
	}
}
