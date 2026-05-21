using UnityEngine;

public class CharacterAIController : MonoBehaviour
{
    private Character Character { get; set; }

    public void Initialize(Character character)
    {
        Character = character;

        Character.NavMeshAgent.enabled = true;
        Character.NavMeshAgent.speed = Character.Config.MovementSpeed;
        Character.NavMeshAgent.angularSpeed = Character.Config.RotationSpeed;
    }

    public void Tick(Character target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        var attackAbility = (AttackAbility)Character.Abilities[0];

        if (distanceToTarget <= attackAbility.Config.Radius && !attackAbility.OnCooldown)
        {
            Character.NavMeshAgent.isStopped = true;
            Character.Abilities[0].Activate(Character);
        }
        else
        {
            Character.NavMeshAgent.isStopped = false;
            Character.NavMeshAgent.destination = target.transform.position;
        }
    }
}
