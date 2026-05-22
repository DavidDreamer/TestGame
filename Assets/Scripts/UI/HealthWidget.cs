using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthWidget : MonoBehaviour
{
    [field: SerializeField]
    private CanvasGroup CanvasGroup { get; set; }

    [field: SerializeField]
    private Slider Slider { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI Label { get; set; }

    [field: SerializeField]
    private bool HideWhenOutOfHealth { get; set; }

    private Health Health { get; set; }

    public void Bind(Health health)
    {
        Health = health;

        Slider.minValue = 0;
        Slider.maxValue = health.Maximum;
    }

    public virtual void Tick()
    {
        if (Health == null)
        {
            return;
        }

        if (HideWhenOutOfHealth)
        {
            CanvasGroup.alpha = Health.Current > 0f ? 1f : 0f;
        }

        Slider.value = Health.Current;
        Label.text = $"{(int)Health.Current}/{(int)Health.Maximum}";
    }
}
