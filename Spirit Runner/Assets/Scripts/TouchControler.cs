using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchControler : MonoBehaviour
{
    [Header("TouchScreen Variables")]
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    [Header("Player Variables")]
    public float speed;
    public float jumpForceUp;
    public float jumpForceFwd;
    public float slideTime;
    public bool jumping = false;
    public Animator playerAnim; //player body animator
    public float incrementGrowth;

    [Header("Turning")]
    public bool turnLeft;
    public bool turnRight;

    [Header("Game Over")]
    public SceneManager sceneManager;
    public float exitTime; //the time between player death and "gameover scene"

    [Header("Flying")]
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

        flyPos = new Vector3(transform.position.x, transform.position.y + flyHeight, transform.position.z);
        PlayerRigidBody.useGravity = false;

        while (transform.position.y < flyPos.y)
        {
            transform.Translate(Vector3.up * takeOffSpeed * Time.deltaTime);
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

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(exitTime);
        SceneManager.LoadScene(1);

    }

    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen

        // Player Variables
        movement = new Vector3(0, 0, 1);
        PlayerRigidBody = PlayerBody.GetComponent<Rigidbody>();
        playerColl = PlayerBody.GetComponent<PlayerCollisionDetection>();
    }

    void Update()
    {
        if (PlayerRigidBody.velocity.y >= 0)
        {
            playerAnim.SetBool("Going_up", true);
        }
        if (PlayerRigidBody.velocity.y <= 0)
        {
            playerAnim.SetBool("Going_up", false);
        }

        //constant forward movement
        if (jumping == false)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            playerAnim.SetTrigger("Strafe");
                            Debug.Log("Right Swipe");
                            turnRight = true;
                        }
                        else
                        {   //Left swipe
                            playerAnim.SetTrigger("Strafe_Left");
                            Debug.Log("Left Swipe");
                            turnLeft = true;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y && !jumping)  //If the movement was up
                        {   //Up swipe
                            jumping = true;
                            playerAnim.SetTrigger("Jump_start");
                            PlayerRigidBody.AddForce(Vector3.up * jumpForceUp, ForceMode.Impulse);
                            PlayerRigidBody.AddForce(Vector3.forward * jumpForceFwd, ForceMode.Impulse);
                            Debug.Log("Up Swipe");
                        }
                        else
                        {
                            if (!jumping)//Down swipe
                                StartCoroutine(Slide());
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        //speed increase
        speed += Time.deltaTime * incrementGrowth;

        if (hawkPicked == true)
        {
            if (flying == false)
            {
                StartCoroutine(Fly());
                hawkPicked = false;
            }

        }
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
    }
}
