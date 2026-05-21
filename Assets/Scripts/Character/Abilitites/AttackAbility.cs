public class AttackAbility : Ability
{
    public AttackAbilityConfig Config { get; }

    public AttackAbility(AttackAbilityConfig config) : base(config)
    {
        Config = config;
    }
}
