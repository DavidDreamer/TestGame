using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaScreen : MonoBehaviour
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
    private TextMeshProUGUI Coins { get; set; }

    [field: SerializeField]
    private Button Play { get; set; }

    public bool InputProvided { get; private set; }

    private void OnEnable()
    {
        Play.onClick.AddListener(OnPlayButtonClick);
        InputProvided = false;
    }

    private void OnDisable()
    {
        Play.onClick.RemoveAllListeners();
    }

    private void OnPlayButtonClick() => InputProvided = true;

    public void Refresh(MetaData metaData)
    {
        //no formatting, only seconds displayed
        TimePlayed.text = $"{metaData.Statistics.TimePlayed:00} s";
        DamageDealt.text = metaData.Statistics.DamageDealt.ToString("00.00");
        DamageReceived.text = metaData.Statistics.DamageReceived.ToString("00.00");
        EnemyKilled.text = metaData.Statistics.EnemiesKilled.ToString();
        Coins.text = metaData.Coins.ToString();
    }
}
