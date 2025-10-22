using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool canControl;
    [SerializeField] private Vector3 levelCenterPoint;
    [SerializeField] private float maxDistanceFromCenter; 

    [Header("Movement details")]
    [SerializeField] private float movementSpeed = 120;
    [SerializeField] private float mouseMovementSpeed = 5;

    [Header("Edge movement Details")]
    [SerializeField] private float edgeMovementSpeed = 10;
    [SerializeField] private float edgeTreshold = 10;
    private float screenWidth;
    private float screenHeight;
     
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
    private Vector3 mouseMovementVelocity = Vector3.zero;
    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 edgeMovementVelocity = Vector3.zero;

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl)
        {
            return;
        }

        HandleRotation();
        HandleZoom();
        HandleMovement();
        //HandleEdgeMovement();
        HandleMouseMovement();

        focusPoint.position = transform.position + (transform.forward * GetFocusPointDistance());
    }

    public void EnableCameraControls(bool enable)
    {
        canControl = enable;
    }

    public void AdjustPitchValue(float value)
    {
        pitch = value;
    }

    public void AdjustKeyboardSensitivity(float value)
    {
        movementSpeed = value;
    }

    private void HandleMovement()
    {
        Vector3 targetPosition = transform.position;

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (verticalInput == 0 && horizontalInput == 0)
        {
            return;
        }

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

        if (Vector3.Distance(levelCenterPoint, targetPosition) > maxDistanceFromCenter)
        {
            targetPosition = levelCenterPoint + (targetPosition - levelCenterPoint).normalized * maxDistanceFromCenter;
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

    private void HandleMouseMovement()
    {
        if (Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 positionDifference = Input.mousePosition - lastMousePosition;
            Vector3 moveRight = transform.right * (-positionDifference.x) * mouseMovementSpeed * Time.deltaTime;
            Vector3 moveForward = transform.forward * (-positionDifference.y) * mouseMovementSpeed * Time.deltaTime;

            moveRight.y = 0;
            moveForward.y = 0;

            Vector3 movement = moveForward + moveRight;
            Vector3 targetPosition = transform.position + movement;

            if(Vector3.Distance(levelCenterPoint, targetPosition) > maxDistanceFromCenter)
            {
                targetPosition = levelCenterPoint + (targetPosition - levelCenterPoint).normalized * maxDistanceFromCenter;
            }

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref mouseMovementVelocity, smoothTime);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleEdgeMovement()
    {
        Vector3 targetPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        if (mousePosition.x > screenWidth - edgeTreshold)
        {
            targetPosition += transform.right * edgeMovementSpeed * Time.deltaTime;
        }
        if (mousePosition.x < edgeTreshold)
        {
            targetPosition -= transform.right * edgeMovementSpeed * Time.deltaTime;
        }
        if (mousePosition.y > screenHeight - edgeTreshold)
        {
            targetPosition += flatForward * edgeMovementSpeed * Time.deltaTime;
        }
        if (mousePosition.y < edgeTreshold)
        {
            targetPosition -= flatForward * edgeMovementSpeed * Time.deltaTime;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref edgeMovementVelocity, smoothTime);

    }
}
