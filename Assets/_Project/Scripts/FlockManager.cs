using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;

    public GameObject walkerPrefab;
    public GameObject seekerPrefab;
    public Transform waypoint;

    public int flockSize = 20;
    public float separation = 0.1f;
    public float align = 0.2f;
    public float cohesion = 0.2f;
    public float maxspeed = 1f;

    public Vector3 flockCenter;
    public Vector3 flockVelocity;

    private Walker[] walkers;

    void Awake()
    {
        Physics.gravity = Vector3.zero;

        walkers = new Walker[0];
        /*walkers = new Walker[flockSize];
        for (var i = 0; i < flockSize; i++)
        {
            Vector3 position = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1).normalized * 0.01f;

            Walker w = Instantiate(walkerPrefab, transform).GetComponent<Walker>();
            w.transform.localPosition = position;
            w.SetManager(this);
            walkers[i] = w;
        }*/

        Seeker s = Instantiate(seekerPrefab, transform).GetComponent<Seeker>();
        s.transform.localPosition = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1).normalized * 0.01f;
        s.SetManager(this, waypoint);
    }

    void Update()
    {
        foreach(Walker w in walkers)
        {
            w.AddForce(Separate(w.transform));
            w.AddForce(Align(w.transform));
        }
    }

    Vector3 Separate(Transform w) {
        Vector3 steer = new Vector3();
        int count = 0;

        foreach (Walker o in walkers)
        {
            float dist = Vector3.Distance(w.position, o.transform.position); //make this distance over the surface
            if (dist > 0 && dist < separation)
            {
                Vector3 diff = (w.position - o.transform.position).normalized;
                diff /= dist;
                steer += diff;
                count++;
            }
        }

        if (count > 0) steer /= count;

        if (steer.magnitude > 0)
        {
            steer = steer.normalized - w.GetComponent<Rigidbody>().velocity;
        }

        return steer;
    }

    Vector3 Align(Transform w)
    {
        Vector3 sum = new Vector3();
        int count = 0;

        foreach(Walker o in walkers)
        {
            float dist = Vector3.Distance(w.position, o.transform.position);
            if(dist > 0 && dist < align)
            {
                sum += o.GetComponent<Rigidbody>().velocity;
                count++;
            }
        }

        if(count > 0)
        {
            sum /= count;
            sum = sum.normalized - w.GetComponent<Rigidbody>().velocity;
        }

        return sum;
    }

    Vector3 Cohere(Transform w)
    {
        Vector3 sum = new Vector3();
        int count = 0;

        foreach(Walker o in walkers)
        {
            float dist = Vector3.Distance(w.position, o.transform.position);
            if(dist > 0 && dist < cohesion)
            {
                sum += o.transform.position;
                count++;
            }
        }

        if(count > 0)
        {
            sum /= count;
            sum -= w.position;
            sum = sum.normalized - w.GetComponent<Rigidbody>().velocity;
        }

        return sum;
    }
}
   