using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapGenerator : MonoBehaviour
{
    public Queue<GameObject> map = new Queue<GameObject>();         //What pieces are on the map
    public List<GameObject> pieceOptions;                           //Options for pieces
    public Queue<GameObject> pastQueue = new Queue<GameObject>();   //A queue to hold past pieces

    public int visiblePieces;                                       //Map infront of player
    public int numofPastPieces;                                     //Number of map pieces player has passed that will be shown
    public GameObject piecePrefabBackupCatcher;                     //This is just a backup, if it can't find prefabs from folder.

    private int rotateAngle;                                        //Randomly created value of new piece rotation
    private int numOfPieceOptions;                                  //Automatically calculated amount of possible pieces
    private GameObject lastPiece;                                   //The last piece created


    // Start is called before the first frame update
    void Start()
    {
        //***Finds all pieces already in world and adds to Queue
        foreach (Transform child in gameObject.transform)
        {
            map.Enqueue(child.gameObject);
            lastPiece = child.gameObject;
        }

        //***Finds all objects in folder Assets/Prefabs/MapPieces/CreateUS and adds them to possible generations.
        var prefabsInFolder = Resources.LoadAll("MapPieces/CreateUs");

        //Debug.Log("PrefabsInFolder = " + prefabsInFolder + " , Count = " + prefabsInFolder.Length);
        
        foreach (var prefab in prefabsInFolder)
        {
            pieceOptions.Add((GameObject)prefab);
            //Debug.Log("I found the prefab " + prefab);
        }

        //Debug.Log("pieceOptions = " + pieceOptions);
        //foreach (GameObject option in pieceOptions)
        //{
        //    Debug.Log("Option = " + option);
        //}

        if(pieceOptions.Count == 0)
        {
            pieceOptions.Add(piecePrefabBackupCatcher);
        }

        numOfPieceOptions = pieceOptions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        while(map.Count < visiblePieces)
        {
            GenerateMap();
        }

        if (pastQueue.Count > 2)
        {
            RemovePastMap();
        }
    }


    /// <summary>
    /// Generates map by choosing the piece by random, rotating it by random and adding to all the lists and parents necessary
    /// </summary>
    void GenerateMap()
    {
        //***Random for next piece
        System.Random rnd = new System.Random();                        //Create random for choosing the next piece
        int rInt = rnd.Next(0, numOfPieceOptions*1000);                 //random from larger numberpool for more variables
        rInt = (Decimal.ToInt32(Math.Round((decimal)rInt / 100, 0, MidpointRounding.AwayFromZero) * 100));  //Round to nearest 100
        rInt = rInt / 1000;                                             //Divide to reasonable numbers
        Debug.Log("rInt = " + rInt);

        //GameObject piece = pieces[rInt];

        //***Location of next piece
        Vector3 newZ = new Vector3(0, 0, 12f);                          //next piece should be generated Z+6.5F
        Vector3 newLocation = lastPiece.transform.position + newZ;      //Set new location for next piece
        Debug.Log(lastPiece);

        //***Random for angle
        System.Random rndAngle = new System.Random();                   //Create a new random for choosing rotation of piece
        int rAngle = rndAngle.Next(0, 600);                             //More numbers to make more random
        rAngle = (Decimal.ToInt32(Math.Round((decimal)rAngle / 100, 0, MidpointRounding.AwayFromZero) * 100));  //Round to nearest 100
        rAngle = rAngle / 100;                                          //Divide to reasonable numbers
        Debug.Log("rAngle = " + rAngle);
        rotateAngle = rAngle * 45;                                      //Rotation is one of the 6 sides, so * 45 degrees 

        //***Create map piece & Organize
        GameObject nextPiece = Instantiate(pieceOptions[rInt], newLocation, Quaternion.identity);    //Instantiate new piece
        nextPiece.transform.Rotate((float)rotateAngle, -90f, -90F, Space.Self);           //Rotate piece
        map.Enqueue(nextPiece);                                         //Queues new piece
        nextPiece.transform.parent = gameObject.transform;              //Puts new piece under Generator object
        lastPiece = nextPiece;                                          //Last = the last generated piece
        Debug.Log("lastPiece = " + lastPiece);

    }


    /// <summary>
    /// Remove past map pieces
    /// </summary>
    void RemovePastMap()
    {
        //Find all objects in map
        foreach(GameObject childObject in map)
        {
            //Find their child objects
            foreach(Transform grandChild in childObject.transform)
            {
                if(grandChild.gameObject.tag == "Checkpoint")
                {
                    Debug.Log("GrandChild = " + grandChild);
                    bool walked = grandChild.GetComponent<PieceActivated>().walkedThrough;       //Find if piece is past

                    Debug.Log("Past = " + pastQueue.Peek());
                    Debug.Log("Walked = " + walked);

                    if (walked == true)
                    {
                        if (map.Count > 2)
                        {

                            GameObject removablePiece = map.Peek();             //Get object first in queue
                            map.Dequeue();                                      //Remove from queue
                            pastQueue.Dequeue();
                            Debug.Log(removablePiece);
                            Destroy(removablePiece, 0.5f);                      //Destroys the gameObject that is the first in list after 0.5 seconds 
                        }

                    }
                }
            }
                //if(grandchildTransform.gameObject.tag == "Checkpoint")
                //{
                //    bool walked = GetComponent<PieceActivated>().walkedThrough;
                //}
            }
    }
}
