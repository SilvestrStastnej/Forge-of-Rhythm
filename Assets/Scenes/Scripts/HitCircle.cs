using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public float ApproachRate = 1f;
    private float ElapsedTime = 0f;
    [SerializeField] private GameObject Pozde;
    [SerializeField] private GameObject ApproachCircle;
    [SerializeField] private GameObject Dite;

    void Update()
    {
        ElapsedTime += Time.deltaTime;

        float scale = Mathf.Lerp(3f, 1f, Mathf.Clamp01(ElapsedTime / ApproachRate));
        ApproachCircle.transform.localScale = Vector3.one * scale;
        if (ApproachCircle.transform.localScale == new Vector3(1, 1, 1))
        {
            RectTransform buttonRect = GetComponentInChildren<RectTransform>();
            RectTransform pozdeRect = Pozde.GetComponent<RectTransform>();

            pozdeRect.position = buttonRect.position;
            Pozde.SetActive(true);
            Dite.SetActive(false);
            Destroy(this.gameObject, 0.4f);
        }
    }

    public void Naklik()
    {
            Destroy(this.gameObject);
    }
}
