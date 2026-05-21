using UnityEngine;

public abstract class AbilityConfig : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public float CastTime { get; private set; }

    [field: SerializeField]
    public float Cooldown { get; private set; }

    public abstract Ability Create();
}