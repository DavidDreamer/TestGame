using TMPro;
using UnityEngine;

public class VictoryScreen : ResultScreen
{
    [field: SerializeField]
    private TextMeshProUGUI Reward { get; set; }

    public void Refresh(Statistics statistics, int coins)
    {
        Refresh(statistics);

        Reward.text = $"+{coins} COINS";
    }
}
