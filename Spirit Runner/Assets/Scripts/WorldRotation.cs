using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotation : MonoBehaviour
{
    public float rotateAngle = 45;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //sideways movement by rotating the level
        if (Input.GetKeyDown("d"))
        {
            transform.Rotate(0, -rotateAngle,0);
        }
        if (Input.GetKeyDown("a"))
        {
            transform.Rotate(0, rotateAngle,0);
        }
    }
 
}
