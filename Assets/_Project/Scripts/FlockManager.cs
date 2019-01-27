using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public bool hasFlock;

    public GameObject walkerPrefab;
    public GameObject seekerPrefab;
    public Pather path;

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
        if(hasFlock)
        {
            walkers = new Walker[flockSize];
            for (var i = 0; i < flockSize; i++)
            {
                Vector3 position = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1).normalized * 0.01f;

                Walker w = Instantiate(walkerPrefab, transform).GetComponent<Walker>();
                w.transform.localPosition = position;
                w.SetManager(this);
                walkers[i] = w;
            }
        }
        else
        {
            walkers = new Walker[0];
        }
    }

    private void Start()
    {
        Seeker s = Instantiate(seekerPrefab, transform).GetComponent<Seeker>();
        path.SetupSheep(s);
        s.SetManager(this);

        Seeker s2 = Instantiate(seekerPrefab, transform).GetComponent<Seeker>();
        path.SetupSheep(s2);
        s2.SetManager(this);

        path.SetupNodes(GetComponent<SphereCollider>().radius);
    }

    void Update()
    {
        foreach(Walker w in walkers)
        {
            w.AddForce(Separate(w.transform));
            w.AddForce(Align(w.transform));
            //w.AddForce(Cohere(w.transform));
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
   