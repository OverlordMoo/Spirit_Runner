using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotation : MonoBehaviour
{
    public TouchControler input;
    public float rotateAngle = 45;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //sideways movement by rotating the level
        if (Input.GetKeyDown("d") || input.turnRight == true)
        {
            Debug.Log("turning right");
            transform.Rotate(0, -rotateAngle, 0);
            input.turnRight = false;
            
        }
        else if (Input.GetKeyDown("a") || input.turnLeft == true)
        {
            Debug.Log("turning left");
            transform.Rotate(0, rotateAngle, 0);
            input.turnLeft = false;

        }
    }
}