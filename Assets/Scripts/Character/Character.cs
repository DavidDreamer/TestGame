using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField]
    public CharacterConfig Config { get; set; }

    [field: SerializeField]
    public Health Health { get; set; }

    public bool IsDead => Health.Current == 0f;

    public void Initialize()
    {
        Health.Current = Config.Health;
        Health.Maximum = Config.Health;
    }
}
