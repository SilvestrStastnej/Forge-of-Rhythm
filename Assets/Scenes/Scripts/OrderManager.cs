using UnityEngine;

public class OrderManager : MonoBehaviour
{
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

    private void OrderStart()
    {
        charManager.IsInProgress = true;
        controlz.isInOrder = true;

        Debug.Log("Konino");
    }
}
