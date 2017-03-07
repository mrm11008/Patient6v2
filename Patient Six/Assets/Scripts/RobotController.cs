using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour {
	private enum STATE { WAIT, CHASE, PATROL, INVESTIGATE, RECALL};
	STATE currentState;

	RaycastHit whatISee;
	RaycastHit whatIHit;

//	public Vector3 startPosition = new Vector3 (-1.5f, 0.5f, 86f);

	public float speed = 1.0f;
	public float speedRotate = 1.0f;
	public float reachDist = 1.0f;
	public int currentPoint = 0;

	public Transform[] path;
	public Transform target;
	public Transform myTransform;
	public Transform targetPath;
	public Vector3 distance;
	public float distanceToSee = 15.0f;
	public float distanceToAttack = 3.0f;

	public Quaternion newRot;
	public Vector3 currRot;

	private float _time;
	public float _angle;
	public float _period;

	//TIMERS
	private float investigateTimer = 8.0f;
	private float waitTimer = 4.0f;

	//SOUNDS
	public RobotSounds robotsound;
	private bool playSoundOne = true;
	private bool playSoundTwo = true;

	//NAV MESH
	private NavMeshAgent myAgent;
	private Transform goal;
	private Vector3 currentRotation;

	public bool playerHidden = false;
	public characterController player;

	// Use this for initialization
	void Start () {
		robotsound = gameObject.GetComponent<RobotSounds> ();
		currentPoint = 1;
//		gameObject.transform.position = startPosition;
		EnterInvestigateState ();
		myAgent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case STATE.WAIT:
			UpdateWait ();
			break;
		case STATE.CHASE:
			UpdateChase ();
			break;
		case STATE.PATROL:
			UpdatePatrol ();
			break;
		case STATE.INVESTIGATE:
			UpdateInvestigate ();
			break;
		}

		playerHidden = player.CheckHidden ();
		if (playerHidden == true) {
			//ENTER A STATE FOR WHAT THE ROBOT DOES WHEN THE PLAYER GETS INTO A HIDING SPOT
		} else {
		}

		Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.blue);
		Debug.DrawRay (this.transform.position, this.transform.forward * distanceToAttack, Color.red);

		//THE PLAYER IS IN SIGHT
		if (Physics.Raycast (this.transform.position, this.transform.forward, out whatISee, distanceToSee)) {
//			Debug.Log (whatISee.collider.tag);
			if (whatISee.collider.tag == "Player") {
				if (playSoundOne == true) {
					robotsound.playFoundSound ();
					playSoundOne = false;
				}
				EnterChaseState ();
				Debug.Log ("I SEE THE PLAYER");
			} 
		}
		//THE PLAYER IS WITHIN ATTACKING DISTANCE
		if (Physics.Raycast (this.transform.position, this.transform.forward, out whatIHit, distanceToAttack)) {
			if (whatIHit.collider.tag == "Player") {
//				Debug.Log ("DART!!");
				if (playSoundTwo == true) {
					robotsound.playDartSound ();
					GM.instance.dartSound ();
					playSoundTwo = false;
				}
				GM.instance.dartPlayer ();
				EnterRecallState ();
			}
		}


		if (currentPoint >= path.Length) {
			currentPoint = 0;
		}

	}

	//---------------------------------Add New State functions------------------------------------------

	private void EnterWaitState() {
		currentState = STATE.WAIT;
	}
	private void UpdateWait() {
		//REPOSITION ROBOT BEFORE MOVING ONWARDS
		waitTimer -= Time.deltaTime;

		rotate ();

		if (waitTimer <= 0) {
			waitTimer = 4.0f;

			EnterPatrolState ();
		}

	}

	private void EnterInvestigateState() {
		currentState = STATE.INVESTIGATE;
//		Debug.Log (" ENTERING INVESTIGATING STATE");
	}

	private void UpdateInvestigate(){
		investigateTimer -= Time.deltaTime;

//		if (path [currentPoint - 1].name == "Destination2" || path [currentPoint - 1].name == "Destination5") {
//			//POSSIBLY DO A SLOW 360 TURN
//			//WHEN AT HALLFWAY POSITIONS
//		} else {
//			Debug.Log ("INVESTIGATING");
//			//ROTATE BACK AND FORTH
//			_time = _time + Time.deltaTime;
//			float phase = Mathf.Sin (_time / 0.5f);
//			transform.rotation = Quaternion.Euler (0f, transform.rotation.eulerAngles.y + phase * 1.78f, 0f);
//		}


//		Debug.Log ("INVESTIGATING");
		//ROTATE BACK AND FORTH
		_time = _time + Time.deltaTime;
		float phase = Mathf.Sin (_time / 0.5f);
		transform.rotation = Quaternion.Euler (0f, transform.rotation.eulerAngles.y + phase * 1.78f, 0f);



		//AFTER CERTAIN AMOUNT OF TIME, ENTER WAIT 
		if (investigateTimer <= 0) {
			investigateTimer = 8.0f;
//			currentPoint++;
			EnterWaitState ();
		}

	}

	private void EnterChaseState() {
		currentState = STATE.CHASE;
	}

	private void UpdateChase() {
		transform.LookAt (target);
//		transform.Translate (Vector3.forward * 1 * Time.deltaTime);
		myAgent.destination = target.position;
	}

	private void EnterPatrolState() {
//		Debug.Log (" ENTERING PATROL STATE");

		currentState = STATE.PATROL;
	}

	private void UpdatePatrol() {
		float dist = Vector3.Distance (path [currentPoint].position, transform.position);
//		transform.position = Vector3.Lerp (transform.position, path [currentPoint].position, Time.deltaTime * speed);
		goal = path[currentPoint].transform;
		myAgent.destination = goal.position;
		if (dist <= reachDist) {
//			Debug.Log ("REACHED MY POSITION");
			currentPoint++;
			EnterInvestigateState ();
		}

	}

	private void EnterRecallState() {
		currentState = STATE.RECALL;
	}

	private void UpdateRecall() {

	}

	void rotate() {
//		Debug.Log ("Rotate to next point");
//		print (currentPoint);

//		currentRotation = transform.eulerAngles;
//		distance = path [currentPoint].position - transform.position;
//		 
////		currentRotation.y = Mathf.Lerp (currentRotation.y, distance.y, speedRotate * Time.deltaTime);
//		currentRotation.y = Mathf.Lerp (currentRotation.y, distance.y, speedRotate * Time.deltaTime);
//
//		transform.eulerAngles = currentRotation;
	



		distance = path [currentPoint].position - transform.position;
//		newRot = Quaternion.LookRotation (distance, transform.up);
		newRot = Quaternion.LookRotation (new Vector3 (distance.x, distance.y, distance.z));

		currentRotation = newRot.eulerAngles;
		currentRotation.z = 0f;
		currentRotation.x = 0f;
		newRot = Quaternion.Euler (currentRotation);
		transform.rotation = Quaternion.Lerp (transform.rotation, newRot, speedRotate * Time.deltaTime);





		if (transform.rotation == newRot) {
//			Debug.Log("I am looking at the next point!!!");

		}
	}



	//VARIOUS HELPER FUNCTIONS
	public void placeRobotAtLocation() {
		currentPoint = 4;
	}
	public void dartReset() {
		playSoundOne = true;
		playSoundTwo = true;
	}
	public void foundPlayer() {
		EnterChaseState ();
		if (playSoundOne == true) {
			robotsound.playFoundSound ();
			playSoundOne = false;
		}

		Debug.Log ("I SEE THE PLAYER");
	}
}
