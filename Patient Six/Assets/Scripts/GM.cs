using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public static GM instance = null;
	public GameObject gameOver;
	public GameObject playerPrefab;
	public GameObject robotPrefab;
    public GameObject fadeinLevel;

	//Player and Robot objects
	private GameObject clonePlayer;
	private GameObject cloneRobot;
//	private Vector3 playerPosition = new Vector3 (27.0f, 1.11f, -20f);
	private Vector3 playerPosition = new Vector3 (19.5f, 1.0f, 68.0f);
	private Vector3 robotPosition = new Vector3 (14f, 1f, 86f);
	public bool playerDarted;
    public int dartCount = 0;
    public GameObject hideHint;
    public Animation hideHintAnim;

    //Meter Values
    public Sprite[] soundStates;
    public Sprite[] moveStates;
    public Sprite[] lightStates;
    public Image activeSound;
    public Image activeMovement;
    public Image activeLight;

    //UI Screens
    public GameObject startUI;
    public GameObject controlsUI;
    public GameObject crosshair;
	public GameObject tutorialUI;
    public GameObject pausedUI;
    private bool pauseShowing;

    //Cameras
    public Camera PlayerCam;
    public Camera StartScreen;

    //public GameObject MovementMeter;
	public MovementMeter mMeter;
	public SoundMeter sMeter;
	public LightMeter lMeter;
	public float mValue;
	public bool mSwitch = false;
	public bool sSwitch = false;
	public bool lSwitch = false;

	//Movement Variables
	public float movementMeterValue = 100;
	public float decrementMovement = 1;
	public float incrementMovement = 25;
	public bool invertMovement = false;
	public float movementTimerOne = 30.0f;
	public float movementTimerTwo = 20.0f;
	public float movementTimerThree = 10.0f;
	public float movementTimerOneInvert = 3.0f;
	public float movementTimerTwoInvert = 5.0f;
	public float movementTimerThreeInvert = 10.0f;
	public int movLevelCount = 1;
	public int moveLevelCountNew = 1;
	//develop a timer that will tic every x seconds, decrease the movement meter

	//Sound Variables
	public AudioSource soundLevelOne;
	public AudioSource soundLevelTwo;
	public AudioSource soundLevelThree;


	// CASSATTE PLAYERS
	public CassettePlayer cp6;
    public CassettePlayer cp5;
    public CassettePlayer cp4;
    public CassettePlayer cp3;
    public CassettePlayer cp2;
    public CassettePlayer cp1;

    //UI ELEMENTS: CLIPBOARD
    public Image clipBoard;
	public Image clipBoardTwo;
	private bool isShowing;

	public bool hidden = false;

	public bool playSound = true;
	public int playSoundCount = 0;
	public int playSoundCountNew = 0;
	public int lightCount = 0;
	public int lightCountNew = 0;
	public float alphaValues = 0.0f;

	public bool isChasing = false;
	public bool levelThreeSound = false;
	public bool reset = false;

	//TUTORIAL CODE
	public bool hasPlayedTut = false;
	public bool checkCassette = false;
	public bool checkMed = false;
	public bool checkHide = false;
	public bool checkClip = false;
	public int tutCount = 0;
	public bool tutorialRun = false;
    public Animator tutDoor;

    //END
    public GameObject endCheck;

	//TIME TRIGGERS
	public float movementTriggerTimer = 30.0f;
	public float lightTriggerTimer = 60.0f;

	public bool tutorialCheckBool = false;

	void Awake() {
//		clonePlayer = Instantiate (playerPrefab, playerPosition, Quaternion.identity) as GameObject;

		if (instance == null) {
			instance = this;

		} else if (instance != this) {
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
        //Check Scene
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "StartTutorial")
        {
            PlayerCam.enabled = false;
            StartScreen.enabled = true;
        }
        else
        {
            Setup();
        }

        reset = false;

        //Active Functionality
        
    }

    public void TutorialScreen() {
		tutorialUI.SetActive (true);

	}

	public void LaunchTutorial() {
		Application.LoadLevel ("StartTutorial");
	}

    public void BacktoStart()
    {
        Application.LoadLevel("StartTutorial");
        Time.timeScale = 1f;
    }

    void Tutorial() {

		if (hasPlayedTut == false) {
			//ask if the player wants to play the tutorial?
			//if yes
			tutorialRun = true;
			Application.LoadLevel ("StartTutorial");
		} 

	}

	public void LoadGame() {
		Application.LoadLevel("StartTutorial");
	}

	public bool GetTutorialCheck () {
		return tutorialRun;
	}

	public void TutorialCheck() {
		//make sure the player checks out all the required items before unlocking the door
		if (checkCassette == true && checkMed == true && checkClip == true && checkHide == true) {
            //trigger that opens door!
            //set Tutorial Door to active and uncheck Door RM 6 [until we integrate into both scenes]
            tutDoor.SetTrigger("tutFinished");

		}
		tutDoor.SetTrigger("tutFinished");
	}

	public void Setup() {
        //float fadeTime = GameObject.Find("GM").GetComponent<Fading>().BeginFade(-1);
		if (tutorialCheckBool == true) {
			mMeter.TutorialSet ();
			lMeter.TutorialSet ();
			sMeter.TutorialSet ();
		}
        fadeinLevel.SetActive(true);
        robotPrefab.SetActive(true);
		Time.timeScale = 1.0f;
        //Instantiate player and Robot
        //cloneRobot = Instantiate (robotPrefab, robotPosition, Quaternion.identity) as GameObject;
        //Switch Camera
        PlayerCam.enabled = true;
        StartScreen.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Set up sound
        var audiosources = this.gameObject.GetComponents<AudioSource> ();
		soundLevelOne = audiosources [0];
		soundLevelTwo = audiosources [1];
		soundLevelThree = audiosources [2];
		playLevelOneSound ();
        
    }

    public void ResetGame()
    {
		reset = true;
        Reset();
		Time.timeScale = 1f;
		Application.LoadLevel ("MainGame");

    }

	void Reset() {
		Time.timeScale = 1f;
//        Application.LoadLevel (Application.loadedLevel);
		Application.LoadLevel ("MainGame");

    }

	public void GameOver() {
		gameOver.SetActive (true);
		Time.timeScale = .1f;
    }

	public void dartReset() {
		//pause the scene, move the player to his bed, deduct 50% meters
		Time.timeScale = 0.25f;
		float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1);
