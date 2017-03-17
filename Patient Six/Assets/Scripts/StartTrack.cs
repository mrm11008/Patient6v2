using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrack : MonoBehaviour {

    public AudioSource song1;
    private float timeOut;
    public float audio1Volume = 0.5f;
    private bool fading = false;

    void Start()
    {
        song1.volume = audio1Volume;
    }

    void Update()
    {
        song1.volume = audio1Volume;
        timeOut = Time.deltaTime;

        if (fading == true)
        {
            audio1Volume -= 0.25f * timeOut;

            if (audio1Volume <= 0f)
            {
                audio1Volume = 0f;
                timeOut = 0f;
            }
        }
    }

    public void musicfadeOut()
    {
        fading = true;
    }

}
