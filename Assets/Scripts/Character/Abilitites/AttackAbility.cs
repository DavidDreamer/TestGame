using UnityEngine;

public class AttackAbility : Ability
{
    public AttackAbilityConfig Config { get; }

    public AttackAbility(AttackAbilityConfig config) : base(config)
    {
        Config = config;
    }

    public override void Activate(Character character)
    {
        base.Activate(character);

        //Unoptimized fast solution
        Collider[] colliders = Physics.OverlapSphere(character.transform.position, Config.Radius);

        foreach (Collider collider in colliders)
        {
            var target = collider.GetComponent<Character>();

            if (target == null)
            {
                continue;
            }

            if (target == character)
            {
                continue;
            }

            DamageService.Apply(character, target, Config.DamageParams);
        }
    }
}
