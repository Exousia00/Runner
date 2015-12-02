using UnityEngine;
using System.Collections;

public class TurnPlayer : MonoBehaviour {

    public enum TurnDirection
    {
        LEFT,
        RIGHT
    }

    float currentYaw;
    public float desiredYaw;
    public bool shouldTurn;
    public TurnDirection turnDir;
	public bool firstTime = true;
	Player playerScript;
    float oldYRot;
	public GameObject setPlayer;
	Forward forward;
	float dist = 0.0f;

	void Start () {
		setPlayer = transform.FindChild("SetPlayer 1").gameObject;
		playerScript = GetPlayer().GetComponent<Player>();
		forward = GetPlayer().GetComponent<Forward>();
	}

   
	
	// Update is called once per frame
	void Update () {
        if (shouldTurn)
        {
			forward.speed = 0;
            UpdateTurning();
        }
	}

	public GameObject GetPlayer(){
		return GameObject.Find("Player/Camera");
	}

    void StartTurning() {

        desiredYaw = currentYaw + (turnDir == LEFT ? -90 : 90);
    }

    public void UpdateTurning(){

		dist = Vector3.Distance(GetPlayer().transform.position, new Vector3 (setPlayer.transform.position.x, GetPlayer().transform.position.y, setPlayer.transform.position.z));

		if (dist >= 0.4){
			GetPlayer().transform.position = Vector3.MoveTowards(GetPlayer().transform.position, new Vector3(setPlayer.transform.position.x,GetPlayer().transform.position.y, setPlayer.transform.position.z)
	              , 15 * Time.deltaTime);
		}
			
		if (turnDir == LEFT){
			currentYaw -= Time.deltaTime * GetTurnRate();
			if (currentYaw <= desiredYaw || currentYaw == desiredYaw){
				GetPlayer().transform.rotation = Quaternion.Euler(GetPlayer().transform.rotation.eulerAngles.x, desiredYaw, GetPlayer().transform.rotation.eulerAngles.z);
				return;
			}
			while (currentYaw >= desiredYaw){
				GetPlayer().transform.rotation = Quaternion.Euler(GetPlayer().transform.rotation.eulerAngles.x, currentYaw, GetPlayer().transform.rotation.eulerAngles.z);
				//Debug.Log("CurrentYaw = " + currentYaw + "  DesiredYaw = " + desiredYaw);
				return;
			}

		}else{
        	currentYaw += Time.deltaTime * GetTurnRate();
			if (currentYaw >= desiredYaw || currentYaw == desiredYaw){
				GetPlayer().transform.rotation = Quaternion.Euler(GetPlayer().transform.rotation.eulerAngles.x, desiredYaw, GetPlayer().transform.rotation.eulerAngles.z);
				return;
			}
			while (currentYaw <= desiredYaw){
				GetPlayer().transform.rotation = Quaternion.Euler(GetPlayer().transform.rotation.eulerAngles.x, currentYaw, GetPlayer().transform.rotation.eulerAngles.z);
				//Debug.Log("CurrentYaw = " + currentYaw + "  DesiredYaw = " + desiredYaw);
				return;
			}
		}


    }

    float GetTurnRate(){
        // return degrees per second that you should be turning

        return 33.0f;
    }

	void OnTriggerEnter (Collider other) { // there is two triggers on this object in the beginning to start the turn, and the end to stop it from turning

		if (other.name == "Player") {
			currentYaw = GetPlayer().transform.eulerAngles.y;       
			if (!shouldTurn)
            {
				Debug.Log("CurrentYaw = " + currentYaw);
                shouldTurn = true;
                StartTurning();
            }
        }
	}
    
    public TurnDirection RIGHT { get; set; }

    public TurnDirection LEFT { get; set; }
}
