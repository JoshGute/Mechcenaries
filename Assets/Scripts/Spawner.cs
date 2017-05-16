using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

  [SerializeField]
  private GameObject SpawnObject;

  //Spawn the prefab every x seconds
  [SerializeField]
  private float iSpawnEveryXSec;

  private float icurSec;

  //Start spawning once scene is opened?
  [SerializeField]
  private bool StartSpawnOnCreate;

  //Whether we are spawning or not.
  private bool isSpawning;

	// Use this for initialization
	void Start ()
  {
    icurSec = 0;

    if(StartSpawnOnCreate == true)
    {
      StartSpawn();
    }
	}
	
  //Start the spawn loop
  void StartSpawn()
  {
    isSpawning = true;
  }

  //Stop the spawn loop, reset the curSec to 0.
  void StopSpawn()
  {
    icurSec = 0;
    isSpawning = false;
  }

  //Spawns the prefab at position
  public void SpawnPrefab()
  {
    Instantiate(SpawnObject, gameObject.transform);
  }

	// Update is called once per frame
	void Update ()
  {
		if(isSpawning == true)
    {
      icurSec += Time.deltaTime;

      if(icurSec >= iSpawnEveryXSec)
      {
        SpawnPrefab();
        icurSec = 0;
      }

    }
	}
}
