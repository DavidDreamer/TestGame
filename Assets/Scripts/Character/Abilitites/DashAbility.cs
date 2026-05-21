public class DashAbility : Ability
{
    public DashAbilityConfig Config { get; }

    public DashAbility(DashAbilityConfig config) : base(config)
    {
        Config = config;
    }
}