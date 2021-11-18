using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class tPlayerController : MonoBehaviour
{
    public float healthPoints;
    public float speed;
    public float runSpeed;
    public float jumpSpeed;
    public float gravity;
    public Camera playerCamera;
    public float lookSpeed;
    public float lookXLimit;
    public float lookDistance;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 rotation = Vector2.zero;
    private float moveSpeed;
    private bool canMove = true;
    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }


    ///<summary>Do charachter movement.</summary>
    public void DoMovement()
    {
         if (characterController.isGrounded && canMove)
        {
            //Recalculate direction
            try
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedX = canMove ? moveSpeed * Input.GetAxis("Vertical") : 0;
                float curSpeedY = canMove ? moveSpeed * Input.GetAxis("Horizontal") : 0;
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }


            if (Input.GetButtonDown("Jump") && canMove)
            {
                //Jump();
                Debug.Log("Jump");
                moveDirection.y = jumpSpeed;
            }
            else if(moveSpeed != speed)
            {
                moveSpeed = speed;
                isRunning = false;
            }
        }


        //Apply gravity. Gravity is multiplied by deltaTime twice.
        moveDirection.y -= gravity * Time.deltaTime;



        if ( Input.GetButton("Vertical") && canMove || Input.GetButton("Horizontal") && canMove)
        {
            Walk();
        }
        else
        {
            Idle();
        }



        //Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

        //Player and Camera rotation
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);
    }


    ///<summary>Perfom idle.</summary>
    public void Idle()
    {
        Debug.Log("Idle");
    }


    ///<summary>Perform walk.</summary>
    public void Walk()
    {
        //animator.Walk();
        Debug.Log("Animator.walk");
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
