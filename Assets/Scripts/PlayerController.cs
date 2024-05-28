using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movePower = 5f;
    [SerializeField] private float rotationPower = 2f;
    private CharacterController controller;
    private Vector3 moveVector;
    private float gravity; 
    private bool isGrounded;   
    private float xRotation = 0;
    private new Camera camera;
    private Glasses glasses;
    private bool glassesOn = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();
        glasses = GetComponentInChildren<Glasses>(); 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            //glassesOn = !glassesOn ? (glasses.PutOnGlasses(), true) : (glasses.TakeOffGlasses(), false);
            if (!glassesOn)
            {
                glasses.PutOnGlasses();
                glassesOn = true;
            }
            else
            {
                glasses.TakeOffGlasses();
                glassesOn = false;
            }   
        }
        

        isGrounded = controller.isGrounded;
        if (!isGrounded)
        {
            gravity -= 9f * Time.deltaTime;
        }
        else 
        {
            gravity = 0;
        }


        moveVector = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));
        moveVector = transform.TransformDirection(moveVector);
        
        controller.Move(moveVector * movePower * Time.deltaTime);


        float mouseX = Input.GetAxis("Mouse X") * rotationPower * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationPower * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);    
    }
}