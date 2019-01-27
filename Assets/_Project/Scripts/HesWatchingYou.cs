using UnityEngine;

public class HesWatchingYou : MonoBehaviour
{
    public Transform cam;

    private Quaternion start;

    public bool isIdle = true;
    private bool isWatching;

    public Vector3 local;

    private void Start()
    {
        start = transform.localRotation;
        Invoke("Watch", Random.Range(5, 15));
    }

    void Watch()
    {
        if(isIdle)
        {
            isWatching = !isWatching;
            Invoke("Watch", Random.Range(5, 15));
        } else
        {
            isWatching = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation;
        if(isWatching)
        {
            if (!isIdle) isWatching = false;
            Vector3 relativePos = cam.position - transform.position;
            rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            if(transform.localEulerAngles.x > 180/* || transform.localEulerAngles.y > 180 || transform.localEulerAngles.z > 180*/)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, start, Time.deltaTime * 3);
            } else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);
            }

            local = transform.localEulerAngles;
        } else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, start, Time.deltaTime * 3);
        }
    }
}
