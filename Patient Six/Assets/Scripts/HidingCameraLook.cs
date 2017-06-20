using UnityEngine;
using System.Collections;

public class HidingCameraLook : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	//	public float sensitivity = 5.0f;
	public float sensitivity = 2.5f;

	public float smoothing = 2.0f;
	public PausedState paused;
	public GameObject pausedScreen;
	public bool PauseisShowing = false;
	GameObject character;
	public GameObject camera;

	// Use this for initialization
	void Start () {
		camera = this.transform.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (!paused.GetPausedState ()) {

			var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

			md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
//			smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
			mouseLook += smoothV;
			mouseLook.y = Mathf.Clamp (mouseLook.y, -120f, -50f);
			mouseLook.x = Mathf.Clamp (mouseLook.x, -120f, -50f);
//			mouseLook.x = Mathf.Clamp(mouseLook.x, -45f, 45f);

//			transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
			camera.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, camera.transform.up);

		}

	}

}
