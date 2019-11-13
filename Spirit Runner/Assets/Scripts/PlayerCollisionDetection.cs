using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public bool sliding;
    public Collider PlayerColl;

    public bool editing = false;
    //notifies parent on collision

    private void Start()
    {
        sliding = false;
        PlayerColl = gameObject.GetComponent<BoxCollider>();
    }
    public void SetSlideFalse()
    {
        sliding = false;
    }
    public void SetSlideTrue()
    {
        sliding = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        //call a function from the player controllers movement script depeding on what the body collides with
        if (collision.other.CompareTag("Fence") && sliding == true)
        {
            Physics.IgnoreCollision(collision.collider, PlayerColl);
        }
        if (collision.other.CompareTag("Wall") || collision.other.CompareTag("Fence") && sliding == false)
        {
            if (editing)
                transform.parent.GetComponent<PlayerMovement>().WallCollisionDetected(this);
            else
                transform.parent.GetComponent<TouchControler>().WallCollisionDetected(this);
        }
        if (collision.other.CompareTag("Base"))
        {
            if (editing)
                transform.parent.GetComponent<PlayerMovement>().JumpLanded(this);
            else
                transform.parent.GetComponent<TouchControler>().JumpLanded(this);
        }
        if (collision.other.CompareTag("RedEnemy"))                                              //Tag is for demo. See if hit red enemy.
        {
            if (transform.parent.GetComponent<ShapeShifting>().RedOn == true)                    //check if red shape is active from ShapeShifting.cs
            {
                Destroy(collision.gameObject, 0);                                               //if red active destroy enemy
            }
        }
        if (collision.other.CompareTag("BlueEnemy"))
        {
            if (transform.parent.GetComponent<ShapeShifting>().BlueOn == true)                  //same thinn as red except different colour
            {
                Destroy(collision.gameObject, 0);
            }
        }
        if (collision.other.CompareTag("BlackEnemy"))
        {
            if (transform.parent.GetComponent<ShapeShifting>().BlackOn == true)                 //same thinn as red except different colour
            {
                Destroy(collision.gameObject, 0);
            }
        }
        if (collision.other.CompareTag("Collectible"))                                          // if tag is collectible
        {
            Destroy(collision.gameObject, 0);                                                   //destroys collectible
            if (transform.parent.GetComponent<ShapeShifting>().ShapeOn == true)                  //if any shape is active                             
            {
                transform.parent.GetComponent<ShapeShifting>().collectiblePicked = true;         //collectible is picked, value is used in shapeshifting.cs
            }
        }
    }
}
