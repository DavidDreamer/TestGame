using UnityEngine;

[CreateAssetMenu(fileName = "DashAbilityConfig", menuName = "Scriptable Objects/DashAbilityConfig")]
public class DashAbilityConfig : AbilityConfig
{
    [field: SerializeField]
    public float Distance { get; private set; }

    public override Ability Create() => new DashAbility(this);
}
