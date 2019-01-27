using UnityEngine;

public class Node : MonoBehaviour {
    public bool blocked;

    void Awake()
    {
        transform.localPosition = transform.localPosition.normalized * 0.2f;
    }

    public void UnBlock() {
        blocked = false;
    }

    public bool IsBlocked() {
        return blocked;
    }
}
