using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceActivated : MonoBehaviour
{
    public bool walkedThrough;

    //If the trigger is activated, bool walkedThrough changes to true.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBody")
        {
            walkedThrough = true;

            Transform daddy = gameObject.transform.parent;                                            //Shorten the next line, by assigning 
            Debug.Log("daddy = " + daddy);
            transform.parent.parent.GetComponent<MapGenerator>().pastQueue.Enqueue(daddy.gameObject);      //Enque this object's parent in the past objects list stored in MapGenerator.cs
            Debug.Log("PastQueue.next = " + transform.parent.parent.GetComponent<MapGenerator>().pastQueue.Peek());
            Debug.Log("Plim!");
        }
    }       
}
