using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField]
    private int Score = 100;
    private int LifeTime = 20;

	// Use this for initialization
	void Start ()
    {
        //display objective type and description on screen
        //InvokeRepeating("LifeUpdate", 3, 1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //subtract a second off of lifetime every second
	}

    //private void LifeUpdate()
    //{
    //    --LifeTime;
    //    if (LifeTime <= 0)
    //    {
    //        ObjectiveFinshed();
    //    }
    //}

    public void Complete()
    {
        //send complete event with player that completed it to objective manager
    }
}
