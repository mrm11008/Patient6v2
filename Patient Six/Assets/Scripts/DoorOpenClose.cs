using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour {

    public AudioSource doorOpen;
    public Animator puzzleDoors;
    public GameObject soundTrigger;

    void OnTriggerEnter(Collider other)
    {
        doorOpen.Play();
    }

    void OnTriggerExit(Collider other)
    {
        puzzleDoors.GetComponent<Puzzle>().DoorTrigger();
    }
    
}
