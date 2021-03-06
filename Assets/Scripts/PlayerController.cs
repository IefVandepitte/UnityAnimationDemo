using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //gravity
    public float gravity = 9.87f;
    private float verticalSpeed = 0f;
    //movement
    public CharacterController characterController;
    public RuntimeAnimatorController runtimeAnimatorController;
    public float speed = 6f;
    //camera and rotation
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50f;
    public float downLimit = 50f;


    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        KeyBoardRotate();
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }
    
    private void KeyBoardRotate()
    {
        float keyBoardRotationHorizontal = 0f;
        float keyBoardRotationVertical = 0f;


        bool up = Input.GetKey(KeyCode.F);
        bool down = Input.GetKey(KeyCode.V);
        bool left = Input.GetKey(KeyCode.C);
        bool right = Input.GetKey(KeyCode.B);

        float step = 0.1f;

        if (left) keyBoardRotationHorizontal += step;
        if (right) keyBoardRotationHorizontal -= step;
        float horizontalRotation = keyBoardRotationHorizontal;

        if (up) keyBoardRotationVertical += step;
        if (down) keyBoardRotationVertical -= step;
        float verticalRotation = keyBoardRotationVertical;



        transform.Rotate(0, -horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (characterController.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        animator.runtimeAnimatorController = runtimeAnimatorController;
        animator.SetBool("isWalking", verticalMove != 0 || horizontalMove != 0);
    }
}
