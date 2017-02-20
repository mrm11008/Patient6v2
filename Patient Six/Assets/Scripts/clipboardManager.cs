using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clipboardManager : MonoBehaviour
{

    [System.Serializable]
    public class clipData
    {
        public Clipboard clip;
        public GameObject boardIMG;
    }

    Clipboard clip;
    public List<clipData> clips;

    GameObject mainCamera;
    public GameObject clipboardUI;
    private GameObject activeImage;
    bool isShowing = false;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    void Update()
    {
        check();
    }

    void check()
    {
        if (Input.GetKeyDown("e"))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // check if clipboard
                clip = hit.collider.GetComponent<Clipboard>();

                //if is clipboard
                clipData clipMatch = null;
                for (int i = 0; i < clips.Count; i++)
                {
                    if (clips[i].clip == clip)
                    {
                        clipMatch = clips[i];
                        break;
                    }
                }
                if (isShowing == false)
                {
                    if (clipMatch != null)
                    {
                        clipboardUI.SetActive(true);
                        clipMatch.boardIMG.SetActive(true);
                        activeImage = clipMatch.boardIMG;
                        isShowing = true;
                    }
                }
                else
                {
                    clipboardUI.SetActive(false);
                    activeImage.SetActive(false);
                    isShowing = false;
                }
            }
        }
    }
}
