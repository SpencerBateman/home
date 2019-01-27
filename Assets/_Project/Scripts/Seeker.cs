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
    private float adjustTime = 0.5f;
    private float currentTime = 0;
    private Animator anim;
    private Quaternion savedRotation;

    void Start()
    {
        anim = transform.Find("Body/mdl_sheep").GetComponent<Animator>();
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
            if(diff.magnitude > 0.031f)
            {
                desired = (target.position - transform.position).normalized * maxspeed;
                desired -= rb.velocity;
                desired = Vector3.ClampMagnitude(desired, maxforce);

                if (currentTime < adjustTime)
                {
                    rb.velocity = Vector3.Lerp(controllerVelocity, desired, currentTime / adjustTime);
                    currentTime += Time.deltaTime;
                }
                else
                {
                    rb.velocity = desired;
                }

                last = desired;

                if(diff.magnitude < 0.032f)
                {
                    anim.SetBool("isWalking", false);
                    body.rotation = savedRotation;

                } else
                {
                    body.LookAt(manager.transform.position, body.up);
                    body.Rotate(Vector3.right * 90f);
                    savedRotation = body.rotation;
                }

                velocity = diff.magnitude;

            } else
            {
                //anim.SetBool("isWalking", false);
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
            StartWalking();
        }

        rb.AddForce((manager.transform.position - transform.position).normalized * gravity);
    }

    public void SetManager(FlockManager flockManager)
    {
        manager = flockManager;
        transform.LookAt(manager.transform.position);
        transform.Rotate(Vector3.right * 90f);
        //transform.Find("Body/mdl_sheep/root/head/head 1").GetComponent<HesWatchingYou>().cam = flockManager.cam;
    }

    public void SetPath(Pather p, int i)
    {
        path = p;
        sheepIndex = i;
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        if(!moving) StartWalking();

        currentTime = 0;
        ViveRoleProperty hr = eventData.eventCaster.gameObject.GetComponent<ViveColliderEventCaster>().viveRole;
        ViveInput.TriggerHapticPulse(hr, 1500);
        controllerVelocity = VRModule.GetDeviceState(hr.GetDeviceIndex()).velocity.normalized * maxspeed;
    }

    private void StartWalking()
    {
        target = path.PokeSheep(sheepIndex);
        if(target != null)
        {
            moving = true;
            anim.SetBool("isWalking", true);
        }
    }
}