using UnityEngine;

public class Ability
{
    private AbilityConfig Config { get; set; }

    public string Name => Config.Name;

    public float Cooldown => Config.Cooldown;

    public AbilityState State { get; private set; }

    public float CastingTimeLeft { get; private set; }

    public float CooldownTimeLeft { get; private set; }

    public float CooldownProgress => CooldownTimeLeft / Cooldown;

    public bool IsReady => State == AbilityState.Ready;

    public Ability(AbilityConfig config)
    {
        Config = config;
    }

    public void Activate()
    {
        CastingTimeLeft = Config.CastTime;
        State = AbilityState.Casting;
    }

    public void Tick(Character character)
    {
        switch (State)
        {
            case AbilityState.Ready:
                break;
            case AbilityState.Casting:
                CastingTimeLeft = Mathf.Max(0, CastingTimeLeft - Time.deltaTime);
                if (CastingTimeLeft == 0)
                {
                    OnCast(character);
                    CooldownTimeLeft = Cooldown;
                    State = AbilityState.OnCooldown;
                }
                else
                {
                    float progress = CastingTimeLeft / Config.CastTime;
                    OnCasting(character, progress);
                }
                break;
            case AbilityState.OnCooldown:
                CooldownTimeLeft = Mathf.Max(0, CooldownTimeLeft - Time.deltaTime);
                if (CooldownTimeLeft == 0f)
                {
                    State = AbilityState.Ready;
                }
                break;
        }
    }

    public virtual void OnCasting(Character character, float progress)
    {
    }

    public virtual void OnCast(Character character)
    {
    }
}