//		GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1, 1);
		Invoke ("replacePlayer", 1.0f);
		Invoke("replaceRobot" , 1.0f);
        showHidingHint();
    }


	void replacePlayer() {
		Time.timeScale = 1f;
		float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (-1);
//		GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (-1, 1);
		playerPrefab.transform.position = playerPosition;

    }

	void replaceRobot() {
		//change inSight to false
		//reposition him 

		robotPrefab.transform.position = robotPosition;
//		//function call to set robots path position
		robotPrefab.GetComponent<RobotController>().placeRobotAtLocation();
//		robotPrefab.gameObject.GetComponent<RobotMovement>().toggleInSight();
//		hidden = true;
		robotPrefab.GetComponent<RobotController>().dartReset();
    }

	// Update is called once per frame
	void Update () {
        
		//METER TIME TRIGGERS
		if (sSwitch == true) {
			movementTriggerTimer -= Time.deltaTime;
			lightTriggerTimer -= Time.deltaTime;
            //activeSound.sprite = soundStates[0];
        }

		if (movementTriggerTimer <= 0) {
			mSwitch = true;
            //activeMovement.sprite = moveStates[0];
        }
		if (lightTriggerTimer <= 0) {
            activeLight.sprite = lightStates[0];
            lSwitch = true;
		}

        if (gameOver.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown("escape"))
        {
            if (endCheck.gameObject.GetComponent<playerRayCasting>().endPlaying == false)
            {
                if (pauseShowing == false)
                {
                    pausedUI.SetActive(true);
                    Time.timeScale = 0f;
                    pauseShowing = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    pausedUI.SetActive(false);
                    Time.timeScale = 1f;
                    pauseShowing = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            else
            {

            }
        }

        //if camera is start screen, show start UI
        if (StartScreen.enabled == true)
        {
            startUI.SetActive(true);
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            startUI.SetActive(false);
			tutorialUI.SetActive (false);
            crosshair.SetActive(true);
        }

        if (playerDarted == true) {
			dartReset ();
            //Invoke ("dartReset", 1.0f);
            playerDarted = false;
            showHidingHint();
		}
		//MOVEMENT CHECKS
		if (mSwitch == true) {
			//MOVEMET TICK WILL ONLY START ONCE ACTIVE
//			Debug.Log("IM TRUE");
			mMeter.gameObject.SetActive (true);
		}
		//SOUND CHECKS
		if (sSwitch == true) {
			sMeter.gameObject.SetActive (true);

		}
		if (lSwitch == true) {
			lMeter.gameObject.SetActive (true);
		}

        //------------------------------------SOUND METER LEVEL CHECK--------------------------
        if (sMeter.getSoundValue() <= 100 && sSwitch == false)
        {
            //Brain
            activeSound.sprite = soundStates[4];
        }

        if (sMeter.getSoundValue () <= 100 && sMeter.getSoundValue () > 75 && sSwitch == true) {
            playSoundCountNew = 0;
            //			playSound = true;

            //Brain
            activeSound.sprite = soundStates[0];
        }
		if (sMeter.getSoundValue () <= 75 && sMeter.getSoundValue () > 50) {
            playSoundCountNew = 1;
            //			playSound = true;

            //Brain
            activeSound.sprite = soundStates[1];
        }
		if (sMeter.getSoundValue () < 50 && sMeter.getSoundValue () > 25) {
			playSoundCountNew = 2;
            //			playSound = true;

            //Brain
            activeSound.sprite = soundStates[2];
        }

        if (sMeter.getSoundValue() < 25 && sMeter.getSoundValue() >= 0)
        {
            activeSound.sprite = soundStates[3];
        }

        if (playSoundCount == playSoundCountNew) {

		} else if (playSoundCountNew == 0) {
			playLevelOneSound ();
			playSoundCount = 0;
		} else if (playSoundCountNew == 1) {
			playLevelTwoSound ();
			playSoundCount = 1;
		} else if (playSoundCountNew == 2) {
			playLevelThreeSound ();
			playSoundCount = 2;
		}

		if (sMeter.getSoundValue () <= 0) {
			Debug.Log ("SOUND METER LOW");
			GameOver ();
//			Reset ();
		}
		if (lMeter.getLightValue () <= 0) {
			Debug.Log ("LIGHT METER LOW");
			GameOver ();
			//			Reset ();
		}
		if (mMeter.getMovementValue () <= 0) {
			Debug.Log ("MOVEMENT METER LOW");
			GameOver ();
			//			Reset ();
		}

		alphaValues = lMeter.getAlphaValue ();
		GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (1, alphaValues);

        //------------------------------------LIGHT METER LEVEL CHECK--------------------------
        if (lMeter.getLightValue() == 100 && lSwitch == false)
        {
            activeLight.sprite = lightStates[4];
        }
        if (lMeter.getLightValue () <= 100 && lMeter.getLightValue () > 75 && lSwitch == true) {
            activeLight.sprite = lightStates[0];
        }
		if (lMeter.getLightValue () <= 75 && lMeter.getLightValue () > 50) {
            activeLight.sprite = lightStates[1];
        }
		if (lMeter.getLightValue () < 50 && lMeter.getLightValue () > 25) {
            activeLight.sprite = lightStates[2];
        }
        if (lMeter.getLightValue() < 25 && lMeter.getLightValue() >= 0)
        {
            activeLight.sprite = lightStates[3];
        }

        //		if (lightCount == lightCountNew) {
        //
        //		} else if (lightCountNew == 0) {
        //			lightLevelOne ();
        //			lightCount = 0;
        //		} else if (lightCountNew == 1) {
        //			lightLevelTwo ();
        //			lightCount = 1;
        //		} else if (lightCountNew == 2) {
        //			lightLevelThree ();
        //			lightCount = 2;
        //		}
        //------------------------------------MOVEMENT METER LEVEL CHECK--------------------------
        if (mMeter.getMovementValue() == 100 && mSwitch == false)
        {
            //Brain
            activeMovement.sprite = moveStates[4];
        }
        if (mMeter.getMovementValue () <= 100 && mMeter.getMovementValue () > 75 && mSwitch == true) {
            //Brain
            activeMovement.sprite = moveStates[0];
        }
		if (mMeter.getMovementValue () <= 75 && mMeter.getMovementValue () > 50) {
			Debug.Log ("MLEVEL ONE");
			movementLevelOne ();
			moveLevelCountNew = 1;

            //Brain
            activeMovement.sprite = moveStates[1];
        }
		if (mMeter.getMovementValue () < 50 && mMeter.getMovementValue () > 25) {
			moveLevelCountNew = 2;
			Debug.Log ("MLEVEL TWO");

			movementLevelTwo ();
            //Brain
            activeMovement.sprite = moveStates[2];
        }

        if (mMeter.getMovementValue() < 25 && mMeter.getMovementValue() >= 0)
        {
			Debug.Log ("MLEVEL THREE");

			moveLevelCountNew = 3;
			movementLevelThree ();
            //Brain
            activeMovement.sprite = moveStates[3];
        }
        if (movLevelCount == moveLevelCountNew) {

		} else if (moveLevelCountNew == 1) {
//			movementLevelOne ();
			movLevelCount = 1;
		} else if (moveLevelCountNew == 2) {
			movementLevelTwo ();
			movLevelCount = 2;
		} else if (moveLevelCountNew == 3) {
			movementLevelThree ();
			movLevelCount = 3;
		}

	}

    //Hide Hint
    public void showHidingHint()
    {
        hideHintAnim.Play();
    }

	//---------------------------------METER CODE---------------------------------------
	public void startSoundMeter() {
		sSwitch = true;
//		lSwitch = true;
//		mSwitch = true;
	}

	//---------------------------------HIDE CODE---------------------------------------
	public void playerHidden() {
		hidden = true;
		mSwitch = true;

	}

	public void playerNotHidden() {
		hidden = false;
	}
	public bool hiddenCheck() {
		return hidden;
	}

	//---------------------------------MEDICINE CODE---------------------------------------
	//MEDICINE UI
	public void displayClipBoard() {
		isShowing = !isShowing;
		clipBoard.gameObject.SetActive (isShowing);
	}
	public void hideClipBoard() {
		clipBoard.gameObject.SetActive (false);
	}
	//MEDICINE FUNCTIONALITY
//	public void onTakeMoveMed() {
//		movementMeterValue += incrementMovement;
//	}
	public void decreaseMovement() {
		movementMeterValue -= decrementMovement;
	}

	public void onTakeSoundMed() {
		sMeter.incrementSound ();
	}
	public void onTakeLightMed() {
		lMeter.incrementLight ();
	}
	public void onTakeMoveMed() {
		mMeter.incrementMovement ();
	}
	//---------------------------------SOUND CODE---------------------------------------

	public void playLevelOneSound() {
		Debug.Log ("PLAY LEVEL 1 SOUND");
//		if (playSound == true && playSoundCount == 0) {
//			soundLevelOne.Play ();
//			playSound = false;
//		}
		levelThreeSound = false;
		soundLevelOne.Play ();
		stopLevelTwoSound ();
		stopLevelThreeSound ();
    }

	public void playLevelTwoSound() {
		Debug.Log ("PLAY LEVEL 2 SOUND");
		levelThreeSound = false;
		soundLevelTwo.Play ();
		stopLevelOneSound ();
		stopLevelThreeSound ();
    }

	public void playLevelThreeSound() {
		Debug.Log ("PLAY LEVEL 3 SOUND");
		levelThreeSound = true;
		soundLevelThree.Play ();
		stopLevelOneSound ();
		stopLevelTwoSound ();
    }
	public bool LevelThreeSoundCheck() {
		return levelThreeSound;
	}

	public void stopLevelOneSound() {
		soundLevelOne.Stop ();
	}

	public void stopLevelTwoSound() {
		soundLevelTwo.Stop ();
	}

	public void stopLevelThreeSound() {
		soundLevelThree.Stop ();
	}

	//---------------------------------LIGHT CODE---------------------------------------

	public void lightLevelOne() {
//		GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (1, 0.0f);
	}

	public void lightLevelTwo() {
//		Debug.Log ("KILL THE LGIHTS!!!");
//		GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (1, 0.2f);
	}

	public void lightLevelThree() {
//		GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (1, 0.4f);
	}

	//---------------------------------MOVEMENT CODE---------------------------------------

	public void movementLevelOne() {
		movementTimerOne -= Time.deltaTime;
//		Debug.Log (movementTimerOne);
		if (movementTimerOne <= 0) {
			Debug.Log ("INVERT!");
			invertMovement = true;
			movementTimerOneInvert -= Time.deltaTime;
			if (movementTimerOneInvert <= 0) {
				movementTimerOne = 30.0f;
				movementTimerOneInvert = 3.0f;
				invertMovement = false;
			}
		}
	}
	public void movementLevelTwo() {
		movementTimerTwo -= Time.deltaTime;
		//		Debug.Log (movementTimerOne);
		if (movementTimerTwo <= 0) {
			Debug.Log ("INVERT!");
			invertMovement = true;
			movementTimerTwoInvert -= Time.deltaTime;
			if (movementTimerTwoInvert <= 0) {
				movementTimerTwo = 20.0f;
				movementTimerTwoInvert = 5.0f;
				invertMovement = false;
			}
		}
	}
	public void movementLevelThree() {
		movementTimerThree -= Time.deltaTime;
		//		Debug.Log (movementTimerOne);
		if (movementTimerThree <= 0) {
			Debug.Log ("INVERT!");
			invertMovement = true;
			movementTimerThreeInvert -= Time.deltaTime;
			if (movementTimerThreeInvert <= 0) {
				movementTimerThree = 10.0f;
				movementTimerThreeInvert = 10.0f;
				invertMovement = false;
			}
		}
	}
	public bool getMovementTrigger() {
		return invertMovement;
	}

	//---------------------------------DART CODE---------------------------------------
	public void dartPlayer() {
		Time.timeScale = 0.25f;
		//SLOW DOWN MOUSE MOVEMENT ASWELL
		playerDarted = true;

	}

	public void dartPlayerTutorial() {
		Time.timeScale = 0.25f;
		float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1);
		//SLOW DOWN MOUSE MOVEMENT ASWELL
//		playerDarted = true;
//		Invoke("ResetGame", 1.0f);
		Invoke("LoadRealGame", 0.7f);
//		LoadRealGame();
//		ResetGame();
	}



	public void LoadRealGame() {
		Application.LoadLevel ("MainGame");
		float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (-1);
	}

	public void dartSound() {
		if (sMeter.isActiveAndEnabled == true) {
			sMeter.dartDecrementSound ();
		}
		if (lMeter.isActiveAndEnabled == true) {
			lMeter.dartDecrementLight ();
//			print ("I AM READY LIGHT");
		} else {
//			print ("I AM NOT READY light");
		}
		if (mMeter.isActiveAndEnabled == true) {
			mMeter.dartDecrementMovement ();
//			print ("I AM READY MOVEMENT");
		} else {
//			print ("I AM NOT READY MOVEMENT");
		}

	}

	//---------------------------------CASSETTE CODE---------------------------------------
	public void accessCassetteSix() {

		cp6.playCassette ();
	}
	public void accessCassetteFive() {

		cp5.playCassette ();
	}
	public void accessCassetteFour() {

		cp4.playCassette ();
	}
	public void accessCassetteThree() {

		cp3.playCassette ();
	}
	public void accessCassetteTwo() {

		cp2.playCassette ();
	}
	public void accessCassetteOne() {

		cp1.playCassette ();
	}


	public void Chasing() {
		isChasing = true;
	}

	public void NotChasing() {
		isChasing = false;
	}

	public bool ChaseCheck() {
		return isChasing;
	}



}
