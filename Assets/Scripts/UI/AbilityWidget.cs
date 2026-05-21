using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWidget : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI Name { get; set; }

    [field: SerializeField]
    public Image Cooldown { get; set; }

    private Ability Ability { get; set; }
    public void Bind(Ability ability)
    {
        Ability = ability;
        Name.text = ability.Name;
    }

    public void Tick()
    {
        Cooldown.fillAmount = Ability.ActiveCooldownNormalized;
    }
}
