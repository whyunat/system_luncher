using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinReward
{
    public int coinAmount;
    public float weight; // 확률 가중치 (예: 70f = 70%)
}

public class Window : MonoBehaviour
{
    private int swipeCount = 0;

    [Header("코인 보상 확률 리스트")]
    public List<CoinReward> coinRewards;

    public void SwipeClean()
    {
        swipeCount++;
        NormalizeRewards(); //확률 보정
        int coin = GetRandomCoin(); // 보상 결정

        CoinManager.Instance.AddCoin(coin);
        IngameUIManager.Instance.UpdateCoins(CoinManager.Instance.TotalCoins);
        IngameUIManager.Instance.UpdateSwipes(swipeCount);

        Vector2 screenPos = Input.mousePosition;
        IngameUIManager.Instance.ShowCoinPopup(coin, screenPos);
    }

    private int GetRandomCoin()
    {
        float totalWeight = 0f;
        foreach (var reward in coinRewards)
            totalWeight += reward.weight;

        float rand = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var reward in coinRewards)
        {
            cumulative += reward.weight;
            if (rand <= cumulative)
                return reward.coinAmount;
        }

        return 1; // fallback
    }

    private void NormalizeRewards()
    {
        float total = 0f;
        foreach (var r in coinRewards) total += r.weight;
        if (total <= 0f) return;

        foreach (var r in coinRewards)
            r.weight = r.weight / total * 100f; // 100 기준으로 재조정
    }

}
