using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingLight : MonoBehaviour {
	public Texture fadeOutTexture;
	public float fadeSpeed = 0.0f;

	private int drawDepth = -1000;
	private float alpha = 0.0f;
	private float checkAlpha = 0.0f;
	private int fadeDir = -1;

	void OnGUI() {
//		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		if (alpha != checkAlpha) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;
		}
		if (alpha >= checkAlpha) {
			alpha = checkAlpha;
		}
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade(int direction, float levelAlpha) {
		fadeDir = direction;
//		alpha = levelAlpha;
		checkAlpha = levelAlpha;
//		alpha += fadeDir * fadeSpeed * Time.deltaTime;
//		alpha = Mathf.Clamp01 (alpha);


		return (fadeSpeed);
	}

//	void onLevelWasLoaded() {
//		BeginFade (-1);
//	}
}
