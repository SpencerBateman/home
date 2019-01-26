using UnityEngine;
using System.Collections;

public class FlockManager : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public int flockSize = 20;
    public GameObject prefab;
    //public GameObject chasee;

    public Vector3 flockCenter;
    public Vector3 flockVelocity;

    private Walker[] walkers;

    void Awake()
    {
        walkers = new Walker[flockSize];
        for (var i = 0; i < flockSize; i++)
        {
            Vector3 position = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1).normalized * 0.01f;

            Walker w = Instantiate(prefab, transform.position, transform.rotation).GetComponent<Walker>();
            w.transform.parent = transform;
            w.transform.localPosition = position;
            w.GetComponent<Walker>().SetManager(this);
            walkers[i] = w;
        }
    }

    void Update()
    {
        Vector3 theCenter = Vector3.zero;
        Vector3 theVelocity = Vector3.zero;

        foreach (Walker w in walkers)
        {
            theCenter = theCenter + w.transform.localPosition;
            theVelocity = theVelocity + w.GetComponent<Rigidbody>().velocity;
        }

        flockCenter = theCenter / (flockSize);
        flockVelocity = theVelocity / (flockSize);
    }
}
   