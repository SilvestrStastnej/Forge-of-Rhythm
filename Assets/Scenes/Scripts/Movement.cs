using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    [SerializeField] private float stepDistance = 4.71f;
    [SerializeField] private float stepDuration = 0.08f;
    [SerializeField] private float turnDuration = 1f;
    [SerializeField] private LayerMask obstacleLayer;

    private Rigidbody Rbody;
    private Transform Tbody;
    [SerializeField] private Transform Raycaster;

    private bool isMoving = false;
    private bool isRotating = false;

    IEnumerator SmoothRotace(float angle)
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

            Rbody.MovePosition(Vector3.Slerp(startPosition, endPosition, t));

            yield return null;
        }

        isMoving = false;
    }

    private void Awake()
    {
        Rbody = GetComponent<Rigidbody>();
        Tbody = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!isRotating && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.A))
                StartCoroutine(SmoothRotace(-90));
            if (Input.GetKeyDown(KeyCode.D))
                StartCoroutine(SmoothRotace(90));


            if (!CheckPredeMnou())
            {
                if (Input.GetKeyDown(KeyCode.W))
                    StartCoroutine(SmoothMovement(Tbody.forward));
            }
            if (!CheckZaMnou())
            {
                if (Input.GetKeyDown(KeyCode.S))
                    StartCoroutine(SmoothMovement(-Tbody.forward));
            }
        }
    }

    bool CheckPredeMnou()
    { return Physics.Raycast(Raycaster.position, Raycaster.forward, stepDistance, obstacleLayer); }
    bool CheckZaMnou()
    { return Physics.Raycast(Raycaster.position, -Raycaster.forward, stepDistance, obstacleLayer); }

}
