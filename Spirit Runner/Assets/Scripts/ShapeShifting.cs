﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifting : MonoBehaviour
{
    public Renderer player;
    public Material materialRed;
    public Material materialBlue;
    public Material materialBlack;
    public Material normalMaterial;
    public float redtimeLimit;
    public float bluetimeLimit;
    public float blacktimeLimit;
    public float collectibleTime;
    public float cooldown;

    public bool RedOn;
    public bool redFinished;
    public bool blueFinished;
    public bool BlueOn;
    public bool BlackOn;
    public bool ShapeOn;
    public bool collectiblePicked;

    private IEnumerator redShape;
    private IEnumerator blueShape;


    // Start is called before the first frame update
    void Start()
    {


        redFinished = true;
        blueFinished = true;
        RedOn = false;
    }
    void Update()
    {
        if (Input.GetKeyDown("1") && RedOn == false && redFinished == true)
        {

            if (BlueOn == true)
            {
                StopCoroutine(blueShape);
                BlueOn = false;
            }
            redShape = ToRedShape();
            StartCoroutine(redShape);
            StartCoroutine(CoolDown("red"));

        }
        else if (Input.GetKeyDown("2") && BlueOn == false && blueFinished == true)
        {

            if (RedOn == true)
            {
                StopCoroutine(redShape);
                RedOn = false;
            }
            blueShape = ToBlueShape();
            StartCoroutine(blueShape);
            StartCoroutine(CoolDown("blue"));
        }
    }


    IEnumerator ToRedShape()                                                                //coroutine for shape(names wil change)
    {
        RedOn = true;                                                                       //red shape activated

        ShapeOn = true;
        redFinished = false;
        player.material = materialRed;                                                      //sets player objects material to material red
        yield return new WaitForSeconds(redtimeLimit);                                      //waits certain amount of time
        if (collectiblePicked == true)                                                      //if collectible is picked up
        {
            redtimeLimit += collectibleTime;
            yield return new WaitForSeconds(collectibleTime);                               // adds exra second to shape duration 
            collectiblePicked = false;
        }
        //red shape not active
        if (BlueOn == false && BlackOn == false)
        {
            player.material = normalMaterial;                                                   // material back to normal, normal shape
            ShapeOn = false;
        }
        RedOn = false;
    }

    IEnumerator ToBlueShape()
    {
        BlueOn = true;
        ShapeOn = true;
        blueFinished = false;
        player.material = materialBlue;
        yield return new WaitForSeconds(bluetimeLimit);                                         //same as the above just different colour
        if (collectiblePicked == true)
        {
            bluetimeLimit += collectibleTime;
            yield return new WaitForSeconds(collectibleTime);
            collectiblePicked = false;
        }

        if (RedOn == false && BlackOn == false)
        {
            player.material = normalMaterial;
            ShapeOn = false;
        }
        BlueOn = false;
    }

    IEnumerator CoolDown(string _color)
    {

        yield return new WaitForSeconds(cooldown);
        if (_color == "red")
            redFinished = true;
        if (_color == "blue")
            blueFinished = true;

    }

    public void RedButton()
    {
        if (BlueOn == true)
        {
            StopCoroutine(blueShape);
            BlueOn = false;
        }
        redShape = ToRedShape();
        StartCoroutine(redShape);
        StartCoroutine(CoolDown("red"));
    }
    public void BlueButton()
    {
        if (RedOn == true)
        {
            StopCoroutine(redShape);
            RedOn = false;
        }
        blueShape = ToBlueShape();
        StartCoroutine(blueShape);
        StartCoroutine(CoolDown("blue"));
    }
}