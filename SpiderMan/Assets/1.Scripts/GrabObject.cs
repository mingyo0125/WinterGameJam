using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    [Header("PickUp Settings")]
    [SerializeField] Transform holdArea;
    [SerializeField] LayerMask layermask;
    Rigidbody holdObjrb;
    GameObject holdObj;

    [Header("Physics Parameters")]
    [SerializeField] float pickRange = 5.0f;
    [SerializeField] float pickupForce = 150.0f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (holdObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickRange, layermask))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        return;
                    }
                    else
                    {
                        GrabObj(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                DropObj();
            }
        }
        if (holdObj != null)
        {
            MoveObj();
        }
    }

    private void GrabObj(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            OutlineObj.outLine();
            
            holdObjrb = pickObj.GetComponent<Rigidbody>();
            holdObjrb.useGravity = false;
            holdObjrb.drag = 10;
            holdObjrb.constraints = RigidbodyConstraints.FreezeRotation;



            //holdObjrb.transform.parent = holdArea;
            holdObj = pickObj;
        }
    }

    private void DropObj()
    {
        holdObjrb.useGravity = true;
        holdObjrb.drag = 1;
        holdObjrb.constraints = RigidbodyConstraints.None;

        holdObjrb.transform.parent = null;
        holdObj = null;
        OutlineObj.outLine();
    }

    public void MoveObj()
    {
        if (Vector3.Distance(holdObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = holdArea.position - holdObj.transform.position;
            holdObjrb.AddForce(moveDirection * pickupForce);
        }

    }
}
