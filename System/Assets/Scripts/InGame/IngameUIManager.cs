using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUIManager : MonoBehaviour
{
    public static IngameUIManager Instance;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI swipeText;
    public GameObject coinPopupPrefab;

    public Canvas uiCanvas;

    void Awake() => Instance = this;

    public void UpdateCoins(int coin)
    {
        coinText.text = $"{coin}";
    }

    public void UpdateSwipes(int count) // ¡ç Ãß°¡
    {
        swipeText.text = $"{count}";
    }

    public void ShowCoinPopup(int amount, Vector2 screenPosition)
    {
        Vector2 anchoredPos;
        Camera cam = (uiCanvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : uiCanvas.worldCamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.transform as RectTransform,
            screenPosition,
            cam,
            out anchoredPos
        );

        Debug.Log($"Mouse Screen Position: {screenPosition}");
        Debug.Log($"Converted Anchored Position: {anchoredPos}");


        GameObject popupObj = Instantiate(coinPopupPrefab, uiCanvas.transform);
        popupObj.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        popupObj.GetComponent<CoinPopup>().Init(amount);
    }
}
