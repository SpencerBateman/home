using System.Collections;
using UnityEngine;

public class Walker : MonoBehaviour
{
    private Rigidbody rb;
    private float acceleration = 9.8f;
    private FlockManager manager;

    private bool inited = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;

    private Vector3 tempForce;
    private Transform visibleSheep;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        visibleSheep = transform.GetChild(0);

        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
        rb.AddForce((manager.transform.position - transform.position).normalized * acceleration);
    }

    void FixedUpdate()
    {
        //have to limit tempForce
        rb.AddForce(tempForce);
        rb.AddForce((manager.transform.position - transform.position).normalized * acceleration);
        tempForce = Vector3.zero;

        visibleSheep.LookAt(manager.transform.position, visibleSheep.up);
        visibleSheep.Rotate(Vector3.right * 90f);
    }

    public void SetManager(FlockManager flockManager)
    {
        manager = flockManager;
    }

    public void AddForce(Vector3 f)
    {
        tempForce += f;
    }
}