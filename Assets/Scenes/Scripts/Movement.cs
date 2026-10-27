using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    [SerializeField] private float stepDistance = 4.71f;
    [SerializeField] private float stepDuration = 0.08f;
    [SerializeField] private float turnDuration = 1f;
    [SerializeField] private LayerMask obstacleLayer;

    private Camera cam;
    private Rigidbody Rbody;
    private Transform Tbody;
    [SerializeField] private Transform Raycaster;

    private bool isMoving = false;
    private bool isRotating = false;
    private bool isLooking = false;

    IEnumerator SmoothRotation(float angle)
    {
        isRotating = true;
        float elapsed = 0f;

        Quaternion startRotation = Rbody.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, angle, 0f);

        while (elapsed < turnDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / turnDuration);

            Rbody.MoveRotation(Quaternion.Slerp(startRotation, endRotation, t));

            yield return null;
        }
        Rbody.MoveRotation(endRotation);
        isRotating = false;
    }

    IEnumerator SmoothLook()
    {
        isLooking = !isLooking;
        float elapsed = 0f;
        Quaternion angle = Quaternion.identity;


        if (isLooking)
            angle = Quaternion.Euler(40f, 0f, 0f);
        else
            angle = Quaternion.identity;

        Quaternion startRotation = cam.transform.localRotation;

        while (elapsed < turnDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / turnDuration);

            cam.transform.localRotation = Quaternion.Slerp(startRotation, angle, t);

            yield return null;
        }
        cam.transform.localRotation = angle;
    }

    IEnumerator SmoothMovement(Vector3 DecidingFactor)
    {
        isMoving = true;
        Vector3 startPosition = Rbody.position;
        Vector3 endPosition = startPosition + DecidingFactor * stepDistance;

        float elapsed = 0f;

        while (elapsed < stepDuration)
        {
            float t = Mathf.Clamp01(elapsed / stepDuration);

            elapsed += Time.deltaTime;

            Rbody.MovePosition(Vector3.Lerp(startPosition, endPosition, t));

            yield return null;
        }
        Rbody.MovePosition(endPosition);
        isMoving = false;
    }

    private void Awake()
    {
        Rbody = GetComponent<Rigidbody>();
        Tbody = GetComponent<Transform>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (!isRotating && !isMoving && !isLooking)
        {
            if (Input.GetKeyDown(KeyCode.A))
                StartCoroutine(SmoothRotation(-90));
            if (Input.GetKeyDown(KeyCode.D))
                StartCoroutine(SmoothRotation(90));


            if (CheckPredeMnou())
            {
                if (Input.GetKeyDown(KeyCode.W))
                    StartCoroutine(SmoothMovement(Tbody.forward));
            }
            if (CheckZaMnou())
            {
                if (Input.GetKeyDown(KeyCode.S))
                    StartCoroutine(SmoothMovement(-Tbody.forward));
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating && !isMoving)
            StartCoroutine(SmoothLook());
    }

private bool CheckPredeMnou()
{
    return Physics.Raycast(
        Raycaster.position,
        Raycaster.forward,
        out RaycastHit hit,
        5f,
        obstacleLayer,
        QueryTriggerInteraction.Collide
    ) && hit.collider.isTrigger;
}

private bool CheckZaMnou()
{
    return Physics.Raycast(
        Raycaster.position,
        -Raycaster.forward,
        out RaycastHit hit,
        5f,
        obstacleLayer,
        QueryTriggerInteraction.Collide
    ) && hit.collider.isTrigger;
}

}
