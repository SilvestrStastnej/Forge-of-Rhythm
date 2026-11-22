using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharManager : MonoBehaviour
{
    public static event Action<bool> OnStart;
    public static event Action<bool> OnEnd;

    public static event Action OrderStart;


    private bool ReachedPosition = false;
    public bool IsInProgress = false;
    public bool OrderFinished = false;

    [SerializeField] private Transform CharacterPoint;
    [SerializeField] private Transform CharacterPoint2;
    [SerializeField] private Transform CharacterSpawn;
    [SerializeField] private List<GameObject> CharPrefabs;

    private Controlz controlz;
    public GameObject newFab;


    private void OnEnable()
    {
        SmithingManager.SmithEnd -= OnSmithingComplete;
        SmithingManager.SmithEnd += OnSmithingComplete;
    }

    private void OnDisable()
    {
        SmithingManager.SmithEnd -= OnSmithingComplete;
    }

    private void OnSmithingComplete()
    {
            OrderFinished = true;
    }
    private void Start()
    {
        controlz = FindAnyObjectByType<Controlz>();
        StartCoroutine(SmoothCharStart());
    }

    private void Update()
    {
        if(ReachedPosition)
        Rozhodnuti();
    }

    IEnumerator SmoothCharStart()
    {   
        newFab = Instantiate(CharPrefabs[UnityEngine.Random.Range(0, CharPrefabs.Count)]);
        newFab.transform.position = CharacterSpawn.transform.position;

        Vector3 startPosition = newFab.transform.transform.position;
        Vector3 endPosition = CharacterPoint.position;

        float elapsed = 0f;

        while (elapsed < 0.7f)
        {
            float t = elapsed / 0.7f;

            elapsed += Time.deltaTime;

            newFab.transform.position = (Vector3.Lerp(startPosition, endPosition, t));

            yield return null;
        }
        newFab.transform.position = endPosition;
            ReachedPosition = true;
        OnStart?.Invoke(true);
    }

    private void Rozhodnuti()
    {
        if (!controlz.YouGood) return;

        if (!IsInProgress)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // přijmout objednávku
                OrderStart?.Invoke();
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // odmítnout
                StartCoroutine(SmoothCharEnd());
        }
        else if (OrderFinished && Input.GetKeyDown(KeyCode.Alpha1)) // předat hotovou věc
        {
            StartCoroutine(SmoothCharEnd());
        }
    }

    IEnumerator SmoothCharEnd()
    {

        ReachedPosition = false;
        IsInProgress = false;
        OrderFinished = false;

        OnEnd?.Invoke(false);
        newFab.transform.position = CharacterPoint.transform.position;

        Vector3 startPosition = newFab.transform.transform.position;
        Vector3 endPosition = CharacterPoint2.position;

        float elapsed = 0f;

        yield return new WaitForSeconds(2f);
        while (elapsed < 0.7f)
        {
            float t = elapsed / 0.7f;

            elapsed += Time.deltaTime;

            newFab.transform.position = (Vector3.Lerp(startPosition, endPosition, t));

            yield return null;
        }
        newFab.transform.position = endPosition;
        Destroy(newFab);
        StartCoroutine(SmoothCharStart());
        ReachedPosition = false;
        IsInProgress = false;
    }
}
