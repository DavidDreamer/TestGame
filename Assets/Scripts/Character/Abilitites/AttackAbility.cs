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
            var collidedCharacter = collider.GetComponent<Character>();

            if (collidedCharacter == null)
            {
                continue;
            }
            
            if (collidedCharacter == character)
            {
                continue;
            }

            collidedCharacter.Health.Current = Mathf.Max(0, collidedCharacter.Health.Current - 10);
        }
    }
}
