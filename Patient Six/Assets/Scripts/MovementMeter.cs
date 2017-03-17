using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MovementMeter : MonoBehaviour {

	public Text movementMeterValues;
	public int movementFloat = 100;
	public int decrementValue = 1;
	public bool movementTrigger = false;

	void Awake() {

	}
	void onEnable() {
//		Debug.Log ("METER HAS AWOKEN");
		InvokeRepeating ("decrementMovement", 1.0f, 2.0f);

	}
	// Use this for initialization
	void Start () {
//		Debug.Log ("METER HAS AWOKEN");

		InvokeRepeating ("decrementMovement", 1.0f, 2.0f);

	}
	
	// Update is called once per frame
	void Update () {
		movementMeterValues.text = "Movement: " + movementFloat;
	}

	void decrementMovement() {
//		Debug.Log ("DECREMENT");
		movementFloat = movementFloat - decrementValue;
//		Debug.Log (movementFloat);
	}
	public void incrementMovement() {
		if (movementFloat <= 100) {
			if (movementFloat + 25 >= 100) {
				movementFloat = 100;
			} else {
				movementFloat = movementFloat + 25;
			}
		}

	}
	public void dartDecrementMovement() {
		//		soundFloat = soundFloat - 50;

		if (movementFloat - 50 <= 0) {
			movementFloat = 0;
		} else {
			movementFloat = movementFloat - 50;
		}


	}

	public void TutorialSet() {
		movementFloat = 75;
	}


	public bool movementSwitch() {
		return movementTrigger;
	}

	public int getMovementValue() {
		return movementFloat;
	}



}
