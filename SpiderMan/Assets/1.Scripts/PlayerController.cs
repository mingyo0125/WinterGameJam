using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody  rigidbody;
    Camera cam;
    RaycastHit hit;
    public LayerMask layerMask;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] float currentRotate = 0;

    [SerializeField] GameObject player;
    
    bool canCling;
    public bool canClimbing;

    float inputX;
    float inputZ;
    float mouseX;
    float mouseY;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Cling();
        PlayerMove();
        RotateCamera();

        if (canClimbing)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
        }
    }

    public void PlayerMove()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * inputX;
        Vector3 moveVertical = transform.forward * inputZ;

        Vector3 velo = (moveHorizontal + moveVertical).normalized * moveSpeed;

        rigidbody.MovePosition(transform.position + velo * Time.deltaTime);

    }

    private void RotateCamera()
    {
        mouseX = Input.GetAxis("Mouse Y");
        mouseY = Input.GetAxis("Mouse X");

        currentRotate -= mouseX * rotateSpeed;

        currentRotate = Mathf.Clamp(currentRotate, -80f, 80f);

        transform.localRotation *= Quaternion.Euler(0, mouseY * rotateSpeed, 0);
        cam.transform.localEulerAngles = new Vector3(currentRotate, 0f, 0f);
    }

    private void Cling()
    {
        if (canCling)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                rigidbody.drag = 100;
                inputZ = 0;
                canClimbing = true;
            }
            else if(Input.GetKeyUp(KeyCode.LeftShift))
            { 
                rigidbody.drag = 0;
                canClimbing = false;
            }
        }
    }

    private void Jump()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        canCling = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        canCling = false;
        canClimbing = false;
        rigidbody.drag = 0;
    }
}
