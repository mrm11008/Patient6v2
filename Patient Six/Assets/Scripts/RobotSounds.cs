using UnityEngine;
using System.Collections;

public class RobotSounds : MonoBehaviour {
	
	private AudioSource audiosource;
	private AudioSource audiosource2;

	public AudioClip foundPlayer;
	public AudioClip scanning;
	public AudioClip dartSound;
	public AudioClip robotLost;

	// Use this for initialization
	void Start () {
//		audiosource = gameObject.GetComponent<AudioSource> ();

		var audiosources = this.gameObject.GetComponents<AudioSource> ();
		audiosource = audiosources [0];
		audiosource2 = audiosources [1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playDartSound() {
		audiosource.PlayOneShot (dartSound);
	}
	public void playFoundSound() {
		audiosource2.PlayOneShot (foundPlayer);
	}

	public void playLostSound() {
		audiosource.PlayOneShot (robotLost);
	}
}
