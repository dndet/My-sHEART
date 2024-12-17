using UnityEngine;
using UnityEngine.UI;

public class tutorialControl : MonoBehaviour
{
    [SerializeField] private ScrollRect Pr_scrollRect;
    [SerializeField] private Button Pr_preBtn, Pr_nextBtn;
    [SerializeField] private Transform Pr_content;

    private float targetPoint, speed;
    void Start()
    {
        targetPoint = 0;
        speed = 1.0f / (Pr_content.childCount - 1);
        if (Pr_preBtn != null)
        {
            Pr_preBtn.onClick.AddListener(() =>
            {
                if(Pr_content != null && Pr_scrollRect != null)
                {
                    targetPoint = Mathf.Clamp(targetPoint - speed, 0, 1);
                    Pr_nextBtn.gameObject.SetActive(targetPoint < 0.99f);
                    Pr_preBtn.gameObject.SetActive(targetPoint > 0.01f);
                }
            });
        }

        if (Pr_nextBtn != null)
        {
            Pr_nextBtn.onClick.AddListener(() =>
            {
                if (Pr_content != null && Pr_scrollRect != null)
                {
                    targetPoint = Mathf.Clamp(targetPoint + speed, 0, 1);
                    Pr_nextBtn.gameObject.SetActive(targetPoint < 0.99f);
                    Pr_preBtn.gameObject.SetActive(targetPoint > 0.01f);
                }
            });
        }
        Pr_nextBtn.gameObject.SetActive(Pr_scrollRect.horizontalNormalizedPosition < 1);
        Pr_preBtn.gameObject.SetActive(Pr_scrollRect.horizontalNormalizedPosition > 0);
    }

    void Update()
    {

        Pr_scrollRect.horizontalNormalizedPosition = Mathf.Lerp(Pr_scrollRect.horizontalNormalizedPosition, targetPoint, speed * 0.75f);
    }
}
