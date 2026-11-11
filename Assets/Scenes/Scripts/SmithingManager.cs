using UnityEngine;

public class SmithingManager : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    private void OnEnable()
    {
        Controlz.SmithStart += SmithSeqStart;
    }

    private void SmithSeqStart()
    {
        Debug.Log("SmithSeqStart");
        cam1.gameObject.SetActive(false);
        cam2.gameObject.SetActive(true);
    }
}
