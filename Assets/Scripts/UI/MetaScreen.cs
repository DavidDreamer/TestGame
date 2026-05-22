using TMPro;
using Unity.VisualScripting;
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
        TimePlayed.text = metaData.TimePlayed.ToString();
        DamageDealt.text = metaData.DamageDealt.ToString();
        DamageReceived.text = metaData.DamageReceived.ToString();
        EnemyKilled.text = metaData.EnemyKilled.ToString();
        Coins.text = metaData.Coins.ToString();
    }
}
