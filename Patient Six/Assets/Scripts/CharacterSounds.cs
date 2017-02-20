using UnityEngine;
using System.Collections;

public class CharacterSounds : MonoBehaviour {

	private AudioSource audiosource;
	private AudioSource audiosource2;
	public AudioClip pickUp;
	public AudioClip takeItem;
	public AudioClip footsteps;

	// Use this for initialization
	void Start () {
		var audiosources = this.gameObject.GetComponents<AudioSource> ();
		audiosource = audiosources [0];
		audiosource2 = audiosources [1];

	}
	public void playPickUpClip() {
		audiosource.PlayOneShot (pickUp);
	}

	public void playTakeItem() {
		audiosource.PlayOneShot (takeItem);
	}
	public void playFootsteps() {
		audiosource2.Play ();
	}
	public void stopFootsteps() {
		audiosource2.Stop ();
	}

	public bool sourceTwoIsPlaying() {
		return audiosource2.isPlaying;
	}



}
