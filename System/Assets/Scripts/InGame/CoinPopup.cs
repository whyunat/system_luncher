using TMPro;
using UnityEngine;

public class CoinPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float floatUpSpeed = 50f;
    public float fadeDuration = 0.8f;

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private float timer;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Init(int amount)
    {
        text.text = $"+{amount}";
        timer = 0f;
    }

    void Update()
    {
        // ��������
        rect.anchoredPosition += Vector2.up * floatUpSpeed * Time.deltaTime;

        // ���̵� �ƿ�
        timer += Time.deltaTime;
        canvasGroup.alpha = 1f - (timer / fadeDuration);

        if (timer >= fadeDuration)
            Destroy(gameObject);
    }
}
