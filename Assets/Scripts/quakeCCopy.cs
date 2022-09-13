using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quakeCCopy : MonoBehaviour
{

    

    [SerializeField, Range(0f, 100f)]
    public float p_maxSpeed = 1f;

    //[SerializeField, Range(0f, 100f)] float maxAcceleration = 10f;


    //jump
    [Range(1f, 20f)] public float p_jumpHeight = 1f;
    private bool desiredJump;
    const string j = "Jump";
    
    //wishdir
    Vector3 wishDir;
    Vector3 forwardDir;

    [SerializeField]
    float wDir_forwardSpeed = 2f;

    [SerializeField]
    float wDir_SideSpeed = 1.5f;

    //rigidbody
    Rigidbody p_rigidbody;
    Vector3 p_velocity;


    //camera 
    Camera playerCamera;
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
    //[SerializeField] float camOffsetY = 0.8f;
    Vector3 camOffset;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";
    Vector2 rotation = Vector2.zero;
    [SerializeField, Range(0.1f, 10f)]
    float camSensitivity = 2f;


    private void Awake()
    {
        p_rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        desiredJump |= Input.GetKeyDown(KeyCode.Space);
        p_rotate();
        p_velocity = p_rigidbody.velocity;
        updatePlayer();
        forwardDir = transform.forward;
    }

    private void FixedUpdate()
    {



        //Vector3 velAdd = p_velocity - p_rigidbody.velocity;
        //p_rigidbody.velocity += velAdd;

        p_accelerate(wishDir);
        p_rigidbody.velocity =  p_velocity;

    }


    void updatePlayer()
    {
        
        wishDir = getDir(wDir_forwardSpeed, wDir_SideSpeed);
        inputChecks();

        //p_velocity = p_rigidbody.velocity;
        //p_rotate();
        


        
    }
    

    void p_accelerate(Vector3 wishVel)
    {
        float currentSpeed, addSpeed;


        
        float wishSpeed = wishVel.magnitude;
        
        //if (wishSpeed == 0) return; not needed cause physics still applys 

        currentSpeed = Vector3.Dot(p_velocity, wishVel);
        float speedMag = wishSpeed - currentSpeed;

        //Debug.Log("Speed: " + currentSpeed + " speedMag :" + speedMag);

        
        addSpeed = p_maxSpeed * wishSpeed;

        if (addSpeed < speedMag) addSpeed = speedMag;

        //float xSpeed = wishDir.x * addSpeed;
        //float ySpeed = wishDir.z * addSpeed;
        //p_velocity +=  (transform.forward * ySpeed + transform.right * xSpeed);
        p_velocity += ((transform.forward * wishDir.z) + (transform.right * wishDir.x)) * addSpeed * Time.deltaTime;

        

        
        

    }

    void p_rotate()
    {
        rotation.x += Input.GetAxis(xAxis) * camSensitivity;
        rotation.y += Input.GetAxis(yAxis) * camSensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);


        Quaternion rotQuat = xQuat * yQuat;

        transform.rotation = xQuat;
        playerCamera.transform.localRotation = rotQuat;
    }

    void inputChecks()
    {
        
        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }
    }


    void Jump()
    {
        //Debug.Log("jump");
        p_velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * p_jumpHeight);
    }


    Vector3 getDir(float xScale, float yScale)
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float yDIr = Input.GetAxisRaw("Vertical");
        //Vector3 wDir = new Vector3(xDir * xScale, 0, yDIr * yScale);
        Vector3 wDir = new Vector3(xDir * xScale, 0, yDIr * yScale);
        return wDir.normalized;
    }




}
