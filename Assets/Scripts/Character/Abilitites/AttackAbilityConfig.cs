using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbilityConfig", menuName = "Scriptable Objects/AttackAbilityConfig")]
public class AttackAbilityConfig : AbilityConfig
{
    [field: SerializeField]
    public float Radius { get; private set; }

    [field: SerializeField]
    public DamageParams DamageParams { get; private set; }

    public override Ability Create() => new AttackAbility(this);
}
