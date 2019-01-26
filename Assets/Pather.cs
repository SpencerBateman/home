using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pather : MonoBehaviour {
  public GameObject sheep1;
  public GameObject sheep2;
  private int sheep1Index;
  private int sheep2Index;
  bool PathComplete;

  private List<GameObject> wayPointList;

  // Start is called before the first frame update
  void Start() {
    wayPointList = new List<GameObject>();
    PathComplete = false;
    foreach (GameObject child in transform) {
      wayPointList.Add(child);
    }
  }

  void Update() {
    if (sheepIndex == sheep2Index) {
      PathComplete = true;
    }
    if (!PathComplete) {
      // Do sheep poking and other things here that can only happen when the
      // path has not been completed.
    }
  }

  public void pokeSheep(int sheepNumber) {
    if (sheepNumber == 1) {
      if (!nextNodeBlocked(sheep1Index, 1)) {
        sheep1Index += 1;
      }
    } else if (sheepNumber == 2) {
      if (!nextNodeBlocked(sheep2Index, 2)) {
        sheep1Index -= 1;
      }
    }
  }

  bool nextNodeBlocked(int sheepIndex, int sheepNumber) {
    if (sheepNumber == 1) {
      return wayPointList[sheep1Index + 1].GetComponent<Node>().isBlocked();
    } else if (sheepNumber == 2) {
      return wayPointList[sheep1Index - 1].GetComponent<Node>().isBlocked();
    } else {
      throw new Exception();
    }
  }
}
