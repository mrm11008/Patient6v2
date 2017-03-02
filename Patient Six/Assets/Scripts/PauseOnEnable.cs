using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEnable : MonoBehaviour {

    public PausedState pauser;

    void OnEnable()
    {
        pauser.SetPausedState(true);
    }

    void OnDisable()
    {
        pauser.SetPausedState(false);
    }

}
