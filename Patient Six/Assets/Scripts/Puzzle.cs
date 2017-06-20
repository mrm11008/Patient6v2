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
    int i = 5; //box 1
    int c = 3; //box 2
    int s = 4; //box 3
    int r = 2; //box 4
    int t = 0; //box 5
    int n = 1; //box 6
    public Sprite[] sprites;
    public AudioSource click;
    public Animator buttoncase;
    public Collider buttoncaseCol;

    //Solved
    public GameObject puzzleuihide;
    public GameObject woot;
    //public GameObject mechUI;
    public Collider pdoor;
    public GameObject soundTrigger;
    public AudioSource doorCloseSound;

    //Door Triggers
    //public Animator puzzleDoors;

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
			if (i + 1 > 5) {
				i = 0;
			} else {
				++i;
			}
            
            box1.sprite = sprites[i];
            click.Play();
            
        }
        
    }

    public void switchImageDown2()
    {
        if (c < sprites.Length)
        {
			if (c + 1 > 5) {
				c = 0;
			} else {
				++c;
			}

            box2.sprite = sprites[c];
            click.Play();
        }

    }

    public void switchImageDown3()
    {
        if (s < sprites.Length)
        {
			if (s + 1 > 5) {
				s = 0;
			} else {
				++s;
			}
            box3.sprite = sprites[s];
            click.Play();
        }

    }

    public void switchImageDown4()
    {
        if (r < sprites.Length)
        {
			if (r + 1 > 5) {
				r = 0;
			} else {
				++r;
			}
            box4.sprite = sprites[r];
            click.Play();
        }

    }

    public void switchImageDown5()
    {
        if (t < sprites.Length)
        {
			if (t + 1 > 5) {
				t = 0;
			} else {
				++t;
			}
            box5.sprite = sprites[t];
            click.Play();
        }

    }

    public void switchImageDown6()
    {
        if (n < sprites.Length)
        {
			if (n + 1 > 5) {
				n = 0;
			} else {
				++n;
			}
            box6.sprite = sprites[n];
            click.Play();
        }

    }

    public void switchImageUp()
    {
        if (i < sprites.Length)
        {
			if (i - 1 < 0) {
				i = 5;
			} else {
				--i;
			}
            
            box1.sprite = sprites[i];
            click.Play();
        }

    }

    public void switchImageUp2()
    {
        if (c < sprites.Length)
        {
			if (c - 1 < 0) {
				c = 5;
			} else {
				--c;
			}
            box2.sprite = sprites[c];
            click.Play();
        }

    }

    public void switchImageUp3()
    {
        if (s < sprites.Length)
        {
			if (s - 1 < 0) {
				s = 5;
			} else {
				--s;
			}
			print (s);
            box3.sprite = sprites[s];
            click.Play();
        }

    }

    public void switchImageUp4()
    {
        if (r < sprites.Length)
        {
			if (r - 1 < 0) {
				r = 5;
			} else {
				--r;
			}
            box4.sprite = sprites[r];
            click.Play();
        }

    }

    public void switchImageUp5()
    {
        if (t < sprites.Length)
        {
			if (t - 1 < 0) {
				t = 5;
			} else {
				--t;
			}
            box5.sprite = sprites[t];
            click.Play();
        }

    }

    public void switchImageUp6()
    {
        if (n < sprites.Length)
        {
			if (n - 1 < 0) {
				n = 5;
			} else {
				--n;
			}
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

        //if (!soundTrigger.activeSelf)
        //{
        //    pdoor.enabled = true;
        //}

    }

    void PuzzleSolve()
    {
        //Puzzle's Combination Key
        if (i == 0 && c == 2 && s == 3 && r == 1 && t == 4 && n == 5)
        {
            //Door Triggers Reset
            //puzzleDoors.SetTrigger("openDoors");
            //puzzleDoors.ResetTrigger("closeDoors");
            //pdoor.enabled = false;
            //soundTrigger.SetActive(true);

            //Close Puzzle
            Cursor.lockState = CursorLockMode.Locked;
            //mechUI.SetActive(false);
            PuzzleRestart();
            puzzleuihide.SetActive(false);

            buttoncase.SetTrigger("openCase");
            buttoncaseCol.GetComponent<Collider>().enabled = false;

        }

    }

    public void PuzzleRestart()
    {
        i = 5;
        c = 3;
        s = 4;
        r = 2;
        t = 0;
        n = 1;
        box1.sprite = sprites[i];
        box2.sprite = sprites[c];
        box3.sprite = sprites[s];
        box4.sprite = sprites[r];
        box5.sprite = sprites[t];
        box6.sprite = sprites[n];
    }

    public void DoorTrigger()
    {
        //doorCloseSound.Play();
        //puzzleDoors.SetTrigger("closeDoors");
        //puzzleDoors.ResetTrigger("openDoors");
        //soundTrigger.SetActive(false);
    }

}
