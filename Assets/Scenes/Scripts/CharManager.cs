using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour
{
    public static event Action<bool> OnStart;
    public static event Action<bool> OnEnd;

    [SerializeField] private Transform CharacterPoint;
    [SerializeField] private Transform CharacterPoint2;
    [SerializeField] private Transform CharacterSpawn;
    [SerializeField] private List<GameObject> CharPrefabs;
    
    private GameObject newFab;

    private void Start()
    {
        StartCoroutine(SmoothCharStart());
    }

    private void Update()
    {
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
        OnStart?.Invoke(true);
    }

    private void Rozhodnuti()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
            
        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(SmoothCharEnd());
    }

    IEnumerator SmoothCharEnd()
    {

        OnEnd?.Invoke(false);
        newFab.transform.position = CharacterPoint.transform.position;

        Vector3 startPosition = newFab.transform.transform.position;
        Vector3 endPosition = CharacterPoint2.position;

        float elapsed = 0f;

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
    }
}
