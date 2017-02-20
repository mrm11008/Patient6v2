using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanics : MonoBehaviour {
    public Renderer rend;
    public GameObject mechUI;
    public PausedState paused;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        mechUI.SetActive(true);
    }
    void OnMouseExit()
    {
        mechUI.SetActive(false);
    }

    void Update()
    {
        if (paused.GetPausedState())
        {
            mechUI.SetActive(false);
        }
    }
}
