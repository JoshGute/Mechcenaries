﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    //frequently referenced components
    private Rigidbody Rb;
    private Transform Tr;

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
    private float maxSpeed = 25f;
    [SerializeField]
    private float Speed = 100f;

    //Dash stuff
    [SerializeField]
    private float DashDistance = 50f;
    [SerializeField]
    private float DashSpeed = 20f;

    //Used to store an instance of the dash position to move towards
    private Vector3 curDashTargetPos;

    //GamePad
    public PlayerIndex playerIndex;
    private GamePadState state;
    private GamePadState prevState;

    //Movement Axis
    private float LeftAxisH;
    private float LeftAxisV;

    private float RightAxisH;
    private float RightAxisV;

    //Utility Booleans
    public bool bDisabled = false;
    private bool bDashing = false;



    // Use this for initialization
    void Start ()
    {
        Rb = GetComponent<Rigidbody>();
        Tr = GetComponent<Transform>();
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

            if (prevState.ThumbSticks.Right.X == 0 && prevState.ThumbSticks.Right.Y == 0)
            {
                if (RightAxisH != 0 || RightAxisV != 0)
                {
                    Dash(RightAxisH, RightAxisV);
                }
            }

            if(state.Triggers.Left > 0.1f)
            {
                Brake();
            }

            if (bDashing == true)
            {
                Tr.position = Vector3.MoveTowards(Tr.position, curDashTargetPos, DashSpeed);

                if (Tr.position == curDashTargetPos)
                {
                    bDashing = false;
                    curDashTargetPos = Vector3.zero;
                }
            }
        }
    }

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

    void Dash(float INfAxisH, float INfAxisV)
    {
        Rb.velocity = Vector3.zero;

        Vector3 NormalizedAngle = Vector3.Normalize(new Vector3(RightAxisH, RightAxisV, 0));
        Vector3 InverseNorm = -NormalizedAngle;

        RaycastHit DashHit;
        if (Physics.Raycast(transform.position, NormalizedAngle, out DashHit, DashDistance))
        {
            curDashTargetPos = DashHit.point + (InverseNorm * 2);
            bDashing = true;
        }
        else
        {
            Vector3 FinalDestination = new Vector3(Tr.position.x + (NormalizedAngle.x * (DashDistance)),
                                               Tr.position.y + (NormalizedAngle.y * (DashDistance)), 0);
            curDashTargetPos = FinalDestination;
            bDashing = true;
        }
    }

    void Shoot()
    {

    }

    public void Brake()
    {
        Rb.velocity = Vector3.zero;
        Rb.useGravity = false;
    }
}
