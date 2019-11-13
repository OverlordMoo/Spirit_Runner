using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public GameObject standardModel;
    public GameObject ramModel;
    public GameObject tigerModel;

    private GameObject currentModel;
    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        currentModel = standardModel;   
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim = currentModel.GetComponent<Animator>();
    }
}
