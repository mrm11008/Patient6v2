using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundMeter : MonoBehaviour {


	public Text soundMeterValues;
	public int soundFloat = 100;
	public int decrementValue = 1;
	public bool soundTrigger = false;

	void Awake() {

	}
	void onEnable() {
		Debug.Log ("METER HAS AWOKEN");
		InvokeRepeating ("decrementSound", 1.0f, 2.0f);

	}
	// Use this for initialization
	void Start () {
		Debug.Log ("METER HAS AWOKEN");

		InvokeRepeating ("decrementSound", 1.0f, 2.0f);

	}

	// Update is called once per frame
	void Update () {
		soundMeterValues.text = "Sound: " + soundFloat;
	}

	void decrementSound() {
		Debug.Log ("DECREMENT");
		soundFloat = soundFloat - decrementValue;
		Debug.Log (soundFloat);
	}

	public int getSoundValue() {
		return soundFloat;
	}

	//WHEN TO START THE METER
	public bool soundSwitch() {
		return soundTrigger;
	}
}
