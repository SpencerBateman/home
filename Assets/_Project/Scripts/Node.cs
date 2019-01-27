using UnityEngine;

public class Node : MonoBehaviour {
    public bool blocked;

    public void AdjustPosition(float scale)
    {
        transform.localPosition = transform.localPosition.normalized * scale;
    }

    public void UnBlock() {
        blocked = false;
    }

    public bool IsBlocked() {
        return blocked;
    }
}
