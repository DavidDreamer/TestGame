using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Scriptable Objects/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [field: SerializeField]
    public float Health { get; private set; }

    [field: SerializeField]
    public float MovementSpeed { get; private set; }

    [field: SerializeField]
    public float RotationSpeed { get; private set; }

    [field: SerializeField]
    public AbilityConfig[] Abilities { get; private set; }
}
