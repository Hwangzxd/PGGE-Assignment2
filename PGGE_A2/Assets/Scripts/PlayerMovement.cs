using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    public float mWalkSpeed = 1.5f;
    public float mRotationSpeed = 50.0f;
    public bool mFollowCameraForward = false;
    public float mTurnRate = 10.0f;

#if UNITY_ANDROID
    public FixedJoystick mJoystick;
#endif

    private float hInput;
    private float vInput;
    private float speed;
    private bool jump = false;
    private bool crouch = false;
    private bool reload = false;
    public float mGravity = -30.0f;
    public float mJumpHeight = 1.0f;

    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //HandleInputs();
        //Move();
    }

    private void FixedUpdate()
    {
        //ApplyGravity();
    }

    public void HandleInputs()
    {
        // We shall handle our inputs here.
#if UNITY_STANDALONE
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
#endif

#if UNITY_ANDROID
        hInput = 2.0f * mJoystick.Horizontal;
        vInput = 2.0f * mJoystick.Vertical;
#endif

        speed = mWalkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mWalkSpeed * 2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            crouch = !crouch;
            Crouch();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reload = !reload;
            Reload();
        }

    }

    public void Move()
    {
        //if (crouch) return;

        //// We shall apply movement to the game object here.
        //if (mAnimator == null) return;

        if (crouch || mAnimator == null)
        {
            return;
        }

        //if (mFollowCameraForward)
        //{
        //    // rotate Player towards the camera forward.
        //    Vector3 eu = Camera.main.transform.rotation.eulerAngles;
        //    transform.rotation = Quaternion.RotateTowards(
        //        transform.rotation,
        //        Quaternion.Euler(0.0f, eu.y, 0.0f),
        //        mTurnRate * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
        //}

        RotatePlayer();

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);
        mAnimator.SetFloat("PosX", 0, 0.2f, Time.deltaTime);
        mAnimator.SetFloat("PosZ", vInput * speed / (2.0f * mWalkSpeed), 0.2f, Time.deltaTime);

        if (jump)
        {
            Jump();
            jump = false;
        }
        ApplyGravity();
    }

    private void RotatePlayer()
    {
        if (mFollowCameraForward)
        {
            //// rotate Player towards the camera forward.
            //Vector3 eu = Camera.main.transform.rotation.eulerAngles;
            //transform.rotation = Quaternion.RotateTowards(
            //    transform.rotation,
            //    Quaternion.Euler(0.0f, eu.y, 0.0f),
            //    mTurnRate * Time.deltaTime);

            RotatePlayerTowardsCamera();
        }
        else
        {
            transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
        }
    }

    private void RotatePlayerTowardsCamera()
    {
        // rotate Player towards the camera forward.
        Vector3 eu = Camera.main.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0.0f, eu.y, 0.0f),
            mTurnRate * Time.deltaTime);
    }

    void Jump()
    {
        //mAnimator.SetTrigger("Jump");
        TriggerAnimator("Jump");
        mVelocity.y += Mathf.Sqrt(mJumpHeight * -2f * mGravity);
    }

    private Vector3 HalfHeight;
    private Vector3 tempHeight;
    void Crouch()
    {
        mAnimator.SetBool("Crouch", crouch);
        if (crouch)
        {
            tempHeight = CameraConstants.CameraPositionOffset;
            HalfHeight = tempHeight;
            HalfHeight.y *= 0.5f;
            CameraConstants.CameraPositionOffset = HalfHeight;
        }
        else
        {
            CameraConstants.CameraPositionOffset = tempHeight;
        }
    }

    void Reload()
    {
        //mAnimator.SetTrigger("Reload");
        TriggerAnimator("Reload");
    }

    void TriggerAnimator(string triggerName)
    {
        mAnimator.SetTrigger(triggerName);
    }

    void ApplyGravity()
    {
        // apply gravity.
        mVelocity.x = 0.0f;
        mVelocity.z = 0.0f;

        mVelocity.y += mGravity * Time.deltaTime;
        mCharacterController.Move(mVelocity * Time.deltaTime);
        if (mCharacterController.isGrounded && mVelocity.y < 0)
            mVelocity.y = 0f;
    }
}
