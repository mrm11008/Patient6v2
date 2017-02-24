using UnityEngine;
using System.Collections;

public class RobotSounds : MonoBehaviour {
	
	private AudioSource audiosource;

	public AudioClip foundPlayer;
	public AudioClip scanning;
	public AudioClip dartSound;

	// Use this for initialization
	void Start () {
		audiosource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playDartSound() {
		audiosource.PlayOneShot (dartSound);
	}
	public void playFoundSound() {
		audiosource.PlayOneShot (foundPlayer);
	}
}
