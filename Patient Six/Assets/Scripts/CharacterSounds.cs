using UnityEngine;
using System.Collections;

public class CharacterSounds : MonoBehaviour {

	private AudioSource audiosource;
	private AudioSource audiosource2;
	public AudioClip pickUp;
	public AudioClip takeItem;
	public AudioClip footsteps;
	public AudioClip getOut;
    public AudioClip hospitalsound;
    public AudioClip ugh;
    public AudioClip what;
    public AudioClip noexit;
    public AudioClip thisplace;
    public AudioClip noclaire;
    public AudioClip saveyou;
    public AudioClip claireinvolved;

    public AudioClip movementInversion1;
	public AudioClip movementInversion2;
	public AudioClip movementInversion3;
	public AudioClip movementInversion4;
	public AudioClip movementInversion5;

	public AudioClip hiddenSound1;
	public AudioClip hiddenSound2;
	public AudioClip hiddenSound3;

	public AudioClip lowSound;

	public AudioClip hidingSpot;

	// Use this for initialization
	void Start () {
		var audiosources = this.gameObject.GetComponents<AudioSource> ();
		audiosource = audiosources [0];
		audiosource2 = audiosources [1];

	}
	public void playPickUpClip() {
		audiosource.PlayOneShot (pickUp);
	}

    public void playHospital()
    {
        audiosource.PlayOneShot(hospitalsound);
    }

    public void Ugh()
    {
        audiosource.PlayOneShot(ugh);
    }

    public void whatisthis()
    {
        audiosource.PlayOneShot(what);
    }

    public void noExit()
    {
        audiosource.PlayOneShot(noexit);
    }

    public void thisPlace()
    {
        audiosource.PlayOneShot(thisplace);
    }

    public void noClaire()
    {
        audiosource.PlayOneShot(noclaire);
    }
    public void saveYou()
    {
        audiosource.PlayOneShot(saveyou);
    }
    public void claireInvolved()
    {
        audiosource.PlayOneShot(claireinvolved);
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

	public void playGetOut() {
		audiosource.PlayOneShot (getOut);
	}

	public void playRandMovement() {

		int i = Random.Range (1, 5);
		if (i == 1) {
			audiosource.PlayOneShot (movementInversion1);
		}
		if (i == 2) {
			audiosource.PlayOneShot (movementInversion2);
		}
		if (i == 3) {
			audiosource.PlayOneShot (movementInversion3);
		}
		if (i == 4) {
			audiosource.PlayOneShot (movementInversion4);
		}
		if (i == 5) {
			audiosource.PlayOneShot (movementInversion5);
		}
			
	}

	public void playHidden() {
		int i = Random.Range (1, 3);
		if (i == 1) {
			audiosource.PlayOneShot (hiddenSound1);
		}
		if (i == 2) {
			audiosource.PlayOneShot (hiddenSound2);
		}
		if (i == 3) {
			audiosource.PlayOneShot (hiddenSound3);
		}
	}

	public void lowSoundNoise() {

		print ("TOO LOUD");
		audiosource.PlayOneShot (lowSound);
	}

	public void PlayInvestigateHiding() {
		audiosource.PlayOneShot (hidingSpot);
	}

}
