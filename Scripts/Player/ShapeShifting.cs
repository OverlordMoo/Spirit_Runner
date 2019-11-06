using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeShifting : MonoBehaviour
{
    public Renderer player;
    public Material materialRed;
    public Material materialBlue;
    public Material materialBlack;
    public Material normalMaterial;
    public float timeLimit;

    public bool RedOn;
    public bool BlueOn;
    public bool BlackOn;
    public bool ShapeOn;
    public bool collectiblePicked;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") && RedOn == false && ShapeOn == false)                   //if key is pressed and no other shapes are on starts coroutine = shape
        {
            StartCoroutine(ToRedShape());
        }
        else if(Input.GetKeyDown("2") && BlueOn == false && ShapeOn == false)               //if key is pressed and no other shapes are on starts coroutine = shape
        {
            StartCoroutine(ToBlueShape());
        }
        else if (Input.GetKeyDown("3") && BlackOn == false && ShapeOn == false)             //if key is pressed and no other shapes are on starts coroutine = shape
        {
            StartCoroutine(ToBlackShape());
        }
    }
    IEnumerator ToRedShape()                                                                //coroutine for shape(names wil change)
    {
        RedOn = true;                                                                       //red shape activated
        ShapeOn = true;                                                                     //shape activated
        player.material = materialRed;                                                      //sets player objects material to material red
        yield return new WaitForSeconds(timeLimit);                                         //waits certain amount of time
        if (collectiblePicked == true)                                                      //if collectible is picked up
        {
            yield return new WaitForSeconds(1);                                             // adds exra second to shape duration
            collectiblePicked = false;                                                      
        }
        player.material = normalMaterial;                                                   // material back to normal, normal shape
        ShapeOn = false;                                                                    // no shapes active
        RedOn = false;                                                                      //red shape not active
        
    }
    IEnumerator ToBlueShape()
    {
        BlueOn = true;
        ShapeOn = true;
        player.material = materialBlue;
        yield return new WaitForSeconds(timeLimit);                                         //same as the above just different colour
        if(collectiblePicked == true)
        {
            yield return new WaitForSeconds(1);
            collectiblePicked = false;
        }
        player.material = normalMaterial;
        ShapeOn = false;
        BlueOn = false;
        

    }
    IEnumerator ToBlackShape()
    {
        BlackOn = true;
        ShapeOn = true;
        player.material = materialBlack;
        yield return new WaitForSeconds(timeLimit);                                        //same as red different colour
        if (collectiblePicked == true)
        {
            yield return new WaitForSeconds(1);
            collectiblePicked = false;
        }
        player.material = normalMaterial;
        ShapeOn = false;
        BlackOn = false;
      
    }
}
