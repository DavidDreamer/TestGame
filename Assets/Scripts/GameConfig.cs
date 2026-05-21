using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField]
    public Character PlayerPrefab { get; private set; }

    [field: SerializeField]
    public Character EnemyPrefab { get; private set; }

    [field: SerializeField]
    public CameraController CameraController { get; private set; }

    [field: SerializeField]
    public CharacterInputController CharacterInputController { get; private set; }

    [field: SerializeField]
    public int MinEnemiesCount { get; private set; }

    [field: SerializeField]
    public int MaxEnemiesCount { get; private set; }
}
