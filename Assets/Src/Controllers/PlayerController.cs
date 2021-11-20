using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip[] walkAudioClips;
    [SerializeField] private AudioClip[] breathingAudioClips;
    public float healthPoints;
    public float speed;
    public float walkStepSpeed;
    public float jumpSpeed;
    public float gravity;
    public Camera playerCamera;
    public float lookSpeed;
    public float lookXLimit;
    public float lookDistance;
    private AudioUtil audioUtil;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 rotation = Vector2.zero;
    private float moveSpeed;
    private bool canMove = true;
    private bool takingStep = false;
    private bool _isIdle = true;
    private bool leverUsed = false;
    private MHandController handController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioUtil = GetComponent<AudioUtil>();
        handController = GetComponentInChildren<MHandController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        DoBreathingSound();
        DoMovement();
    }


    private void DoBreathingSound()
    {
        if (!audioUtil.isPlaying)
        {
            AudioClip clip  = GetRandomClip(breathingAudioClips);
            audioUtil.Play(clip);
        }

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
                moveDirection.y = jumpSpeed;
            }
            else if(moveSpeed != speed)
            {
                moveSpeed = speed;
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
        _isIdle = true;
    }


    public bool isIdle => _isIdle;


    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        int size = clips.Length;
        int random_index = UnityEngine.Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }


    ///<summary>Perform walk.</summary>
    public void Walk()
    {
        _isIdle = false;
        if (!takingStep)
        {
            AudioClip clip = GetRandomClip(walkAudioClips);
            audioUtil.PlayOneShot(clip);
            takingStep = true;
            Action releaseStep = ()=>{
                takingStep = false;
            };
            StartCoroutine(Wait(walkStepSpeed, releaseStep));
        }
        //animator.Walk();
    }


    public void Hit()
    {
        healthPoints --;
        if (healthPoints <= 0)
        {
            //TODO Loser scene
        }
    }


    public bool HasAllPowercells()
    {
        int currentTotal = handController.GetPowerCellCount();
        int maxCount = handController.GetMaxPowerCells();
        bool hasAll = false;
        if (currentTotal >= maxCount)
        {
            hasAll = true;
        }
        return hasAll;
    }


    public void UseLever()
    {
        leverUsed = true;
    }


    public bool UsedLever()
    {
        return leverUsed;
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
