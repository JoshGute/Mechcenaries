using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DroneStates { Move, Follow, Stay, Shoot, Die};

public class DroneAI : MonoBehaviour {

  [SerializeField]
  private Vector3 MoveDirection;

  [SerializeField]
  private float moveSpeed;

  DroneStates curState = DroneStates.Move;

	// Use this for initialization
	void Start ()
  {
	}

	// Update is called once per frame
	void Update ()
  {
    if (curState == DroneStates.Move)
    {
      gameObject.transform.position = MoveDirection += new Vector3(1 * moveSpeed, 0, 0);

    }
	}
}
