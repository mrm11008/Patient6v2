using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanBehaviour : MonoBehaviour {

	public Transform _Pivot;

	public float speed = 100f;
	public float maxAngle = 18f;

	float curAngle = 0f;
	public float distanceToSee = 50;
	RaycastHit whatIHit;
	private bool canLeanLeft = false;
	private bool canLeanRight = false;
	// Use this for initialization
	void Awake () {
//		if (_Pivot == null && transform.parent != null) _Pivot = transform.parent;
	}

	// Update is called once per frame
	void Update () {

		Debug.DrawRay (this.transform.position, (transform.TransformDirection(Vector3.left)) * 1, Color.red);
		Debug.DrawRay (this.transform.position, (transform.TransformDirection(Vector3.right)) * 1, Color.red);
		//LEAN LEFT CHECK
		if (Physics.Raycast (this.transform.position, (transform.TransformDirection (Vector3.left)) * 1, out whatIHit, 1.0f)) {
			canLeanLeft = false;
		} else {
			canLeanLeft = true;
		}
		//LEAN RIGHT CHECK
		if (Physics.Raycast (this.transform.position, (transform.TransformDirection (Vector3.right)) * 1, out whatIHit, 1.0f)) {
			canLeanRight = false;
		} else {
			canLeanRight = true;
		}

		// lean left
		if (Input.GetKey(KeyCode.Q))
		{
			if (canLeanLeft) {
				curAngle = Mathf.MoveTowardsAngle(curAngle, maxAngle, speed * Time.deltaTime);
			}
		}
		// lean right
		else if (Input.GetKey(KeyCode.E))
		{
			if (canLeanRight) {
				curAngle = Mathf.MoveTowardsAngle(curAngle, -maxAngle, speed * Time.deltaTime);
			}
		}
		// reset lean
		else
		{
			curAngle = Mathf.MoveTowardsAngle(curAngle, 0f, speed * Time.deltaTime);
		}

		_Pivot.transform.localRotation = Quaternion.AngleAxis(curAngle, Vector3.forward);
	}

}