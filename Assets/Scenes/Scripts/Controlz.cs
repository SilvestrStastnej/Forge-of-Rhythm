using System;
using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;
using Color = UnityEngine.Color;

public class Controlz : MonoBehaviour
{
    public static event Action SmithStart;

    [SerializeField] private float stepDistance = 4.71f;
    [SerializeField] private float stepDuration = 0.08f;
    [SerializeField] private float turnDuration = 1f;
    [SerializeField] private LayerMask obstacleLayer;

    private Camera cam;
    [SerializeField]private Transform RayCam;
    private Rigidbody Rbody;
    private Transform Tbody;
    [SerializeField] private Transform Raycaster;
    [SerializeField] private GameObject CharTriggerSpot;
    public bool isInOrder = false;
    private bool isMoving = false;
    private bool isRotating = false;
    private bool isLooking = false;
    public bool YouGood = false;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        if (CheckKovadlina() && Input.GetKeyDown(KeyCode.E) && isInOrder == true)
        {
            SmithStart?.Invoke();
        }
    }


    private bool CheckKovadlina()
    {
        return Physics.Raycast(cam.transform.position, cam.transform.forward, 4f, obstacleLayer);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CharTriggerSpot)
        {
            YouGood = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == CharTriggerSpot)
        {
            YouGood = false;
        }
    }

}
