using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.VRModuleManagement;

public class FallingTree : MonoBehaviour, IColliderEventHoverEnterHandler
{
    public Transform removeBlock;
    public Node node;

    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartAnim()
    {
        node.UnBlock();
        GetComponent<Collider>().isTrigger = true;
        removeBlock.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("startFall");
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        if(!activated)
        {
            activated = true;
            StartAnim();
            ViveRoleProperty hr = eventData.eventCaster.gameObject.GetComponent<ViveColliderEventCaster>().viveRole;
            ViveInput.TriggerHapticPulse(hr, 1500);
        }
    }
}
