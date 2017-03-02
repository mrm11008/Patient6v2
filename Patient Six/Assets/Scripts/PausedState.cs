using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : MonoBehaviour {

    bool paused = false;

    public void SetPausedState(bool state)
    {
        paused = state;
    }

    public bool GetPausedState()
    {
        return paused;
    }

}
