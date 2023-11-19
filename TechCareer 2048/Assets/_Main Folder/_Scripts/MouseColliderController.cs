using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseColliderController : MonoBehaviour
{

    [SerializeField] Collider ignoreCollider;
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<SphereCollider>(), ignoreCollider, true);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "slot_cube" && MouseActionsManager.instance.isHoldingCube)
        {
            col.transform.GetChild(11).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "slot_cube")
        {
            col.transform.GetChild(11).gameObject.SetActive(false);
        }
    }
}
