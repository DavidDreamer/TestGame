using System;
using UnityEngine;

public static class DamageService
{
    public static event Action<DamageEventArgs> OnDamageApply;

    public static void Apply(Character source, Character target, DamageParams damageParams)
    {
        float amount = UnityEngine.Random.Range(damageParams.Min, damageParams.Max);
        bool critical = UnityEngine.Random.value <= damageParams.CritChance;
        if (critical)
        {
            amount *= damageParams.CritMultiplier;
        }

        target.Health.Current = Mathf.Max(0, target.Health.Current - amount);

        DamageEventArgs damageEvenArgs = new()
        {
            Source = source,
            Target = target,
            Amount = amount
        };

        OnDamageApply?.Invoke(damageEvenArgs);
        
        bool targetKilled = target.Health.Current == 0;

        if (targetKilled)
        {
            target.Die();
        }
    }
}
