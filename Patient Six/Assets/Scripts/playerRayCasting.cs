using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class playerRayCasting : MonoBehaviour {

    [System.Serializable]
    public class clipData
    {
        public Clipboard clip;
        public GameObject boardIMG;
    }

    Clipboard clip;
    public List<clipData> clips;

    public GameObject clipboardUI;
    private GameObject activeImage;
    //player voice
    public AudioSource puzzleinvest;
    public AudioSource medinvest;
    public AudioSource otherdoorinvest;
    //clipboard showing
    bool clipShowing = false;
    //puzzle showing
    bool puzShowing = false;
    public GameObject puzzleUI;
    public GameObject mechUI;
    public GameObject singlemech;
    public PausedState paused;

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

            // check if clipboard
            clip = whatIHit.collider.GetComponent<Clipboard>();

            // check if puzzledoor
            Puzzle p = whatIHit.collider.GetComponent<Puzzle>();
            //check if otherdoor
            OtherDoor od = whatIHit.collider.GetComponent<OtherDoor>();

            //check if cassette
            CassettePlayer cp = whatIHit.collider.GetComponent<CassettePlayer>();

            //check if medicine
            Medicine m = whatIHit.collider.GetComponent<Medicine>();

            if (paused.GetPausedState())
            {
                mechUI.SetActive(false);
                singlemech.SetActive(false);
            }
            else
            {
                if (p != null || m != null || od != null)
                {
                    mechUI.SetActive(true);
                }
                else if(clip != null || cp != null)
                {
                    singlemech.SetActive(true);
                }
                else
                {
                    mechUI.SetActive(false);
                    singlemech.SetActive(false);
                }
                       
            }

            //if is clipboard
            clipData clipMatch = null;
            for (int i = 0; i < clips.Count; i++)
            {
                if (clips[i].clip == clip)
                {
                    clipMatch = clips[i];
                    break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (m != null)
                {
                    medinvest.Play();
                }
                if (p != null)
                {
                    puzzleinvest.Play();
                }
                if (od != null)
                {
                    otherdoorinvest.Play();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {

                if (clipShowing == false)
                {
                    if (clipMatch != null)
                    {
                        clipboardUI.SetActive(true);
                        clipMatch.boardIMG.SetActive(true);
                        activeImage = clipMatch.boardIMG;
                        clipShowing = true;
                    }
                }
                else
                {
                    clipboardUI.SetActive(false);
                    activeImage.SetActive(false);
                    clipShowing = false;
                }
                //

                //if is puzzle
                if (puzShowing == false)
                {
                    if (p != null)
                    {
                        puzzleUI.SetActive(true);
                        puzShowing = true;
                    }
                }
                else
                {
                    puzzleUI.SetActive(false);
                    puzShowing = false;
                }
                //
            }

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
				if (whatIHit.collider.tag == "SoundMedicine") {


					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.sound) {
						audso.playTakeItem ();
						GM.instance.onTakeSoundMed ();
						Destroy (whatIHit.collider.gameObject);
					}



				}
				if (whatIHit.collider.tag == "LightMedicine") {


					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.light) {
						audso.playTakeItem ();
						GM.instance.onTakeLightMed ();
						Destroy (whatIHit.collider.gameObject);
					}



				}

				if (whatIHit.collider.tag == "Clipboard") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.clipboard) {
						GM.instance.displayClipBoard ();
						audso.playPickUpClip();
					}
				}

				if (whatIHit.collider.name == "Casette Player 6") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.cassette) {
						GM.instance.accessCassetteSix ();
//						GM.instance.startSoundMeter ();
					}
				}
				if (whatIHit.collider.name == "Casette Player 5") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.cassette) {
						GM.instance.accessCassetteFive ();
					}
				}
				if (whatIHit.collider.name == "Casette Player 4") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.cassette) {
						GM.instance.accessCassetteFour ();
					}
				}
				if (whatIHit.collider.name == "Casette Player 3") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.cassette) {
						GM.instance.accessCassetteThree ();
					}
				}
				if (whatIHit.collider.name == "Casette Player 2") { 

					if (whatIHit.collider.gameObject.GetComponent<InteractionObjects> ().whatObjAmI == InteractionObjects.InteractType.cassette) {
						GM.instance.accessCassetteTwo ();
					}
				}
				if (whatIHit.collider.name == "Casette Player 1") { 

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
