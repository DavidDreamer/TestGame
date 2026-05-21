using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaScreen : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshProUGUI Time { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI DamageDealt { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI DamageReceived { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI EnemyKilled { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI Gold { get; set; }

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
}
