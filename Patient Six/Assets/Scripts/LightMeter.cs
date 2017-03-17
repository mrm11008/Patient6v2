using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LightMeter : MonoBehaviour {

	public Text lightMeterValues;
	public int lightFloat = 100;
	public float alphaFloat = 0.0f;
	public int decrementValue = 1;
	public bool lightTrigger = false;

//
//	void onEnable() {
//		Debug.Log ("METER HAS AWOKEN");
//		InvokeRepeating ("decrementLight", 1.0f, 2.0f);
//	}

	// Use this for initialization
	void Start () {
//		Debug.Log ("METER HAS AWOKEN");
		InvokeRepeating ("decrementLight", 1.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		lightMeterValues.text = "Light: " + lightFloat;
	}

	void decrementLight() {
//		Debug.Log ("DECREMENT");
		lightFloat = lightFloat - decrementValue;
//		alphaFloat += 0.008f;
		if (decrementValue == 0) {

		} else {
			alphaFloat += 0.01f;
		}

//		Debug.Log (li);
	}
	public void incrementLight() {
		if (lightFloat <= 100) {
			if (lightFloat + 25 >= 100) {
				lightFloat = 100;
			} else {
				lightFloat = lightFloat + 25;
				//HAVE TO CHECK TO MAKE SURE IT ISNT BELOW 0
//				if ((alphaFloat - 0.2f) <= 0f) {
//					alphaFloat = 0f;
//				} else {
//					alphaFloat -= 0.2f;
//				}

				if ((alphaFloat - 0.25f) <= 0f) {
					alphaFloat = 0f;
				} else {
					alphaFloat -= 0.25f;
				}

			}
		}

	}

	public void TutorialSet() {
		lightFloat = 75;
	}

	public void dartDecrementLight() {
		if (lightFloat - 50 <= 0) {
			lightFloat = 0;
			alphaFloat -= 0.5f;
		} else {
			lightFloat = lightFloat - 50;
			alphaFloat -= 0.5f;
		}
	}

	public int getLightValue() {
		return lightFloat;
	}

	public float getAlphaValue() {
		return alphaFloat;
	}

	//WHEN TO START THE METER
	public bool lightSwitch() {
		return lightTrigger;
	}
}
