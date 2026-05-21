using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
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
}
