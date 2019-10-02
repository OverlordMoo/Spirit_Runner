using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> map;
    public List<GameObject> pieces;

    public int visiblePieces;
    public int pastPieces;
    public GameObject piecePrefab1;
    public GameObject piecePrefab2;
    public GameObject piecePrefab3;

    private int numOfPieceOptions;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {

            map.Add(child.gameObject);
        }

        pieces.Add(piecePrefab1);
        pieces.Add(piecePrefab2);
        pieces.Add(piecePrefab3);
        numOfPieceOptions = pieces.Count -2;
    }

    // Update is called once per frame
    void Update()
    {
        while(map.Count < visiblePieces)
        {
            GenerateMap();
        }
        //RemovePastMap();
    }

    void GenerateMap()
    {
        //Create random for choosing the next piece
        System.Random rnd = new System.Random();
        int rInt = rnd.Next(0, numOfPieceOptions);
        //GameObject piece = pieces[rInt];

        GameObject last = map[map.Count - 1];                          //Last = the last generated piece
        Vector3 newZ = new Vector3(0, 0, 12f);                        //next piece should be generated Z+6.5F
        Vector3 newLocation = last.transform.position + newZ;          //Set new location for next piece

        GameObject nextPiece = Instantiate(pieces[rInt], newLocation, Quaternion.identity);    //Instantiate new piece
        nextPiece.transform.Rotate(-90f, 0f, 0f, Space.Self);
        map.Add(nextPiece);                                         //Put the new piece to the list
        transform.parent = gameObject.transform;                    //Put new piece under Generator object
    }


    void RemovePastMap()
    {

    }
}
