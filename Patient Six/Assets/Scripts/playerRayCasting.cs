using UnityEngine;
using System.Collections;

public class playerRayCasting : MonoBehaviour {


	public float distanceToSee;
	RaycastHit whatIHit;
	private bool touching = false;

	private bool isShowing = false;

	public CharacterSounds audso;

	// Use this for initialization
	void Start () {
		audso = GetComponentInParent<CharacterSounds> ();
        //		audso = this.gameObject.GetComponent<CharacterSounds> ();
    }
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (this.transform.position, this.transform.forward * distanceToSee, Color.blue);
		touching = false;
		if (Physics.Raycast (this.transform.position, this.transform.forward, out whatIHit, distanceToSee)) {
			touching = true;
			Debug.Log (whatIHit.collider.tag);
//			if (whatIHit.collider.tag == null) {
//				GM.instance.hideMoveMed ();
//			}
			if (whatIHit.collider.tag == "MoveMed") {
				GM.instance.displayMoveMed ();

			} else {
				GM.instance.hideMoveMed ();


			}


            if (Input.GetKeyDown (KeyCode.E)) {
				Debug.Log ("I picked up a " + whatIHit.collider.gameObject.name);
				if (whatIHit.collider.tag == "MoveMed") {
					

					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.movement) {
						audso.playTakeItem ();
						GM.instance.hideMoveMed ();
						GM.instance.playerHidden ();
//						GM.instance.displayClipBoard ();
						Destroy (whatIHit.collider.gameObject);
					}
					//add code to hide clipboard after viewing


				}

				if (whatIHit.collider.tag == "Clipboard") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.clipboard) {
						GM.instance.displayClipBoard ();
						audso.playPickUpClip();
					}
				}

				if (whatIHit.collider.tag == "Cassette") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.cassette) {
						GM.instance.accessCassetteOne ();
					}
				}

			}

		} else {
			touching = false;
		}
		if (touching = false) {
			GM.instance.hideMoveMed ();
		}


	}
}
