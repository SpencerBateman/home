using UnityEngine;

public class Seeker : MonoBehaviour
{
    public float maxspeed = 0.02f;
    public float maxforce = 0.05f;

    private Rigidbody rb;
    private float gravity = 9.8f;
    private FlockManager manager;

    private Vector3 tempForce;
    private Transform body;
    public Transform target;
    private Pather path;
    private int sheepIndex;

    public float velocity;

    public bool poke;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        body = transform.GetChild(0);

        rb = GetComponent<Rigidbody>();
        body.LookAt(manager.transform.position, body.up);
        body.Rotate(Vector3.right * 90f);
        rb.Sleep();
    }

    void FixedUpdate()
    {

        if (target)
        {
            Vector3 diff = target.position - transform.position;
            if(diff.magnitude > 0.01f)
            {
                rb.position += Steer() * Time.deltaTime;
                body.LookAt(manager.transform.position, body.up);
                body.Rotate(Vector3.right * 90f);

            } else
            {
                target = null;
                rb.Sleep();
                if (path.PathComplete)
                {
                    Debug.Log("We Did It!");
                }
            }
        } else if(path && poke)
        {
            poke = false;
            target = path.PokeSheep(sheepIndex);
        } else
        {
        }

        rb.AddForce((manager.transform.position - transform.position).normalized * gravity);

        velocity = rb.velocity.magnitude;
    }

    private Vector3 Steer()
    {
        Vector3 desired = (target.position - transform.position).normalized * maxspeed;
        desired -= rb.velocity;
        desired = Vector3.ClampMagnitude(desired, maxforce);

        return desired;
    }

    public void SetManager(FlockManager flockManager)
    {
        manager = flockManager;
        transform.LookAt(manager.transform.position);
        transform.Rotate(Vector3.right * 90f);
    }

    public void SetPath(Pather p, int i)
    {
        path = p;
        sheepIndex = i;
    }
}