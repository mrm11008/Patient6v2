using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour {

    public AudioSource doorOpen;
    public AudioSource doorClose;
    public Animator doorLeftClose;
    public Animator doorRightClose;
    public GameObject soundTrigger;

    void OnTriggerEnter(Collider other)
    {
        doorOpen.Play();
    }

    void OnTriggerExit(Collider other)
    {
        doorClose.Play();

        doorLeftClose.SetTrigger("doorClose");
        doorRightClose.SetTrigger("doorClose");
        doorLeftClose.ResetTrigger("doorOpen");
        doorRightClose.ResetTrigger("doorOpen");
        soundTrigger.SetActive(false);
    }

}
