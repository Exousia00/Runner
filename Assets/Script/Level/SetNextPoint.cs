using UnityEngine;
using System.Collections;

public class SetNextPoint : MonoBehaviour {

	public GameObject next;
	Forward forward;

	void Start () {
		forward = GameObject.Find("Player/Camera").GetComponent<Forward>();
	}
	void OnTriggerEnter (Collider other){
		if (other.tag == "Player"){
			if (next == null){
				transform.parent.GetComponent<TurnPlayer>().shouldTurn = false;
				forward.speed = 0.3f;
				return;
			}
			transform.parent.GetComponent<TurnPlayer>().setPlayer = next;

		}
	}
}
