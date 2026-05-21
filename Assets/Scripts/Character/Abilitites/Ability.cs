using UnityEngine;

public class Ability
{
    private AbilityConfig Config { get; set; }

    public string Name => Config.Name;

    public float Cooldown => Config.Cooldown;

    public float ActiveCooldown { get; private set; }

    public float ActiveCooldownNormalized => ActiveCooldown / Cooldown;

    public Ability(AbilityConfig config)
    {
        Config = config;
    }

    public virtual void Activate(Character character)
    {
        ActiveCooldown = Cooldown;
    }

    public void Tick()
    {
        if (ActiveCooldown > 0)
        {
            ActiveCooldown -= Mathf.Max(0, Time.deltaTime);
        }
    }
}
