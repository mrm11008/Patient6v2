using UnityEngine;
using System.Collections;

public class CassettePlayer : MonoBehaviour {


	private AudioSource audso;

	// Use this for initialization
	void Start () {
		audso = gameObject.GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playCassette() {
		audso.Play ();
	}
}
