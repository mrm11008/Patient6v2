using UnityEngine;
using System.Collections;

public class RobotMovement : MonoBehaviour {

	private enum STATE { WAIT, CHASE, PATROL, INVESTIGATE};
	STATE currentState;

	public Transform[] path;
	public float speed = 1.0f;
	public float speedRotate = 1.0f;
	public float reachDist = 10.0f;
	public int currentPoint = 0;

	public bool inSight = false;
	public bool hiddenChecker = false;
	public bool wait = false;
	public bool readyToMove = true;
	public bool readyToRotate = true;
	public bool investigate = false;
	public float distanceToSee = 15.0f;
	public float distanceToAttack = 3.0f;

	RaycastHit whatISee;
	RaycastHit whatIHit;

	private float _time;
	public float _angle;
	public float _period;


	public Transform target;
	public Transform myTransform;
	public Transform targetPath;

	public Quaternion newRot;
	public Vector3 currRot;
	public Vector3 distance;
	public float minAngle = -45f;
	public float maxAngle = 45f;

	public RobotSounds robotsound;
	private bool playSoundOne = true;
	private bool playSoundTwo = true;
//	Quaternion rotI = new Quaternion(0f, 30f, 0f);
	// Use this for initialization
	void Start () {
		robotsound = gameObject.GetComponent<RobotSounds> ();
	}
	
	// Update is called once per frame
	void Update () {
		//New State Code
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




		//-----------------------------------OLD CODE, REPLACE WITH STATES-------------------------------------
		Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.blue);
		Debug.DrawRay (this.transform.position, this.transform.forward * distanceToAttack, Color.red);

		//If the player is withing sight
		if (Physics.Raycast (this.transform.position, this.transform.forward, out whatISee, distanceToSee)) {
			Debug.Log (whatISee.collider.tag);
			if (whatISee.collider.tag == "Player") {
				inSight = true;

				if (playSoundOne == true) {
					robotsound.playFoundSound ();
					playSoundOne = false;
				}

				Debug.Log ("I SEE THE PLAYER");
			} 
		}

		if (Physics.Raycast (this.transform.position, this.transform.forward, out whatIHit, distanceToAttack)) {
			if (whatIHit.collider.tag == "Player") {
				if (playSoundTwo == true) {
					robotsound.playDartSound ();
					playSoundTwo = false;
				}
				Debug.Log ("DART!!");
				GM.instance.dartPlayer ();
			}
		}

		if (inSight == true) {
			transform.LookAt (target);
			transform.Translate (Vector3.forward * 1 * Time.deltaTime);
			//If the player is in attack range

		}

		if (inSight == false && wait == true) {
			
		}
		if (inSight == false) {
			float dist = Vector3.Distance (path [currentPoint].position, transform.position);

			//ROTATE BEFORE MOVE....WORKS-------
			if (readyToMove == true && wait == false) {
				transform.position = Vector3.Lerp (transform.position, path [currentPoint].position, Time.deltaTime * speed);
			}
//			rotate ();
			//---------------------------


			if (investigate == false) {
					rotate ();

			}

		


			if (dist <= reachDist) {
				wait = true;
				investigate = true;

				currentPoint++;
				//THIS WORKS FOR WAITING 8 SECONDS AT EACH POSITION
				Invoke ("waitCycle", 12.0f);
				Invoke ("investigateCycle", 7.0f);
//				Invoke ("rotateCycleTrue", 7.0f);
//				Invoke ("rotateCycleFalse", 9.0f);


//				Invoke ("moveCycle", 10.0f);
				Debug.Log ("REACHED POSITION");
				readyToMove = false;
//				
			}

			if (currentPoint >= path.Length) {
				currentPoint = 0;
			}

		}

		if (inSight == false) {
			if (wait == true) {

				if (investigate == true) {
					Debug.Log (path [currentPoint]);

					if (path [currentPoint - 1].name == "Destination2" || path [currentPoint - 1].name == "Destination5") {
						//POSSIBLY DO A SLOW 360 TURN
						//WHEN AT HALLFWAY POSITIONS
					} else {
						Debug.Log ("INVESTIGATING");
//						transform.rotation = Quaternion.Euler (0f, transform.rotation.eulerAngles.y + 1 * Mathf.Sin (Time.time * 1), 0f);
						_time = _time + Time.deltaTime;
						float phase = Mathf.Sin (_time / 0.5f);
						transform.rotation = Quaternion.Euler (0f, transform.rotation.eulerAngles.y + phase * 1.78f, 0f);
//						transform.rotation = Quaternion.Lerp(
//						transform.rotation *= Quaternion.Euler(0,90,0);
//						var adjustRotation = transform.rotation.y + 
//						transform.rot

					}
					//
					//				transform.rotation = Quaternion.Euler (0f, transform.rotation.eulerAngles.y + 1 * Mathf.Sin (Time.time * 1), 0f);


					//				transform.rotation = Quaternion.Euler (0f, transform.rotation.eulerAngles.y + 1 * Mathf.Sin (Time.time * 1), 0f);
					//				investigate = false;
					//				Invoke ("investigateCycle", 2.0f);


				} 

			}
		}



		hiddenChecker = GM.instance.hiddenCheck ();
		if (hiddenChecker == true) {
			inSight = false;
		}

	}


	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Player") {
			GM.instance.dartPlayer ();
			inSight = true;
		}
	}

	void incrementPoint() {
		currentPoint++;
	}
	void waitCycle() {
		wait = false;
	}
			
	void onDrawGizmos() {
		if (path.Length > 0)
			for (int i = 0; i < path.Length; i++) {
				if (path [i] != null) {
					Gizmos.DrawSphere (path [i].position, reachDist);
				}
			}
	}

	void rotate() {
		Debug.Log ("Rotate to next point");
		distance = path [currentPoint].position - transform.position;
		newRot = Quaternion.LookRotation (distance, transform.up);
//		newRot.x = 0;
//		newRot.z = 0;
		transform.rotation = Quaternion.Lerp (transform.rotation, newRot, speedRotate * Time.deltaTime);
//		if (transform.rotation == newRot) {

		if (transform.rotation == newRot) {
			Debug.Log("I am looking at the next point!!!");
//			readyToMove = true;
//			Invoke ("moveCycle", 1.0f);
		}

		Invoke ("moveCycle", 5.0f);
	}

	void moveCycle() {
		readyToMove = true;
	}

	void investigateCycle() {
		investigate = false;
		readyToRotate = true;
	}
	void investigateCycleTrue() {
		investigate = true;
	}
	void rotateCycleTrue() {
		readyToRotate = true;
	}
	void rotateCycleFalse() {
		readyToRotate = false;
	}

	public void toggleInSight() {
		inSight = false;
	}




	//---------------------------------Add New State functions------------------------------------------

	private void EnterWaitState() {

	}
	private void UpdateWait() {

	}

	private void EnterInvestigateState() {

	}
	private void UpdateInvestigate(){
	}

	private void EnterChaseState() {

	}

	private void UpdateChase() {

	}

	private void EnterPatrolState() {

	}

	private void UpdatePatrol() {

	}

}
