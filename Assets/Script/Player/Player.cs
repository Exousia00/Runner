using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxMove ;
	public float speed = 0.4f;
	Vector3 left = new Vector3();
	Vector3 right = new Vector3();
	bool leftBool = false;
	bool rightBool = false;
	bool jumping = false;
	public bool slide = false;
	CharacterController cc;
	CharacterMotor cm;

	public int lane = 0;		// 1 = left lane, 0 = middle lane, -1 = right lane
	bool touchEnabled = false;


	// TouchInput

	public float minMovement = 20.0f;
	private Vector2 StartPos;
	private int SwipeID = -1;


	void Start () {
		cc = gameObject.GetComponent<CharacterController>();
		cm = gameObject.GetComponent<CharacterMotor>();
		touchEnabled = Application.isMobilePlatform;
	}
	

	void Update () {

		if (touchEnabled){
			TouchInput ();
		}else {PlayerMoveInput ();}

		if (!GetComponent<Animation>().isPlaying) {
			jumping = false;
			GetComponent<Animation>().Play ("run");
		}
		if (GetComponent<Animation>()["slide"].enabled == false && slide){
			slide = false;
		}
		Move ();
	}

	void PlayerMoveInput () {

		if (Input.GetKeyDown (KeyCode.A) && lane != 1){
			left = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + maxMove);
			leftBool = true;
		}
		if (Input.GetKeyDown (KeyCode.D) && lane != -1){
			right = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - maxMove);
			rightBool = true;
		}
		if (Input.GetKeyDown(KeyCode.S) && jumping == false){
			slide = true;
			GetComponent<Animation>().Blend ("slide", 70f);
		}
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)){
			jumping = true;
			GetComponent<Animation>().Play ("jump");
		}

	}

	void Move () {

		if (leftBool == true){
			float step = speed * Time.deltaTime;
		
			while( transform.localPosition.z <= left.z ){
				transform.Translate(Vector3.left * speed, Space.Self);

				return;
			}
			lane++;
			LanePos ();

		}
		
		if (rightBool == true){
			float step = speed * Time.deltaTime;
			
			while( transform.localPosition.z >= right.z ){
				transform.Translate(Vector3.right * speed, Space.Self);

				return;
			}
			lane--;
			LanePos ();
		}

	}

	void LanePos () {

		if (lane == 0){
			transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, 0);
		}
		if (lane == -1){
			transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, -maxMove);
		}
		if (lane == 1){
			transform.localPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, maxMove);
		}
		leftBool = false;
		rightBool = false;
	}

	void TouchInput () {
		
		foreach (var T in Input.touches)
		{
			var P = T.position;
			if (T.phase == TouchPhase.Began && SwipeID == -1)
			{
				SwipeID = T.fingerId;
				StartPos = P;
			}
			else if (T.fingerId == SwipeID)
			{
				var delta = P - StartPos;
				
				if (T.phase == TouchPhase.Moved && delta.magnitude > minMovement){
					
					SwipeID = -1;
					if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)){
						
						if (delta.x > 0 &&  lane != -1){
							
							right = new Vector3(transform.position.x, transform.position.y, transform.localPosition.z - maxMove);
							rightBool = true;
						}
						
						if (delta.x < 0 && lane != 1){
							
							left = new Vector3(transform.position.x, transform.position.y, transform.localPosition.z + maxMove);
							leftBool = true;
						}
					}
					else
					{
						if (delta.y > 0 && jumping == false){
							
							jumping = true;
							cm.inputJump = true;
							GetComponent<Animation>().Play ("jump");
						}
						else if (delta.y < 0 && jumping == false){
							
							slide = true;
							GetComponent<Animation>().Blend ("slide", 70f);
						}
					}
				}
				else if (T.phase == TouchPhase.Canceled || T.phase == TouchPhase.Ended){
					
					SwipeID = -1;
				}
			}
		}
		
	}
	
	
}
