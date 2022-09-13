using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charicterController : MonoBehaviour
{

    

    CharacterController playerCon;

    Rigidbody playerRb;
    

    // Start is called before the first frame update
    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
    }

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    //tracked vectors 
    Vector3 moveDir;
    Vector3 velocity;

    bool desiredJump;

    // Update is called once per frame
    void Update()
    {


        desiredJump |= Input.GetButtonDown("Jump");
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        

        

    }

    private void FixedUpdate()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 acceleration =
            new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        Vector3 desiredVelocity =
            new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        velocity = playerRb.velocity;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
       
        desiredJump |= Input.GetButtonDown("Jump");
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }


        playerRb.velocity = velocity;


 

    }


    

    void Jump()
    {
        //Debug.Log("jump");
        velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    }





    /*
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        Vector3 velocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        Vector3 displacement = velocity * Time.deltaTime;
        transform.localPosition += displacement;
     */
}
