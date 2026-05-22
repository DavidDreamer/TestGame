using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshProUGUI TimePlayed { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI DamageDealt { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI DamageReceived { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI EnemyKilled { get; set; }

    [field: SerializeField]
    private Button Ok { get; set; }

    public bool InputProvided { get; private set; }

    private void OnEnable()
    {
        Ok.onClick.AddListener(OnPlayButtonClick);
        InputProvided = false;
    }

    private void OnDisable()
    {
        Ok.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClick() => InputProvided = true;

    public void Refresh(Statistics statistics)
    {
        //no formatting, only seconds displayed
        TimePlayed.text = $"{statistics.TimePlayed:00} s";
        DamageDealt.text = statistics.DamageDealt.ToString("00.00");
        DamageReceived.text = statistics.DamageReceived.ToString("00.00");
        EnemyKilled.text = statistics.EnemiesKilled.ToString();
    }
}
