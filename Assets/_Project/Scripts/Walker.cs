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
    //private GameObject chasee;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Vector3.zero;
        rb.velocity = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
        rb.AddForce((manager.transform.position - transform.position).normalized * acceleration);
    }

    void FixedUpdate()
    {
        //rb.velocity = rb.velocity + Calc() * Time.deltaTime;
        rb.AddForce((manager.transform.position - transform.position).normalized * acceleration);
    }

    IEnumerator Steering()
    {
        yield return null;
        while (true)
        {
            rb.velocity = rb.velocity + Calc() * Time.deltaTime;

            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetManager(FlockManager flockManager)
    {
        manager = flockManager;
        minVelocity = manager.minVelocity;
        maxVelocity = manager.maxVelocity;
        randomness = manager.randomness;
        StartCoroutine("Steering");
    }

    private Vector3 Calc()
    {
        Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);

        randomize.Normalize();
        Vector3 flockCenter = manager.flockCenter;
        Vector3 flockVelocity = manager.flockVelocity;

        flockCenter = flockCenter - transform.localPosition;
        flockVelocity = flockVelocity - rb.velocity;

        return (flockCenter + flockVelocity + randomize * randomness);
    }
}