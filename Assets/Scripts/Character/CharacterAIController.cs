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
        if (Character.IsDead)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        var attackAbility = (AttackAbility)Character.Abilities[0];

        if (distanceToTarget <= attackAbility.Config.Radius && attackAbility.IsReady)
        {
            Character.NavMeshAgent.isStopped = true;
            Character.TryActivateAbility(0);
        }
        else
        {
            Character.NavMeshAgent.isStopped = false;
            Character.NavMeshAgent.destination = target.transform.position;
        }
    }
}
