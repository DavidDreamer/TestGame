using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthWidget : MonoBehaviour
{
    [field: SerializeField]
    private Slider Slider {get; set;}

    [field: SerializeField]
    private TextMeshProUGUI Label {get; set;}
    
    private Health Health {get;set;}

    public void Bind(Health health)
    {
        Health = health;

        Slider.minValue = 0;
        Slider.maxValue = health.Maximum;
    }
    
    public void Update()
    {
        if (Health == null)
        {
            return;
        }
        
        Slider.value = Health.Current;
        Label.text = $"{(int)Health.Current}/{(int)Health.Maximum}";
    }
}
