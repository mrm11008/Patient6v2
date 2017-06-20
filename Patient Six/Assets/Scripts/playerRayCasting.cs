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
        public GameObject hPiece1;
        public GameObject hPiece2;
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
    public GameObject hideMech;
    public GameObject takeMech;
    public GameObject playMech;
    public GameObject openMech;
    public GameObject viewMech;
    public GameObject useMech;
    public GameObject inspectMech;
    //public GameObject singlemech2;
    public PausedState paused;

    //cabinet animation
    public Animator cabinet;
    public Collider cdoorCol;
    public bool cabinetOpen = false;

    //hover shader
    private Shader outline;
    private Shader standard;
    private Renderer activekeyHover;
    private Renderer activecassetteHover;
    private Renderer activeClipHover1;
    private Renderer activeClipHover2;
	private Renderer activeLockerHover;
	private Renderer activeMedHover;
	private HoverThis[] medicineHovers;
	private HoverThis medicineHover;
	private HoverThis medicineLastHover;
	private Transform medTransformCheck;
	private GameObject medLastHit;
	private GameObject medNowHit;
	private GameObject lockerLastHit;
	private GameObject lockerNowHit;
//	private Renderer[] medicineHovers;

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

	public bool SoundFull = false;
	public bool MovementFull = false;
	public bool LightFull = false;

	public characterController player;

	// Use this for initialization
	void Start () {
		audso = GetComponentInParent<CharacterSounds> ();
        //		audso = this.gameObject.GetComponent<CharacterSounds> ();
        buttoncase.GetComponent<Animator>();

		player = GetComponentInParent<characterController> ();

        //hover
        standard = Shader.Find("Standard");
        outline = Shader.Find("Outlined/Silhouetted Bumped");
    }
	
	// Update is called once per frame
	void Update () {
		//check to see if this is the tutorial
		isTutorial = GM.instance.GetTutorialCheck();
		if (isTutorial == true) {
			//check list
		}
		if (checkCassette == true && checkMedMove == true && checkMedLight == true && checkMedSound == true && checkClip == true && checkHide == true) {
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
			LockedLocker locker = whatIHit.collider.GetComponent<LockedLocker> ();
            DoorKey key = whatIHit.collider.GetComponent<DoorKey> ();
			KeyDoor door = whatIHit.collider.GetComponent<KeyDoor> ();

            //check if buttoncase
            ButtonCase bc = whatIHit.collider.GetComponent<ButtonCase>();

            //check if button
            ButtonEnd button = whatIHit.collider.GetComponent<ButtonEnd>();
            
            //check if brain
            Brain brain = whatIHit.collider.GetComponent<Brain>();

            //check if parasite
            Parasite parasite = whatIHit.collider.GetComponent<Parasite>();

			//check speaker
			PlaySpeaker speaker = whatIHit.collider.GetComponent<PlaySpeaker>();

			//medicine hover?

			if (m != null) {
				medNowHit = m.gameObject;
//				medicineHovers = whatIHit.collider.GetComponentsInChildren<HoverThis> ();
//				foreach (HoverThis hover in medicineHovers) {
//					hover.gameObject.GetComponent<Renderer>().material.shader = outline;
//					activeMedHover = hover.gameObject.GetComponent<Renderer>();
//				}
//				clipMatch.hPiece2.GetComponent<Renderer>().material.shader = outline;
//				activeClipHover1 = clipMatch.hPiece1.GetComponent<Renderer>();
				medicineHover = whatIHit.collider.GetComponentInChildren<HoverThis>();
				medicineHover.GetComponentInChildren<Renderer> ().material.shader = outline;
				activeMedHover = medicineHover.GetComponentInChildren<Renderer> ();
//				whatIHit.collider.GetComponentInChildren<Renderer> ().material.shader = outline;
//				activeMedHover = whatIHit.collider.GetComponentInChildren<Renderer> ();
				if (medLastHit && medLastHit != medNowHit) {
					medicineLastHover = medLastHit.GetComponentInChildren<HoverThis> ();
					medicineLastHover.GetComponentInChildren<Renderer> ().material.shader = standard;
				}
				medLastHit = medNowHit;
			} else {
//				medLastHit = medNowHit;
				if (activeMedHover != null) {
					activeMedHover.material.shader = standard;
				}

//				if (medLastHit && medLastHit != medNowHit) {
//					medicineLastHover = medLastHit.GetComponentInChildren<HoverThis> ();
//					medicineLastHover.GetComponentInChildren<Renderer> ().material.shader = standard;
//				}
//				activeMedHover.material.shader = standard;
			}

			//locker hover
			if (locker != null) {
				lockerNowHit = locker.gameObject;
				whatIHit.collider.GetComponent<Renderer> ().material.shader = outline;
				activeLockerHover = whatIHit.collider.GetComponent<Renderer> ();

				if (lockerLastHit && lockerLastHit != lockerNowHit) {
//					medicineLastHover = medLastHit.GetComponentInChildren<HoverThis> ();
//					medicineLastHover.GetComponentInChildren<Renderer> ().material.shader = standard;

					lockerLastHit.GetComponent<Renderer> ().material.shader = standard;
				}
				lockerLastHit = lockerNowHit;
			} else {
				if (activeLockerHover != null)
				{
					activeLockerHover.material.shader = standard;
				}
			}

            //speaker hover
            if (speaker != null)
            {
                whatIHit.collider.GetComponent<Renderer>().material.shader = outline;
                activecassetteHover = whatIHit.collider.GetComponent<Renderer>();
            }
            else
            {
                if (activecassetteHover != null)
                {
                    activecassetteHover.material.shader = standard;
                }
            }

            //key hover
            if (key != null)
            {
                whatIHit.collider.GetComponent<Renderer>().material.shader = outline;
                activekeyHover = whatIHit.collider.GetComponent<Renderer>();
            }
            else
            {
                if (activekeyHover != null)
                {
                    activekeyHover.material.shader = standard;
                }
            }

            //Sticky UI Elements Debug, Includes all
            if (p == null && cp == null && m == null && c == null && hidingSpot == null && locker == null 
                && key == null && door == null && bc == null && button == null && brain == null 
                && parasite == null)
            {
                viewMech.SetActive(false);
                playMech.SetActive(false);
                useMech.SetActive(false);
                openMech.SetActive(false);
                hideMech.SetActive(false);
                takeMech.SetActive(false);
                inspectMech.SetActive(false);
                
            }

            if (paused.GetPausedState())
            {
                hideMech.SetActive(false);
                takeMech.SetActive(false);
				inspectMech.SetActive (false);
            }
            else
            {
                //hide,inspect
                if (locker != null) {
                    hideMech.SetActive(true);
                    inspectMech.SetActive(true);
                //view,inspect
                }else if (p != null)
                {
                    viewMech.SetActive(true);
                    inspectMech.SetActive(true);
                //view
                } else if (clip != null){
                    viewMech.SetActive(true);
                }
                //play
                else if(cp != null){
                    playMech.SetActive(true);
                }
                //take,inspect
                else if (m != null)
                {
					if (m.whatMedAmI == Medicine.MedicineType.sound) {
						if (SoundFull == true) {
							useMech.SetActive (false);
						} else {
							useMech.SetActive(true);
						}
					} else if (m.whatMedAmI == Medicine.MedicineType.light) {
						if (LightFull == true) {
							useMech.SetActive (false);
						} else {
							useMech.SetActive(true);
						}
					} else if (m.whatMedAmI == Medicine.MedicineType.movement) {
						if (MovementFull == true) {
							useMech.SetActive (false);
						} else {
							useMech.SetActive(true);
						}
					}
                    
                    inspectMech.SetActive(true);
                }
                //open
                else if(c != null || door != null)
                {
                    openMech.SetActive(true);
                }
                //take
                else if (button != null || key != null) {
                    takeMech.SetActive(true);
                //inspect
                } else if (hidingSpot != null || brain != null || parasite != null) {
                    inspectMech.SetActive(true);
				} else if (speaker != null) {
					playMech.SetActive (true);
				}
                else
                {
                    viewMech.SetActive(false);
                    playMech.SetActive(false);
                    useMech.SetActive(false);
                    openMech.SetActive(false);
                    hideMech.SetActive(false);
                    takeMech.SetActive(false);
                    inspectMech.SetActive(false);
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

            //clipboard hover
            if (clipMatch != null)
            {
                clipMatch.hPiece1.GetComponent<Renderer>().material.shader = outline;
                clipMatch.hPiece2.GetComponent<Renderer>().material.shader = outline;
                activeClipHover1 = clipMatch.hPiece1.GetComponent<Renderer>();
                activeClipHover2 = clipMatch.hPiece2.GetComponent<Renderer>();
            }
            else
            {
                if (activeClipHover1 != null || activeClipHover2 != null)
                {
                    activeClipHover1.material.shader = standard;
                    activeClipHover2.material.shader = standard;
                }
            }

            //            if (Input.GetKeyDown(KeyCode.Q))
            if (Input.GetMouseButtonDown(1))
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



//            if (Input.GetKeyDown(KeyCode.E))
			if (Input.GetMouseButtonDown(0))
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
                    return;
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
                        return;
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
                    return;
                }
                //

                //if is puzzle
                if (puzShowing == false)
                {
                    if (p != null)
                    {
						viewMech.SetActive(false);
						inspectMech.SetActive(false);
                        puzzleUI.SetActive(true);
						Cursor.visible = true;
                        puzShowing = true;
                        Cursor.lockState = CursorLockMode.None;
                        return;
                    }
                }
                else
                {
					print ("THIS SHOULD");
//						if (Input.GetKey.

					

                }
				if (puzShowing) {
					print ("PUZZLE IS SHOWING!!!!!");
					if (Input.GetKeyDown(KeyCode.Z)) {

						puzzleUI.SetActive(false);
						puzShowing = false;
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
						return;
					}
				}
                //
            }

			if (puzShowing) {
				print ("PUZZLE IS SHOWING!!!!!");
				if (Input.GetKeyDown(KeyCode.Z)) {

					puzzleUI.SetActive(false);
					puzShowing = false;
					Cursor.lockState = CursorLockMode.Locked;
					return;
				}
			}

//            Debug.Log (whatIHit.collider.tag);
//			if (whatIHit.collider.tag == null) {
//				GM.instance.hideMoveMed ();
//			}

			//CHECK TO SEE IF YOU NEED TO HIDE THE UI
			if (player.CheckHidden ()) {
				//ONLY WORKS FOR CROUCHING SPOTS
				print ("HIDE THE UI");
				hideMech.SetActive(false);
				inspectMech.SetActive(false);
			}

			
//            if (Input.GetKeyDown (KeyCode.E)) {
			if (Input.GetMouseButtonDown(0)) {
				Debug.Log ("I picked up a " + whatIHit.collider.gameObject.name);
				if (whatIHit.collider.tag == "MoveMed") {
					

//					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.movement) {
						if (!GM.instance.IsMoveFull ()) {
							checkMedMove = true;
							audso.playTakeItem ();
							GM.instance.onTakeMoveMed ();

//						GM.instance.hideMoveMed ();
//						GM.instance.playerHidden ();

//						GM.instance.displayClipBoard ();
							Destroy (whatIHit.collider.gameObject);
							takeMech.SetActive (false);
							inspectMech.SetActive (false);
						}
                    }
					//add code to hide clipboard after viewing


				}
				if (whatIHit.collider.tag == "SoundMedicine") {


//					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.sound) {
						if (!GM.instance.IsSoundFull ()) {
							checkMedSound = true;
							audso.playTakeItem ();
							GM.instance.onTakeSoundMed ();
							Destroy (whatIHit.collider.gameObject);
							takeMech.SetActive (false);
							inspectMech.SetActive (false);
						}
                    }



				}
				if (whatIHit.collider.tag == "LightMedicine") {

					checkMedLight = true;
//					Debug.Log ("WTF");
					if (whatIHit.collider.gameObject.GetComponent<Medicine> ().whatMedAmI == Medicine.MedicineType.light) {
						if (!GM.instance.IsLightFull ()) {
							audso.playTakeItem ();
							GM.instance.onTakeLightMed ();
							Destroy (whatIHit.collider.gameObject);
							takeMech.SetActive (false);
							inspectMech.SetActive (false);
						}
                    }



				}

				//SPEAKER SYSTEM
				if (whatIHit.collider.tag == "SpeakerButton") {
					checkCassette = true;
					whatIHit.collider.gameObject.GetComponent<PlaySpeaker> ().PlayCassette ();
				}


				if (whatIHit.collider.tag == "LockedDoor") { 
					player.PlayLocked ();
				}

				if (whatIHit.collider.tag == "Key") {
					GM.instance.PickUpKey ();
					audso.playTakeItem ();
					Destroy (whatIHit.collider.gameObject);
				}
				if (whatIHit.collider.tag == "KeyDoor") {
					GM.instance.DoorCheck ();
				}



				if (whatIHit.collider.tag == "HideLockerOne") { 

					whatIHit.collider.gameObject.GetComponent<Renderer> ().material.shader = standard;
					checkHide = true;
					player.lockerHidden ();
					GM.instance.HidingCameraSwitchOne ();
					Debug.Log ("I HAVE CLICKED THE HIDING SPOT");
					hideMech.SetActive(false);
					inspectMech.SetActive(false);
				}
				if (whatIHit.collider.tag == "HideLockerTwo") { 

					whatIHit.collider.gameObject.GetComponent<Renderer> ().material.shader = standard;
					checkHide = true;
					player.lockerHidden ();
					GM.instance.HidingCameraSwitchTwo ();
					Debug.Log ("I HAVE CLICKED THE HIDING SPOT");
					hideMech.SetActive(false);
					inspectMech.SetActive(false);
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
            hideMech.SetActive(false);
            takeMech.SetActive(false);
            inspectMech.SetActive(false);
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

	public void HideSoundUseMech() {
		SoundFull = true;
	}

	public void ShowSoundUseMech() {
		SoundFull = false;
	}

	public void HideMovementUseMech() {
		MovementFull = true;
	}

	public void ShowMovementUseMech() {
		MovementFull = false;
	}
	public void HideLightUseMech() {
		LightFull = true;
	}

	public void ShowLightUseMech() {
		LightFull = false;
	}


}
