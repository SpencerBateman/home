using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.VRModuleManagement;

public class Seeker : MonoBehaviour, IColliderEventHoverEnterHandler
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

    private bool moving;
    private Vector3 last;
    public Vector3 controllerVelocity;
    public Vector3 desired;
    float adjustTime = 0.5f;
    float currentTime = 0;

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
            if(diff.magnitude > 0.025f)
            {
                desired = (target.position - transform.position).normalized * maxspeed;
                desired -= rb.velocity;
                desired = Vector3.ClampMagnitude(desired, maxforce);

                if(currentTime < adjustTime)
                {
                    rb.velocity = Vector3.Lerp(controllerVelocity, desired, currentTime / adjustTime);
                    currentTime += Time.deltaTime;
                } else
                {
                    rb.velocity = desired;
                }

                last = desired;


                body.LookAt(manager.transform.position, body.up);
                body.Rotate(Vector3.right * 90f);
                velocity = diff.magnitude;

            } else
            {
                target = null;
                moving = false;
                controllerVelocity = Vector3.zero;
                last = Vector3.zero;
                currentTime = 0;
                rb.Sleep();
                if (path.PathComplete)
                {
                    Debug.Log("We Did It!");
                }
            }
        } else if(path && poke && !moving)
        {
            poke = false;
            currentTime = 1;
            moving = true;
            target = path.PokeSheep(sheepIndex);
        }

        rb.AddForce((manager.transform.position - transform.position).normalized * gravity);
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

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        if(!moving)
        {
            moving = true;
            target = path.PokeSheep(sheepIndex);
        }

        currentTime = 0;
        ViveRoleProperty hr = eventData.eventCaster.gameObject.GetComponent<ViveColliderEventCaster>().viveRole;
        ViveInput.TriggerHapticPulse(hr, 1500);
        controllerVelocity = VRModule.GetDeviceState(hr.GetDeviceIndex()).velocity.normalized * maxspeed;
    }
}