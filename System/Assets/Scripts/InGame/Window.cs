using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinReward
{
    public int coinAmount;
    public float weight; // Ȯ�� ����ġ (��: 70f = 70%)
}

public class Window : MonoBehaviour
{
    private int swipeCount = 0;

    [Header("���� ���� Ȯ�� ����Ʈ")]
    public List<CoinReward> coinRewards;

    public void SwipeClean()
    {
        swipeCount++;
        NormalizeRewards(); //Ȯ�� ����
        int coin = GetRandomCoin(); // ���� ����

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
            r.weight = r.weight / total * 100f; // 100 �������� ������
    }

}
