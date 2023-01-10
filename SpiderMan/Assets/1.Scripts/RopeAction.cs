using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeAction : MonoBehaviour
{
    public Transform player;
    public Transform gunTip;
    public LayerMask layerMask;

    [SerializeField] Image aimImage;

    Camera cam;
    SpringJoint springJoint;
    LineRenderer lineRenderer;
    RaycastHit hit;
    Vector3 spot;


    bool OnGrapping = false;
    [SerializeField]float lerpTime;
    [SerializeField] float dampPower;
    [SerializeField] float springPower;
    
    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            EndShoot();
        }

        if (Input.GetMouseButton(1))
        {
            if(OnGrapping)
            {
                Grapping();
            }
        }
        DrawRope();
    }

    private void Grapping()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, hit.transform.position, lerpTime * Time.deltaTime);
    }

    private void RopeShoot()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, layerMask))
        {
            OnGrapping = true;


            aimImage.gameObject.SetActive(false);

            spot = hit.point;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0,this.transform.position);
            lineRenderer.SetPosition(1,hit.point);

            springJoint = player.gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = spot;

            float dis = Vector3.Distance(this.transform.position, spot);

            springJoint.maxDistance = dis;
            springJoint.minDistance = dis * 0.5f;
            springJoint.damper = dampPower;
            springJoint.spring = springPower;
            springJoint.massScale = 5f;
        }
    }

    private void EndShoot()
    {
        OnGrapping = false;


        aimImage.gameObject.SetActive(true);

        this.transform.rotation = new Quaternion(0,0,0,0);
        if(springJoint != null)
        {
            Destroy(springJoint);
        }
        lineRenderer.positionCount = 0;
    }

    private void DrawRope()
    {
        if(OnGrapping)
        {
            lineRenderer.SetPosition(0,gunTip.transform.position);
            this.transform.LookAt(spot);
        }
    }
}
