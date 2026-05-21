using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Game : MonoBehaviour
{
    [field: SerializeField]
    private GameConfig Config { get; set; }

    [field: SerializeField]
    private UI UI { get; set; }

    private GameState State { get; set; }

    private GameData Data { get; set; }

    private void Update()
    {
        switch (State)
        {
            case GameState.Setup:
                Setup();
                break;
            case GameState.Running:
                Running();
                break;
            case GameState.Victory:
                Victory();
                break;
            case GameState.Defeat:
                Defeat();
                break;
            case GameState.Clenup:
                Cleanup();
                break;
            default: throw new System.Exception($"Unknown State : {State}");
        }
    }

    private void Setup()
    {
        Data = new();

        SetupPlayerCharacter();
        SetupEnemies();

        State = GameState.Running;

        void SetupPlayerCharacter()
        {
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            Character character = Instantiate(Config.PlayerPrefab, position, rotation);
            character.Initialize();

            CameraController cameraController = Instantiate(Config.CameraController);
            cameraController.Bind(character);

            UI.Health.Bind(character.Health);

            Data.Player = character;
            Data.Camera = cameraController;
        }

        void SetupEnemies()
        {
            int count = Random.Range(Config.MinEnemiesCount, Config.MaxEnemiesCount + 1);

            for (int i = 0; i < count; i++)
            {
                Vector3 position = GetRandomPositionOnSurface();
                Quaternion rotation = Quaternion.identity;
                Character character = Instantiate(Config.EnemyPrefab, position, rotation);
                character.Initialize();

                Data.Enemies.Add(character);
            }

            Vector3 GetRandomPositionOnSurface()
            {
                //Hardcoded radius of the location
                const float radius = 25;

                Vector3 position = Random.insideUnitSphere * radius;

                if (NavMesh.SamplePosition(position, out NavMeshHit hit, radius, NavMesh.AllAreas))
                {
                    return hit.position;
                }
                else
                {
                    return Vector3.zero;
                }
            }
        }
    }

    private void Running()
    {
        Data.Player.Health.Current = Mathf.Max(0, Data.Player.Health.Current - 20f * Time.deltaTime);

        UI.Health.Tick();

        if (WinCondition())
        {
            State = GameState.Victory;
        }
        else if (LoseCondition())
        {
            State = GameState.Defeat;
        }

        bool WinCondition() => Data.Enemies.All(enemy => enemy.IsDead);

        bool LoseCondition() => Data.Player.IsDead;
    }

    private void Victory()
    {
        State = GameState.Clenup;
    }

    private void Defeat()
    {
        State = GameState.Clenup;
    }

    private void Cleanup()
    {
        Destroy(Data.Player.gameObject);
        Destroy(Data.Camera.gameObject);

        foreach(Character character in Data.Enemies)
        {
            Destroy(character.gameObject);
        }

        Data = null;

        State = GameState.Setup;
    }
}
