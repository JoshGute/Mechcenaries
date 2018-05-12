using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int timeLimit;
    public GameObject[] mechs;
    public GameObject[] spawnPoints;
    //private PlayerHolder playerholder;


    // Use this for initialization
    void Start()
    {
        //FindSpawnPoints();
        //SpawnPlayers(SpawnPointArray);
    }

    private GameObject[] FindSpawnPoints()
    {
        //find all the spawnpoints and stuff them in an array for the spawner
        return null;
    }

    private void SpawnPlayers(GameObject[] spawnPoints_)
    {
        //loop through the spawnpoints and spawn the players at different points
    }

	// Update is called once per frame
	void Update ()
    {
		//I'm sure something will go here
	}

    private void SpawnObjective()
    {
        //spawns prefab objective at one of the spawnpoints
    }

    private void UpdateScore()
    {
        //takes event from the score updating
        //checks scores to see if someone won and calls winner
        //winner(player that won);
    }

    private void Winner()
    {
        //takes the player that won and stores them in an object to take to the win screen
    }
}
