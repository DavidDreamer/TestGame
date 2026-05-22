using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public DashAbilityConfig Config { get; }

    private Vector3 StartPosition { get; set; }

    private Vector3 TargetPosition { get; set; }

    private HashSet<Character> AffectedChararcters { get; set; } = new();

    public DashAbility(DashAbilityConfig config) : base(config)
    {
        Config = config;
    }

    public override void OnCastStarted(Character character)
    {
        base.OnCastStarted(character);

        StartPosition = character.transform.position;
        TargetPosition = StartPosition + character.transform.forward * Config.Distance;
        TargetPosition = NavMeshUtils.GetValidPosition(TargetPosition);
    }

    public override void OnCasting(Character character, float progress)
    {
        base.OnCasting(character, progress);

        character.transform.position = Vector3.Lerp(StartPosition, TargetPosition, progress);

        //Unoptimized fast solution
        Collider[] colliders = Physics.OverlapSphere(character.transform.position, Config.DamageRadius);

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

            if (target.IsDead)
            {
                continue;
            }
            
            if (AffectedChararcters.Contains(target))
            {
                continue;
            }

            AffectedChararcters.Add(target);

            DamageService.Apply(character, target, Config.DamageParams);
        }
    }

    public override void OnCastFinished(Character character)
    {
        base.OnCastFinished(character);

        AffectedChararcters.Clear();
    }
}