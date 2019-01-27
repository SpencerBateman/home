using System.Collections.Generic;
using System;
using UnityEngine;

public class Pather : MonoBehaviour {
    public GameObject sheep1;
    public GameObject sheep2;
    private int sheep1Index;
    private int sheep2Index;
    public bool PathComplete { get; private set; }

    private List<Transform> wayPointList;

    void Awake() {
        wayPointList = new List<Transform>();
        PathComplete = false;
        foreach (Transform child in transform) {
            wayPointList.Add(child);
        }
        sheep1Index = 0;
        sheep2Index = wayPointList.Count - 1;
    }

    public void SetupSheep(Seeker s)
    {
        if(sheep1 == null)
        {
            s.transform.position = wayPointList[sheep1Index].position;
            s.SetPath(this, 1);
            sheep1 = s.gameObject;
        } else
        {
            s.transform.position = wayPointList[sheep2Index].position;
            s.SetPath(this, 2);
            sheep2 = s.gameObject;
        }
    }

    public Transform PokeSheep(int sheepNumber) {
        Transform result = null;
        if (sheepNumber == 1) {
            if (!NextNodeBlocked(sheep1Index, 1)) {
                sheep1Index += 1;
                result = wayPointList[sheep1Index];
            }
        } else if (sheepNumber == 2) {
            if (!NextNodeBlocked(sheep2Index, 2)) {
                sheep2Index -= 1;
                result = wayPointList[sheep2Index];
            }
        }
        if (sheep1Index == sheep2Index) PathComplete = true;
        return result;
    }

    bool NextNodeBlocked(int sheepIndex, int sheepNumber) {
        if (sheepNumber == 1) {
            return wayPointList[sheep1Index + 1].GetComponent<Node>().IsBlocked();
        } else if (sheepNumber == 2) {
            return wayPointList[sheep2Index - 1].GetComponent<Node>().IsBlocked();
        } else {
            throw new Exception();
        }
    }
}
