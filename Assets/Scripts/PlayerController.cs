using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    //frequently referenced components
    private Rigidbody Rb;
    private Transform Tr;
    private GameObject gGun;

    //Heat generattion and management
    [SerializeField]
    private float fMaxHeat = 200;
    private float fCoolDown;
    private float fCurrentHeat = 0;

    //Turning
    [SerializeField]
    private float TurnSpeed = 5f;

    //Moving
    [SerializeField]
    private float maxSpeed = 50f;
    [SerializeField]
    private float Speed = 500f;

    //Used to store an instance of the dash position to move towards
    private Vector3 curDashTargetPos;

    //GamePad
    public PlayerIndex playerIndex;
    private GamePadState state;
    private GamePadState prevState;

    //Movement Axis
    private float LeftAxisH;
    private float LeftAxisV;

    //Rotation Axis (robot mode only!)
    private float RightAxisH;
    private float RightAxisV;

    //Gameplay Booleans
    public bool bDisabled = false;
    private bool bDashing = false;
    private bool bRobotMode = false;



    // Use this for initialization
    void Start ()
    {
        Rb = GetComponent<Rigidbody>();
        Tr = GetComponent<Transform>();
        gGun = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bDisabled)
        {
            prevState = state;
            state = GamePad.GetState(playerIndex);

            LeftAxisH = state.ThumbSticks.Left.X;
            LeftAxisV = state.ThumbSticks.Left.Y;

            RightAxisH = state.ThumbSticks.Right.X;
            RightAxisV = state.ThumbSticks.Right.Y;

            if (LeftAxisH != 0 || LeftAxisV != 0)
            {
                float LookDirection = Mathf.Atan2(LeftAxisV, LeftAxisH);
                Tr.rotation = Quaternion.Euler(0f, 0f, LookDirection * Mathf.Rad2Deg);
            }

            if (bRobotMode)
            {
                if (RightAxisH != 0 || RightAxisV != 0)
                {
                    float LookDirection = Mathf.Atan2(RightAxisV, RightAxisH);
                    gGun.transform.rotation = Quaternion.Euler(0f, 0f, LookDirection * Mathf.Rad2Deg);
                }
            }

            if(prevState.Triggers.Left < 0.1f && state.Triggers.Left > 0.1f)
            {
                TransformRobot();
            }
        }
    }

    //physics
    void FixedUpdate()
    {
        if (!bDisabled && LeftAxisH != 0 || LeftAxisV != 0)
        {
            Rb.AddRelativeForce(new Vector3(1,0,0) * Speed);
        }

        if (!bDisabled && Rb.velocity.magnitude > maxSpeed)
        {
            Rb.velocity = Rb.velocity.normalized * maxSpeed;
        }
     }

    void Shoot()
    {

    }

    private void TransformRobot()
    {
        Rb.velocity = Vector3.zero;
        //if in robot mode switch to plane
        if (bRobotMode)
        {
            gGun.transform.localRotation = Quaternion.identity;//Quaternion.Euler(Vector3.zero);
            bRobotMode = false;
            maxSpeed = 50.0f;
            Rb.useGravity = true;
        }

        //vice versa
        else
        {
            bRobotMode = true;
            maxSpeed = 30.0f;
            Rb.useGravity = false;
        }
    }
}
