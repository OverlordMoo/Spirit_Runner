using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public Animator anim;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject ingameMenu;
    private bool sett;                   //Settings menu is true or false

    void Start()
    {
        anim = GetComponent<Animator>();
        mainMenu = GetComponent<GameObject>();
    }

    public void Play()
    {
        //Play.anim("StartGame");
        Debug.Log("Starting Game");
        //we'll use this for now
        mainMenu.SetActive(false);
        ingameMenu.SetActive(true);

    }

    public void Settings()
    {
        Debug.Log("Starting Settings");
        sett = true;
    }

    public void Credits()
    {
        Debug.Log("Starting Credits");
    }

    public void ExitGame()
    {
        if (sett)
        {
            Debug.Log("Exiting Settings");
            sett = false;
        }
        else
        {
            Debug.Log("Exiting Game");
        }
    }
}