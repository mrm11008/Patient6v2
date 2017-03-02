using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDetectRange : MonoBehaviour {
	public GameObject player;
	public RobotMovement parentR;
	// Use this for initialization
	void Start () {
		parentR = GetComponentInParent<RobotMovement> ();

	}

//	void OnCollisionEnter(Collision col) {
//		if (col.gameObject.name == "Player") {
//
//
////			GM.instance.dartPlayer ();
////			inSight = true;
//			Debug.Log ("ROBOT FOUND");
////			parentR.foundPlayer ();
//
//		}
//	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("TRIGGER");
		if (col.gameObject.name == "Player") {


//			GM.instance.dartPlayer ();
//			inSight = true;
			Debug.Log ("ROBOT FOUND");
			parentR.foundPlayer ();

		}
	}
}
