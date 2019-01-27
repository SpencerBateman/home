using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClouds : MonoBehaviour {

  // The list of Orbiting Objects to Spawn
  public GameObject[] OrbitingObjects;

  public float mag;
  public float distance;


  // Initialize and spawn
  void Start() {
    foreach (GameObject g in OrbitingObjects) {
      GameObject childObject = Instantiate(g) as GameObject;
      childObject.transform.parent = gameObject.transform;

      childObject.transform.position = new Vector3((Random.Range(0,2)*2-1) * (Random.Range(0, mag) + distance), (Random.Range(0,2)*2-1) * (Random.Range(0, mag) +distance), (Random.Range(0,2)*2-1) * (Random.Range(0, mag) +distance));

    }
  }
}
