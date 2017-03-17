using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class playerRayCasting : MonoBehaviour {

    [System.Serializable]
    public class clipData
    {
        public Clipboard clip;
        public GameObject boardIMG;
        public bool wifeBoard = false;
    }

    Clipboard clip;
    public List<clipData> clips;
    public bool isWifeBoard;
    public bool wifeBoardSeen = false;
    public bool foundWifeClip = false;

    public GameObject clipboardUI;
    private GameObject activeImage;
    //player voice
    public AudioSource puzzleinvest;
    public AudioSource medinvest;
    public AudioSource otherdoorinvest;
    public AudioSource clipSound;
    //clipboard showing
    bool clipShowing = false;
    //puzzle showing
    bool puzShowing = false;
    public GameObject puzzleUI;
    public GameObject mechUI;
    public GameObject singlemech;
	public GameObject investigate;
    //public GameObject singlemech2;
    public PausedState paused;
    public GameObject hoverCrosshair;

    //cabinet animation
    public Animator cabinet;
    public Collider cdoorCol;
    public bool cabinetOpen = false;

    //end variables
    public Animator buttoncase;
    public Animator buttonAnim;
    public Animator cameraShake;
    public Collider buttonCol;
    public Collider buttoncaseCol;
    public GameObject endLight1;
    public GameObject endLight2;
    public GameObject endLight3;
    public GameObject endLight4;
    public GameObject endLight1model;
    public GameObject endLight2model;
    public GameObject endLight3model;
    public GameObject endLight4model;
    public AudioSource explosion;
    public AudioSource seeYou;
    public bool endPlaying = false;
    public GameObject whiteScreen;
    public GameObject endCredits;
    public GameObject restart;
    private float endtimer = 0;

    public float distanceToSee;
	RaycastHit whatIHit;
	private bool touching = false;

	private bool isShowing = false;

	public CharacterSounds audso;

	public bool isTutorial = false;
	public bool checkCassette = false;
	public bool checkMed = false;
	public bool checkMedLight = false;
	public bool checkMedSound = false;
	public bool checkMedMove = false;

	public bool checkHide = false;
	public bool checkClip = false;
	public bool openDoor = false;

	// Use this for initialization
	void Start () {
		audso = GetComponentInParent<CharacterSounds> ();
        //		audso = this.gameObject.GetComponent<CharacterSounds> ();
        buttoncase.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
		//check to see if this is the tutorial
		isTutorial = GM.instance.GetTutorialCheck();
		if (isTutorial == true) {
			//check list
		}
		if (checkCassette == true && checkMedMove == true && checkMedLight == true && checkMedSound == true && checkClip == true) {
			Debug.Log ("OPEN ZEE DOOR!!!");
			GM.instance.TutorialCheck ();
			openDoor = true;
		}
        Debug.Log(clipShowing);
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
            //or fake med (tutorial)
            FakeMedicine fm = whatIHit.collider.GetComponent<FakeMedicine>();

            //check if cabinet
            Cabinet c = whatIHit.collider.GetComponent<Cabinet>();

			//check if hidingspot
			HidingSpot hidingSpot = whatIHit.collider.GetComponent<HidingSpot>();

            //check if buttoncase
            ButtonCase bc = whatIHit.collider.GetComponent<ButtonCase>();

            //check if button
            ButtonEnd button = whatIHit.collider.GetComponent<ButtonEnd>();
            
            //check if brain
            Brain brain = whatIHit.collider.GetComponent<Brain>();

            //check if parasite
            Parasite parasite = whatIHit.collider.GetComponent<Parasite>();

            if (paused.GetPausedState())
            {
                mechUI.SetActive(false);
                singlemech.SetActive(false);
				investigate.SetActive (false);
                //singlemech2.SetActive(false);
            }
            else
            {
				if (p != null || m != null || od != null) {
					mechUI.SetActive (true);
                    hoverCrosshair.SetActive(true);
				}
                //interact only
                else if (clip != null || cp != null || c != null || bc != null || button != null) {
					singlemech.SetActive (true);
                    hoverCrosshair.SetActive(true);
                } else if (hidingSpot != null || fm != null || brain != null || parasite != null) {
					investigate.SetActive (true);
                    hoverCrosshair.SetActive(true);
                }
                //else if (c != null)
                //{
                //    singlemech2.SetActive(true);
                //}
                else
                {
                    mechUI.SetActive(false);
                    singlemech.SetActive(false);
					investigate.SetActive (false);
                    hoverCrosshair.SetActive(false);
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
                if (m != null || fm != null)
                {
                    medinvest.Play();
                }
                if (p != null)
                {
                    puzzleinvest.Play();
                }
                if(brain != null)
                {
                    audso.Ugh();
                }
                if (parasite != null)
                {
                    audso.whatisthis();
                }
                if (od != null)
                {
                    otherdoorinvest.Play();
                }
				if (hidingSpot != null) {
					print ("HIDING SPOT CHECK");
					audso.PlayInvestigateHiding ();
					checkHide = true;

				}

            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                if (bc != null)
                {
                    buttoncase.SetTrigger("openCase");
                    buttoncaseCol.GetComponent<Collider>().enabled = false;
                }
                if (button != null)
                {
                    buttonAnim.SetTrigger("pushButton");
                    cameraShake.SetTrigger("shake");
                    buttonCol.GetComponent<Collider>().enabled = false;
                    endScene();
                }
                if (c != null)
                {
                    checkMed = true;
                    Debug.Log("MED CHECK");
                    whatIHit.collider.gameObject.GetComponent<Cabinet>().OpenCabinet();
                }

                if (clipShowing == false)
                {
                    if (clipMatch != null) { 

                        Debug.Log("CLIP CHECK");
                        checkClip = true;
                        clipSound.Play();
                        clipboardUI.SetActive(true);
                        clipMatch.boardIMG.SetActive(true);
                        isWifeBoard = clipMatch.wifeBoard;
                        activeImage = clipMatch.boardIMG;
                        clipShowing = true;

                        if (wifeBoardSeen == false)
                        {
                            if (isWifeBoard == true)
                            {
                                audso.noClaire();
                            }
                        }
                    }
                }
                else
                {
                    clipboardUI.SetActive(false);
                    activeImage.SetActive(false);
                    clipShowing = false;

                    if (wifeBoardSeen == false)
                    {
                        if (isWifeBoard == true)
                        {
                            audso.claireInvolved();
                            wifeBoardSeen = true;
                        }
                    }
                }
                //

                //if is puzzle
                if (puzShowing == false)
                {
                    if (p != null)
                    {
                        puzzleUI.SetActive(true);
                        puzShowing = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
                else
                {
                    puzzleUI.SetActive(false);
                    puzShowing = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                //
            }

//            Debug.Log (whatIHit.collider.tag);
//			if (whatIHit.collider.tag == null) {
//				GM.instance.hideMoveMed ();
//			}


            if (Input.GetKeyDown (KeyCode.E)) {
				Debug.Log ("I picked up a " + whatIHit.collider.gameObject.name);
				if (whatIHit.collider.tag == "MoveMed") {
					

//					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.movement) {
						checkMedMove = true;
						audso.playTakeItem ();
						GM.instance.onTakeMoveMed ();

//						GM.instance.hideMoveMed ();
//						GM.instance.playerHidden ();

//						GM.instance.displayClipBoard ();
						Destroy (whatIHit.collider.gameObject);
                        mechUI.SetActive(false);
                    }
					//add code to hide clipboard after viewing


				}
				if (whatIHit.collider.tag == "SoundMedicine") {


//					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.sound) {
						checkMedSound = true;
						audso.playTakeItem ();
						GM.instance.onTakeSoundMed ();
						Destroy (whatIHit.collider.gameObject);
                        mechUI.SetActive(false);
                    }



				}
				if (whatIHit.collider.tag == "LightMedicine") {

					checkMedLight = true;
//					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.light) {
						audso.playTakeItem ();
						GM.instance.onTakeLightMed ();
						Destroy (whatIHit.collider.gameObject);
                        mechUI.SetActive(false);
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
						Debug.Log ("CASSETTE CHECK");

						checkCassette = true;
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
            mechUI.SetActive(false);
            singlemech.SetActive(false);
            investigate.SetActive(false);
            hoverCrosshair.SetActive(false);
        }
        
        if (endPlaying == true)
        {
            endtimer += Time.deltaTime;
        }

        if (endtimer >= 18)
        {
            whiteScreen.SetActive(true);
        }

        if (endtimer >= 28)
        {
            endCredits.SetActive(true);
        }

        if (endtimer >= 40)
        {
            restart.gameObject.GetComponent<GM>().BacktoStart();
        }

    }

    void endScene()
    {
        endLight1.SetActive(false);
        endLight1model.SetActive(false);
        endLight2.SetActive(false);
        endLight2model.SetActive(false);
        endLight3.SetActive(true);
        endLight3model.SetActive(true);
        endLight4.SetActive(true);
        endLight4model.SetActive(true);
        explosion.Play();
        seeYou.Play();
        endPlaying = true;
        restart.gameObject.GetComponent<GM>().stopLevelOneSound();
        restart.gameObject.GetComponent<GM>().stopLevelTwoSound();
        restart.gameObject.GetComponent<GM>().stopLevelThreeSound();
    }

    void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Hide") {

			checkHide = true;
			Debug.Log ("HIDE CHECK");
		}
	}



}
