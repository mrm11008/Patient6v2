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

	public CharacterSounds audso;
	public Rigidbody rb;

	public float speed = 10.0f;
	public float length = 0;

	private Vector3 movementVec;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		audso = gameObject.GetComponent<CharacterSounds> ();
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis ("Vertical") * speed;
		float straffe = Input.GetAxis ("Horizontal") * speed;
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
		if (Input.GetAxis ("Vertical") > 0) {
			transform.Translate (0, 0, translation);
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			transform.Translate (straffe / 50, 0, 0);
		}
		if (Input.GetAxis ("Vertical") < 0) {
			transform.Translate (0, 0, translation);
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			transform.Translate (straffe / 50, 0, 0);

		}

		positionTwo = transform.position;

		if (positionOne != positionTwo && audso.sourceTwoIsPlaying() == false) {
			Debug.Log ("play footsteps");
			audso.playFootsteps ();
		}

		if (positionOne == positionTwo) {
			audso.stopFootsteps ();
		}

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}




	}
}
