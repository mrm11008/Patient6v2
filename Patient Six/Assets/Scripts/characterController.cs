using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {
	//-----------------PRACTICE CODE----------------------
//	public float playerAcceleration = 500;
//	public float maxSpeed = 20;
//	public Vector2 horizontalMovement;
//	public float deaceleration;
//	public float deacelerationX;
//	public float deacelerationZ;
//	public float jumpSpeed = 20;
//	public float maxSlope = 60;

	public Vector3 positionOne;
	public Vector3 positionTwo;

    public PausedState paused;

    public CharacterSounds audso;
	public Rigidbody rb;

	public float speed = 10.0f;
	public float length = 0;
	public int getOutCount = 0;
	public bool hidden = false;

	private Vector3 movementVec;
	public bool invertMove = false;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		audso = gameObject.GetComponent<CharacterSounds> ();
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		invertMove = GM.instance.getMovementTrigger ();
        if (!paused.GetPausedState())
        {
			if (invertMove == false) {

				float translation = Input.GetAxis("Vertical") * speed;
				float straffe = Input.GetAxis("Horizontal") * speed;
				//		float translation = Input.GetAxis ("Vertical");
				//		float straffe = Input.GetAxis ("Horizontal");

				//		movementVec = new Vector3 (straffe, 0, translation);
				//		movementVec = movementVec.normalized;

				translation *= Time.deltaTime;
				straffe += Time.deltaTime;

				positionOne = transform.position;


				//		if (Input.GetAxis ("Vertical") > 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//		}
				//		if (Input.GetAxis ("Horizontal") > 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//		}
				//		if (Input.GetAxis ("Vertical") < 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//		}
				//		if (Input.GetAxis ("Horizontal") < 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//
				//		}
				if (Input.GetAxis("Vertical") > 0)
				{
					transform.Translate(0, 0, translation / 2);
				}
				if (Input.GetAxis("Horizontal") > 0)
				{
					transform.Translate(straffe / 70, 0, 0);
				}
				if (Input.GetAxis("Vertical") < 0)
				{
					transform.Translate(0, 0, translation / 2);
				}
				if (Input.GetAxis("Horizontal") < 0)
				{
					transform.Translate(straffe / 70, 0, 0);

				}
			}
			if (invertMove == true) {
				Debug.Log ("INVERT MOVEMENT!");
				float translation = Input.GetAxis("Vertical") * speed;
				float straffe = Input.GetAxis("Horizontal") * speed;
				//		float translation = Input.GetAxis ("Vertical");
				//		float straffe = Input.GetAxis ("Horizontal");

				//		movementVec = new Vector3 (straffe, 0, translation);
				//		movementVec = movementVec.normalized;

				translation *= Time.deltaTime;
				straffe += Time.deltaTime;

				positionOne = transform.position;


				//		if (Input.GetAxis ("Vertical") > 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//		}
				//		if (Input.GetAxis ("Horizontal") > 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//		}
				//		if (Input.GetAxis ("Vertical") < 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//		}
				//		if (Input.GetAxis ("Horizontal") < 0) {
				//			transform.Translate (straffe / 100, 0, translation);
				//
				//		}
				if (Input.GetAxis("Vertical") > 0)
				{
					transform.Translate(0, 0, -translation / 2);
				}
				if (Input.GetAxis("Horizontal") > 0)
				{
					transform.Translate(-straffe / 70, 0, 0);
				}
				if (Input.GetAxis("Vertical") < 0)
				{
					transform.Translate(0, 0, -translation / 2);
				}
				if (Input.GetAxis("Horizontal") < 0)
				{
					transform.Translate(-straffe / 70, 0, 0);

				}
			}


            positionTwo = transform.position;

            if (positionOne != positionTwo && audso.sourceTwoIsPlaying() == false)
            {
                Debug.Log("play footsteps");
                audso.playFootsteps();
            }

            if (positionOne == positionTwo)
            {
                audso.stopFootsteps();
            }

            if (Input.GetKeyDown("escape"))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "MeterTrigger") {

			if (getOutCount == 0) {
				GM.instance.startSoundMeter ();
				audso.playGetOut ();
				getOutCount++;
			}

		}
		if (other.gameObject.tag == "Hide") {

			hidden = true;
//			Debug.Log ("PLAYER IS HIDING");
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Hide") {
			hidden = false;
//			Debug.Log ("PLAYER IS NOT HIDING");
		}
	}


	public bool CheckHidden() {
		return hidden;
	}
}
