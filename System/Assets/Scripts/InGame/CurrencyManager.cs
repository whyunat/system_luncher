using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    private int coins = 0;

    void Awake() => Instance = this;

    public void AddCoins(int amount)
    {
        coins += amount;
        IngameUIManager.Instance.UpdateCoins(coins);
    }
}
