using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour {

    public Image box1;
    public Image box2;
    public Image box3;
    public Image box4;
    public Image box5;
    public Image box6;
    int i = 0; //box 1
    int c = 1; //box 2
    int s = 2; //box 3
    int r = 3; //box 4
    int t = 4; //box 5
    int n = 5; //box 6
    public Sprite[] sprites;
    public AudioSource click;

    //Solved
    public GameObject puzzleuihide;
    public GameObject mechUI;
    public Collider pdoor;
    public GameObject soundTrigger;

    //Door Triggers
    public Animator doorLeft;
    public Animator doorRight;

    void Start()
    {
        box1.sprite = sprites[i];
        box2.sprite = sprites[c];
        box3.sprite = sprites[s];
        box4.sprite = sprites[r];
        box5.sprite = sprites[t];
        box6.sprite = sprites[n];

        pdoor.GetComponent<BoxCollider>();
    }
    
    public void switchImageDown()
    {
        if (i < sprites.Length)
        {
            ++i;
            box1.sprite = sprites[i];
            click.Play();
            
        }
        
    }

    public void switchImageDown2()
    {
        if (c < sprites.Length)
        {
            ++c;
            box2.sprite = sprites[c];
            click.Play();
        }

    }

    public void switchImageDown3()
    {
        if (s < sprites.Length)
        {
            ++s;
            box3.sprite = sprites[s];
            click.Play();
        }

    }

    public void switchImageDown4()
    {
        if (r < sprites.Length)
        {
            ++r;
            box4.sprite = sprites[r];
            click.Play();
        }

    }

    public void switchImageDown5()
    {
        if (t < sprites.Length)
        {
            ++t;
            box5.sprite = sprites[t];
            click.Play();
        }

    }

    public void switchImageDown6()
    {
        if (n < sprites.Length)
        {
            ++n;
            box6.sprite = sprites[n];
            click.Play();
        }

    }

    public void switchImageUp()
    {
        if (i < sprites.Length)
        {
            --i;
            box1.sprite = sprites[i];
            click.Play();
        }

    }

    public void switchImageUp2()
    {
        if (c < sprites.Length)
        {
            --c;
            box2.sprite = sprites[c];
            click.Play();
        }

    }

    public void switchImageUp3()
    {
        if (s < sprites.Length)
        {
            --s;
            box3.sprite = sprites[s];
            click.Play();
        }

    }

    public void switchImageUp4()
    {
        if (r < sprites.Length)
        {
            --r;
            box4.sprite = sprites[r];
            click.Play();
        }

    }

    public void switchImageUp5()
    {
        if (t < sprites.Length)
        {
            --t;
            box5.sprite = sprites[t];
            click.Play();
        }

    }

    public void switchImageUp6()
    {
        if (n < sprites.Length)
        {
            --n;
            box6.sprite = sprites[n];
            click.Play();
        }

    }

    void Update()
    {
        if (i == 6)
        {
            i = 0;
            box1.sprite = sprites[i];
        }

        if (i == -1)
        {
            i = 5;
            box1.sprite = sprites[i];
        }
        //

        if (c == 6)
        {
            c = 0;
            box2.sprite = sprites[c];
        }

        if (c == -1)
        {
            c = 5;
            box2.sprite = sprites[c];
        }
        //

        if (s == 6)
        {
            s = 0;
            box3.sprite = sprites[s];
        }

        if (s == -1)
        {
            s = 5;
            box3.sprite = sprites[s];
        }
        //

        if (r == 6)
        {
            r = 0;
            box4.sprite = sprites[r];
        }

        if (r == -1)
        {
            r = 5;
            box4.sprite = sprites[r];
        }
        //

        if (t == 6)
        {
            t = 0;
            box5.sprite = sprites[t];
        }

        if (t == -1)
        {
            t = 5;
            box5.sprite = sprites[t];
        }
        //

        if (n == 6)
        {
            n = 0;
            box6.sprite = sprites[n];
        }

        if (n == -1)
        {
            n = 5;
            box6.sprite = sprites[n];
        }
        //

        PuzzleSolve();

        if (!soundTrigger.activeSelf)
        {
            pdoor.enabled = true;
        }

    }

    void PuzzleSolve()
    {
        //Puzzle's Combination Key
        if (i == 3 && c == 1 && s == 4 && r == 5 && t == 0 && n == 2)
        {
            //Door Triggers Reset
            doorLeft.SetTrigger("doorOpen");
            doorRight.SetTrigger("doorOpen");
            doorLeft.ResetTrigger("doorClose");
            doorRight.ResetTrigger("doorClose");
            pdoor.enabled = false;
            soundTrigger.SetActive(true);

            //Close Puzzle
            Cursor.lockState = CursorLockMode.Locked;
            mechUI.SetActive(false);
            PuzzleRestart();
            puzzleuihide.SetActive(false);

        }

    }

    public void PuzzleRestart()
    {
        i = 0;
        c = 1;
        s = 2;
        r = 3;
        t = 4;
        n = 5;
        box1.sprite = sprites[i];
        box2.sprite = sprites[c];
        box3.sprite = sprites[s];
        box4.sprite = sprites[r];
        box5.sprite = sprites[t];
        box6.sprite = sprites[n];
    }

}
