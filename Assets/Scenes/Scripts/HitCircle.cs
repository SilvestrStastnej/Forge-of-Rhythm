using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public float ApproachRate = 1f;
    private float ElapsedTime = 0f;
    [SerializeField] private GameObject ApproachCircle;

    void Update()
    {
        ElapsedTime += Time.deltaTime;

        float scale = Mathf.Lerp(3f, 1f, Mathf.Clamp01(ElapsedTime / ApproachRate));
        ApproachCircle.transform.localScale = Vector3.one * scale;
        if (ApproachCircle.transform.localScale == new Vector3(1, 1, 1))
            Destroy(this.gameObject);
    }
}
