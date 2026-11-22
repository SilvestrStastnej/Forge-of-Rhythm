using System;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static event Action OnOrderStart;

    CharManager charManager;
    Controlz controlz;

    private void Awake()
    {
        charManager = FindAnyObjectByType<CharManager>();
        controlz = FindAnyObjectByType<Controlz>();
    }

    private void OnEnable()
    {
        CharManager.OrderStart += OrderStart;
    }

    private void OnDisable()
    {
        CharManager.OrderStart -= OrderStart;
    }

    private void OrderStart()
    {
        charManager.IsInProgress = true;
        controlz.isInOrder = true;
        OnOrderStart?.Invoke();
    }
}
