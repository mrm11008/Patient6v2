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
	public LightMeter lMeter;
	public float mValue;
	public bool mSwitch = false;
	public bool sSwitch = false;
	public bool lSwitch = false;

	//Movement Variables
	public GameObject movementMed;
	public float movementMeterValue = 100;
	public float decrementMovement = 1;
	public float incrementMovement = 25;
	public bool invertMovement = false;
	public float movementTimerOne = 20.0f;
	public float movementTimerTwo = 15.0f;
	public float movementTimerThree = 10.0f;
	public float movementTimerOneInvert = 5.0f;
	public float movementTimerTwoInvert = 10.0f;
	public float movementTimerThreeInvert = 15.0f;
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
//		GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1, 1);
		Invoke ("replacePlayer", 1.0f);
		Invoke("replaceRobot" , 1.0f);
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
		if (sMeter.getSoundValue () <= 100 && sMeter.getSoundValue () > 75) {
			playSoundCountNew = 0;
//			playSound = true;

		}
		if (sMeter.getSoundValue () < 75 && sMeter.getSoundValue () > 50) {
			playSoundCountNew = 1;
//			playSound = true;


		}
		if (sMeter.getSoundValue () < 50 && sMeter.getSoundValue () > 25) {
			playSoundCountNew = 2;
//			playSound = true;


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

		alphaValues = lMeter.getAlphaValue ();
		GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (1, alphaValues);

		//------------------------------------LIGHT METER LEVEL CHECK--------------------------
//		if (lMeter.getLightValue () <= 100 && lMeter.getLightValue () > 75) {
//			lightCountNew = 0;
////			float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (-1, 1.0);
////			GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (-1);
//		}
//		if (lMeter.getLightValue () < 75 && lMeter.getLightValue () > 50) {
//			lightCountNew = 1;
////			float fadeTime = GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1, 0.75);
////			GameObject.Find ("GM").GetComponent<Fading> ().BeginFade (1, 1);
////			GameObject.Find ("GM").GetComponent<FadingLight> ().BeginFade (1, 0.2f);
//			Debug.Log ("FADE!!!!!!!");
//		}
//		if (lMeter.getLightValue () < 50 && lMeter.getLightValue () > 25) {
//			lightCountNew = 2;
//		}
//
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
		if (mMeter.getMovementValue () <= 100 && mMeter.getMovementValue () > 75) {
//			Debug.Log ("MLEVEL ONE");
			movementLevelOne ();
			moveLevelCountNew = 1;
		}
		if (mMeter.getMovementValue () < 75 && mMeter.getMovementValue () > 50) {
			moveLevelCountNew = 2;
		}
		if (mMeter.getMovementValue () < 50 && mMeter.getMovementValue () > 25) {
			moveLevelCountNew = 3;

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

	//---------------------------------METER CODE---------------------------------------
	public void startSoundMeter() {
		sSwitch = true;
		lSwitch = true;
		mSwitch = true;
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
	public void onTakeLightMed() {
		lMeter.incrementLight ();
	}

	//---------------------------------SOUND CODE---------------------------------------

	public void playLevelOneSound() {
		Debug.Log ("PLAY LEVEL 1 SOUND");
//		if (playSound == true && playSoundCount == 0) {
//			soundLevelOne.Play ();
//			playSound = false;
//		}
		soundLevelOne.Play ();
		stopLevelTwoSound ();
		stopLevelThreeSound ();
	}

	public void playLevelTwoSound() {
		Debug.Log ("PLAY LEVEL 2 SOUND");
		soundLevelTwo.Play ();
		stopLevelOneSound ();
		stopLevelThreeSound ();
	}

	public void playLevelThreeSound() {
		Debug.Log ("PLAY LEVEL 3 SOUND");
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
				movementTimerOne = 20.0f;
				movementTimerOneInvert = 5.0f;
				invertMovement = false;
			}
		}
	}
	public void movementLevelTwo() {

	}
	public void movementLevelThree() {

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
