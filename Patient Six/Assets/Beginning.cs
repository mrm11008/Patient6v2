using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning : MonoBehaviour {

    private float timer = 0;
    public bool hospitalplay = false;
    public AudioSource beginning;
    public GameObject openingEnd;
    public GameObject tutorialui;
    public GameObject self;

    void Start () {
        beginning = GetComponent<AudioSource>();
    }
	
	void Update () {

        timer += Time.deltaTime;
        Debug.Log(timer);

        if (timer >= 14)
        {
            openingEnd.SetActive(true);
        }

        if (timer >= 16.5)
        {
            tutorialui.SetActive(true);
            self.SetActive(false);
        }

    }

    public void playHospital()
    {
        beginning.Play();
    }

}
