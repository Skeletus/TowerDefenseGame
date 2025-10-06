using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 120;

    [Header("Rotation and Details")]
    [SerializeField] private Transform focusPoint;
    [SerializeField] private float maxFocusPointDistance = 15;
    [SerializeField] private float rotationSpeed = 200;
    [Space]
    private float pitch;
    private float minPitch = 5f;
    private float maxPitch = 85f;

    [Header("Zoom Details")]
    [SerializeField] private float zoomSpeed = 10;
    [SerializeField] private float minZoom = 3;
    [SerializeField] private float maxZoom = 15;

    private float smoothTime = .1f;
    private Vector3 movementVelocity = Vector3.zero;
    private Vector3 zoomVelocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleZoom();
        HandleMovement();

        focusPoint.position = transform.position + (transform.forward * GetFocusPointDistance());
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = transform.position;

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        if (verticalInput > 0)
        {
            targetPosition += flatForward * movementSpeed * Time.deltaTime;
        }
        if (verticalInput < 0)
        {
            targetPosition -= flatForward * movementSpeed * Time.deltaTime;
        }

        if (horizontalInput > 0)
        {
            targetPosition += transform.right * movementSpeed * Time.deltaTime;
        }
        if (horizontalInput < 0)
        {
            targetPosition -= transform.right * movementSpeed * Time.deltaTime;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref movementVelocity, smoothTime);
    }

    private void HandleRotation()
    {
        // 1 -> right click
        if (Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float verticalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch - verticalRotation, minPitch, maxPitch );

            transform.RotateAround(focusPoint.position, Vector3.up, horizontalRotation);
            transform.RotateAround(focusPoint.position, transform.right, pitch - transform.eulerAngles.x);

            transform.LookAt(focusPoint);
        }
    }

    private float GetFocusPointDistance()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxFocusPointDistance))
        {
            return hit.distance;
        }

        return maxFocusPointDistance;
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomDirection = transform.forward * scroll * zoomSpeed;
        Vector3 targetPosition = transform.position + zoomDirection;

        if (transform.position.y < minZoom && scroll > 0)
        {
            return;
        }
        if (transform.position.y > maxZoom && scroll < 0)
        {
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref zoomVelocity, smoothTime);
    }
}
