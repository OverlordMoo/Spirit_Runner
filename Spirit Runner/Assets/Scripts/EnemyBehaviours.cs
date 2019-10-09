using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviours : MonoBehaviour
{
    /* IMPORTANT! This is a script dependant on a box collider 
     * that is set as a trigger infront of the enemy 
     * Place it as a child to the enemy (in the enemy prefabs)
     * 
     * This scripts purpose is to work with all enemies 
     * and can be managed by the "Enemy Type" bools 
     * to assign the enemy outside the script.
     */

    [Header("Enemy Type")]
    public bool goblin;
    public bool golem;
    public bool other; // referring to the 3rd enemy type


    [Header("Outside Variable Collection")]
    public ShapeShifting userState;
    public BoxCollider attackState;


    // Start is called before the first frame update
    void Start()
    {
        attackState = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider rigidbody) // Checks collision for rigidbody of the player (isn't limited to that)
    {
        userState = rigidbody.GetComponentInParent<ShapeShifting>(); // Checks Shapeshifting script to determine the enemy behaviour
        if (goblin == true)
            if (!userState.BlueOn)
                Debug.Log("BLUE ATTACK!");
                // animation is needed here
            else if (userState.BlueOn)
                Debug.Log("Retreat!");
                // if animation is needed fill here
        if (golem == true)
            if (!userState.BlackOn)
                Debug.Log("FORTIFY!");
                // animation is needed here
            else if (userState.BlackOn)
                Debug.Log("Oh no");
                // if animation is needed fill here
        if (other == true)
            if (!userState.RedOn)
                Debug.Log("Stand Ground!");
                // animation is needed here
            else if (userState.RedOn)
                Debug.Log("RETREAT!");
                // if animation is needed fill here   
    }



}
