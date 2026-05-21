using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [field: SerializeField]
    public CharacterConfig Config { get; private set; }

    [field: SerializeField]
    public CharacterController CharacterController { get; private set; }

    [field: SerializeField]
    public NavMeshAgent NavMeshAgent { get; private set; }

    [field: SerializeField]
    public Animation Animation { get; private set; }

    [field: SerializeField]
    public Health Health { get; set; }

    public bool IsDead => Health.Current == 0f;

    //Assume we always have 2 abilities
    public Ability[] Abilities { get; private set; }

    public void Initialize()
    {
        Health.Current = Config.Health;
        Health.Maximum = Config.Health;

        Abilities = new Ability[Config.Abilities.Length];
        for (int i = 0; i < Abilities.Length; i++)
        {
            Abilities[i] = Config.Abilities[i].Create();
        }
    }

    public void Tick()
    {
        for (int i = 0; i < Abilities.Length; i++)
        {
            Abilities[i].Tick(this);
        }
    }

    public void TryActivateAbility(int index)
    {
        Ability ability = Abilities[index];

        if (!ability.IsReady)
        {
            return;
        }

        ability.Activate();

        Animation.Play(ability.Name, PlayMode.StopAll);
    }
}
