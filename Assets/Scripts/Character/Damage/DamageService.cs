using UnityEngine;

public static class DamageService
{
    public static void Apply(Character source, Character target, DamageParams damageParams)
    {
        float amount = Random.Range(damageParams.Min, damageParams.Max);
        bool critical = Random.value <= damageParams.CritChance;
        if (critical)
        {
            amount *= damageParams.CritMultiplier;
        }

        target.Health.Current = Mathf.Max(0, target.Health.Current - amount);
    }     
}
