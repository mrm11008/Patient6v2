using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySpeaker : MonoBehaviour {

	public AudioSource playMySpeaker;
	public GameObject[] speakers;

	//SPEAKER NAMES
	private string myName;

	public AudioClip clipOne;
	public AudioClip clipTwo;
	public AudioClip clipThree;
	// Use this for initialization
	void Start () {
		myName = this.gameObject.name;
		speakers = GameObject.FindGameObjectsWithTag ("Speaker");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayCassette() {
		if (myName == "speakerButtonOne") {
			foreach (GameObject speaker in speakers) {
				speaker.GetComponent<AudioSource> ().clip = clipOne;
				speaker.GetComponent<AudioSource> ().Play ();
			}
		}
		if (myName == "speakerButtonTwo") {
			foreach (GameObject speaker in speakers) {
				speaker.GetComponent<AudioSource> ().clip = clipTwo;
				speaker.GetComponent<AudioSource> ().Play ();
			}
		}
		if (myName == "speakerButtonThree") {
			foreach (GameObject speaker in speakers) {
				speaker.GetComponent<AudioSource> ().clip = clipThree;
				speaker.GetComponent<AudioSource> ().Play ();
			}
		}

	}
}
