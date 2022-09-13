using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camLook : MonoBehaviour
{
    public Transform target;
    Camera playerCamera;
    [SerializeField] float camOffsetY = 0.8f;
    Vector3 camOffset;

    //rotation station
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

    Vector2 rotation = Vector2.zero;
    //Strings in direct code generate garbage, storing and re-using them creates no garbage, or so this one guy says
    const string xAxis = "Mouse X"; 
    const string yAxis = "Mouse Y";

    [SerializeField, Range(0.1f, 10f)]
    float sensitivity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera = GetComponent<Camera>();
        camOffset = new Vector3(0, camOffsetY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        

        /*rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        
        Quaternion rotQuat = xQuat * yQuat;
        
        transform.localRotation = rotQuat;*/

        transform.position = target.transform.position + camOffset;
        //Quaternions seem to rotate more consistently than EulerAngles.  
        //Sensitivity seemed to change slightly at certain degrees using Euler.
        //transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
    }


    void FpFollow()
    {
        
        
    }


}
