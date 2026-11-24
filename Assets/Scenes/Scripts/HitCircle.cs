using System.Collections;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    private float approachRate = 1f;
    private float elapsedTime = 0f;
    private bool missed = false;

    [SerializeField] private GameObject Pozde;
    [SerializeField] private GameObject ApproachCircle;
    [SerializeField] private GameObject Dite;

    public void Init(float rate)
    {
        approachRate = Mathf.Max(rate, 0.01f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        StartCoroutine(ClickTime());
    }

    IEnumerator ClickTime()
    {
        float t = Mathf.Clamp01(elapsedTime / approachRate);
        ApproachCircle.transform.localScale = Vector3.one * Mathf.Lerp(3f, 1f, t);

        yield return new WaitForSeconds(approachRate + 0.3f);
        if (ApproachCircle.transform.localScale.x <= 1)
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
        Destroy(gameObject);
    }
}