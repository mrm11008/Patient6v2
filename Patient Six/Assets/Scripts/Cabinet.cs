using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : MonoBehaviour {

    public Animator anim;
    private bool cabinetOpen;
    public Collider cdoors;

    public void OpenCabinet()
    {
        if (cabinetOpen == false)
        {
            anim.SetTrigger("cabinetOpen");
            anim.ResetTrigger("cabinetClose");
            cdoors.enabled = false;
            cabinetOpen = true;
        }
        else
        {
            anim.SetTrigger("cabinetClose");
            anim.ResetTrigger("cabinetOpen");
            cdoors.enabled = true;
            cabinetOpen = false;

        }
    }
}
