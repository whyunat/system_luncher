using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int TotalCoins { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddCoin(int amount)
    {
        TotalCoins += amount;
    }
}
