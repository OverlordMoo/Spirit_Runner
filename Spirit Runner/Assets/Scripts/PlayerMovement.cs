using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForceUp;
    public float jumpForceFwd;
    public float slideTime;
    public bool jumping = false;
    public Animator playerAnim; //player body animator
    public float incrementGrowth;

    public bool flying;
    public float flyTime;
    public float flyHeight;
    public float takeOffSpeed;
    public bool hawkPicked;
    public Vector3 flyPos;
    public Vector3 landPos;
    

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

    IEnumerator Fly()
    {
        
        flying = true;
        
        flyPos = new Vector3(transform.position.x, transform.position.y + flyHeight , transform.position.z);
        PlayerRigidBody.useGravity = false;
        
        while (transform.position.y < flyPos.y)
        {
            transform.Translate(Vector3.up*takeOffSpeed*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(flyTime);
        landPos = new Vector3(transform.position.x, transform.position.y - flyHeight, transform.position.z);
        while (transform.position.y > landPos.y)
        {
            transform.Translate(Vector3.down * takeOffSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        PlayerRigidBody.useGravity = true;
        flying = false;
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector3(0,0,1);
        PlayerRigidBody = PlayerBody.GetComponent<Rigidbody>();
        playerColl = PlayerBody.GetComponent<PlayerCollisionDetection>();
        playerAnim = PlayerBody.GetComponent<Animator>();
        hawkPicked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //constant forward movement
        if (jumping==false)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space)&&jumping==false && flying == false)
        {
            jumping = true;
            Debug.Log("Hypättiin");
            PlayerRigidBody.AddForce(Vector3.up * jumpForceUp, ForceMode.Impulse);
            PlayerRigidBody.AddForce(Vector3.forward * jumpForceFwd, ForceMode.Impulse);
        }
        //slide
        if (Input.GetKeyDown("s") && jumping == false && flying == false)
        {
            Debug.Log("slide");
            StartCoroutine(Slide());
        }
        if(hawkPicked == true)
        {
            if (flying == false )
            {
                StartCoroutine(Fly());
                hawkPicked = false;
            }
            
        }
        //speed increase
        speed += Time.deltaTime * incrementGrowth;
    }



    //functions for player collisions
    public void WallCollisionDetected(PlayerCollisionDetection childScript)
    {
        speed = 0;
        Debug.Log("Osuttiin seinään");
    }
    public void JumpLanded(PlayerCollisionDetection childScript)
    {
        Debug.Log("Osuttiin maahan");
        jumping = false;
    }
}
