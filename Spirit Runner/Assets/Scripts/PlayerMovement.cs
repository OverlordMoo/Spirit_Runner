﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForceUp;
    public float jumpForceFwd;
    public float slideTime;
    public bool jumping = false;
    public Animator playerAnim; //player body animator
    public float incrementGrowth;

    public SceneManager sceneManager;
    public float exitTime; //the time between player death and "gameover scene"

    public Vector3 movement;
    public GameObject PlayerBody;
    public Rigidbody PlayerRigidBody;
    public PlayerCollisionDetection playerColl;

    IEnumerator Slide()
    {
        playerColl.SetSlideTrue();
        PlayerBody.transform.localScale -= new Vector3(0, 0.3f, 0);
        yield return new WaitForSeconds(slideTime);
        playerColl.SetSlideFalse();
        PlayerBody.transform.localScale += new Vector3(0, 0.3f, 0);

    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene(1);

    }

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(0,0,1);
        PlayerRigidBody = PlayerBody.GetComponent<Rigidbody>();
        playerColl = PlayerBody.GetComponent<PlayerCollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerRigidBody.velocity.y>=0)
        {
            playerAnim.SetBool("Going_up", true);
        }
        if (PlayerRigidBody.velocity.y <= 0)
        {
            playerAnim.SetBool("Going_up", false);
        }
        //constant forward movement
        if (jumping==false)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space)&&jumping==false)
        {
            playerAnim.SetTrigger("Jump_start");
            jumping = true;
            Debug.Log("Hypättiin");
            PlayerRigidBody.AddForce(Vector3.up * jumpForceUp, ForceMode.Impulse);
            PlayerRigidBody.AddForce(Vector3.forward * jumpForceFwd, ForceMode.Impulse);
        }
        //slide
        if (Input.GetKeyDown("s") && jumping == false)
        {
            Debug.Log("slide");
            StartCoroutine(Slide());
        }
        //speed increase
        speed += Time.deltaTime * incrementGrowth;
    }



    //functions for player collisions
    public void WallCollisionDetected(PlayerCollisionDetection childScript)
    {
        speed = 0;
        Debug.Log("Osuttiin seinään");
        StartCoroutine(GameOver());
    }
    public void JumpLanded(PlayerCollisionDetection childScript)
    {
        Debug.Log("Osuttiin maahan");
        jumping = false;
        playerAnim.ResetTrigger("Jump_start");
    }
}
