using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;
    Camera cam;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] float currentRotate = 0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        PlayerMove();
        RotateCamera();
    }

    private void PlayerMove()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * inputX;
        Vector3 moveVertical = transform.forward * inputZ;

        Vector3 velo = (moveHorizontal + moveVertical).normalized * moveSpeed;

        rigidbody.MovePosition(transform.position + velo * Time.deltaTime);
    }

    public void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse Y");
        float mouseY = Input.GetAxis("Mouse X");

        currentRotate -= mouseX * rotateSpeed;

        currentRotate = Mathf.Clamp(currentRotate, -80f, 80f);

        transform.localRotation *= Quaternion.Euler(0, mouseY * rotateSpeed, 0);
        cam.transform.localEulerAngles = new Vector3(currentRotate, 0f, 0f);
    }
}
