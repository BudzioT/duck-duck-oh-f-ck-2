using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coins = 0;
    public TextMeshProUGUI coinText;

    void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public void RemoveCoins(int amount)
    {
        coins = Mathf.Max(0, coins - amount);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = coins.ToString();
    }
}
