using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
  public bool Blocked;

  // Update is called once per frame
  void Update() {
  }

  public void unBlock() {
    Blocked = false;
  }

  public bool isBlocked() {
    return Blocked;
  }
}
