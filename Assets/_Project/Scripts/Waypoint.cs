using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = transform.localPosition.normalized * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
