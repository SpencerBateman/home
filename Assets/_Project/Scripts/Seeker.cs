using System.Collections;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    private Rigidbody rb;
    private float acceleration = 9.8f;
    private FlockManager manager;

    private bool inited = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;

    private Vector3 tempForce;
    private Transform body;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        body = transform.GetChild(0);

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if (target)
        {
            Vector3 diff = target.position - transform.position;
            if(diff.magnitude > 0.1f)
            {
                rb.AddForce((diff.normalized - rb.velocity) * 0.2f);

            } else if(diff.magnitude < 0.04f)
            {
                Debug.Log("here");
            }
        }

        if(rb.velocity.magnitude > 0.0005f)
        {
            body.LookAt(manager.transform.position, body.up);
            body.Rotate(Vector3.right * 90f);
        }

        rb.AddForce((manager.transform.position - transform.position).normalized * acceleration);
    }

    public void SetManager(FlockManager flockManager, Transform waypoint)
    {
        manager = flockManager;
        transform.LookAt(manager.transform.position);
        transform.Rotate(Vector3.right * 90f);

        target = waypoint;
    }
}