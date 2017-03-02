using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GM : MonoBehaviour {

	public static GM instance = null;
	public GameObject startGame;
	public GameObject gameOver;
	public GameObject playerPrefab;
	public GameObject robotPrefab;

	//Player and Robot objects
	private GameObject clonePlayer;
	private GameObject cloneRobot;
//	private Vector3 playerPosition = new Vector3 (27.0f, 1.11f, -20f);
	private Vector3 playerPosition = new Vector3 (19.5f, 1.0f, 68.0f);
	private Vector3 robotPosition = new Vector3 (14f, 1f, 86f);
	public bool playerDarted;

	//Meter Values
	public Text movementMeter;
	public Text soundMeter;
	public Text visionMeter;
	public Text lightMeter;

    //public GameObject MovementMeter;
	public MovementMeter mMeter;
	public SoundMeter sMeter;
	public float mValue;
	public bool mSwitch = false;
	public bool sSwitch = false;

	//Movement Variables
	public GameObject movementMed;
	public float movementMeterValue = 100;
	public float decrementMovement = 1;
	public float incrementMovement = 25;
	public bool invertMovement = false;
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
		startTheGame ();
	}
	void startTheGame() {
		Time.timeScale = 0.0f;
		startGame.SetActive (true);
	}
	void Setup() {
		Time.timeScale = 1.0f;
		startGame.SetActive (false);
		//Instantiate player and Robot
//		cloneRobot = Instantiate (robotPrefab, robotPosition, Quaternion.identity) as GameObject;

		//Set up sound
		var audiosources = this.gameObject.GetComponents<AudioSource> ();
		soundLevelOne = audiosources [0];
		soundLevelTwo = audiosources [1];
		soundLevelThree = audiosources [2];
		playLevelOneSound ();

	}

	void Reset() {
		Time.timeScale = 1f;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void GameOver() {

		gameOver.SetActive (true);
		Time.timeScale = .1f;
		Invoke ("Reset", 1.0f);
	}

	public void dartReset() {
		//pause the scene, move the player to his bed, deduct 50% meters
		Time.timeScale = 0.25f;
		float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1);
		Invoke ("replacePlayer", 1.0f);
		Invoke("replaceRobot" , 1.0f);
	}

	void replacePlayer() {
		Time.timeScale = 1f;
		float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (-1);
		playerPrefab.transform.position = playerPosition;

	}

	void replaceRobot() {
		//change inSight to false
		//reposition him 

		robotPrefab.transform.position = robotPosition;
//		//function call to set robots path position
		robotPrefab.GetComponent<RobotMovement>().placeRobotAtLocation();
//		robotPrefab.gameObject.GetComponent<RobotMovement>().toggleInSight();
//		hidden = true;
		robotPrefab.GetComponent<RobotMovement>().dartReset();
	}

	// Update is called once per frame
	void Update () {
		//starting the game
		if (startGame == true) {

		}
		if (Input.GetKeyDown ("s")) {
			Setup ();

		}
		if (Input.GetKeyDown ("r")) {
			Reset ();
		}

		if (playerDarted == true) {
			dartReset ();
//			Invoke ("dartReset", 1.0f);
			playerDarted = false;
		}
		//MOVEMENT CHECKS
		if (mSwitch == true) {
			//MOVEMET TICK WILL ONLY START ONCE ACTIVE
			Debug.Log("IM TRUE");
			mMeter.gameObject.SetActive (true);
		}
		//SOUND CHECKS
		if (sSwitch == true) {
			sMeter.gameObject.SetActive (true);
		}

//		if (sMeter.getSoundValue () <= 100 && sMeter.getSoundValue () > 75) {
//			playSoundCount = 0;
//			if (playSound == true && playSoundCount == 0) {
//				playLevelOneSound ();
//				stopLevelTwoSound ();
//				stopLevelThreeSound ();
//				playSoundCount = 5;
//			}
//			playSound = false;
//
//		}
//		if (sMeter.getSoundValue () < 75 && sMeter.getSoundValue () > 50) {
//			playSoundCount = 1;
//			playSound = true;
//			if (playSound == true && playSoundCount == 1) {
//				playLevelTwoSound ();	
//				stopLevelOneSound();
//				stopLevelThreeSound ();
//				playSoundCount = 5;
//			}
//			playSound = false;
//
//		}
//		if (sMeter.getSoundValue () < 50 && sMeter.getSoundValue () > 25) {
//			playSoundCount = 2;
//			playSound = true;
//			if (playSound == true && playSoundCount == 2) {
//				playLevelThreeSound ();
//				stopLevelTwoSound();
//				stopLevelOneSound ();
//				playSoundCount = 5;
//			}
//			playSound = false;
//
//		}
		if (sMeter.getSoundValue () <= 100 && sMeter.getSoundValue () > 75) {
			playSoundCount = 0;
//			playSound = true;

		}
		if (sMeter.getSoundValue () < 75 && sMeter.getSoundValue () > 50) {
			playSoundCount = 1;
//			playSound = true;


		}
		if (sMeter.getSoundValue () < 50 && sMeter.getSoundValue () > 25) {
			playSoundCount = 2;
//			playSound = true;


		}
		if (sMeter.getSoundValue () <= 0) {
			Debug.Log ("SOUND METER LOW");
			GameOver ();
//			Reset ();
		}

	}

	//---------------------------------METER CODE---------------------------------------
	public void startSoundMeter() {
		sSwitch = true;

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
	public void displayMoveMed() {

		movementMed.SetActive (true);
	}
	public void hideMoveMed() {
		movementMed.SetActive (false);
	}
	public void displayClipBoard() {
		isShowing = !isShowing;
		clipBoard.gameObject.SetActive (isShowing);
	}
	public void hideClipBoard() {
		clipBoard.gameObject.SetActive (false);
	}
	//MEDICINE FUNCTIONALITY
	public void onTakeMoveMed() {
		movementMeterValue += incrementMovement;
	}
	public void decreaseMovement() {
		movementMeterValue -= decrementMovement;
	}

	public void onTakeSoundMed() {
		sMeter.incrementSound ();
	}

	//---------------------------------SOUND CODE---------------------------------------

	public void playLevelOneSound() {
		Debug.Log ("PLAY AMBIENT SOUND");
//		if (playSound == true && playSoundCount == 0) {
//			soundLevelOne.Play ();
//			playSound = false;
//		}
		soundLevelOne.Play ();
		stopLevelTwoSound ();
		stopLevelThreeSound ();
	}

	public void playLevelTwoSound() {
		
		soundLevelTwo.Play ();
		stopLevelOneSound ();
		stopLevelThreeSound ();
	}

	public void playLevelThreeSound() {
		soundLevelThree.Play ();
		stopLevelOneSound ();
		stopLevelTwoSound ();
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

	//---------------------------------DART CODE---------------------------------------
	public void dartPlayer() {
		Time.timeScale = 0.25f;
		//SLOW DOWN MOUSE MOVEMENT ASWELL
		playerDarted = true;


	}
	public void dartSound() {
		sMeter.dartDecrementSound ();

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

}
