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

    private void Start() => ChangeState(GameState.Meta);

    private void Update()
    {
        switch (State)
        {
            case GameState.Meta:
                Meta();
                break;
            case GameState.Setup:
                Setup();
                break;
            case GameState.Fight:
                Fight();
                break;
            case GameState.Victory:
                Victory();
                break;
            case GameState.Defeat:
                Defeat();
                break;
            case GameState.Cleanup:
                Cleanup();
                break;
            default: throw new System.Exception($"Unknown State : {State}");
        }
    }

    private void ChangeState(GameState state)
    {
        State = state;
        UI.Refresh(state);
    }

    private void Meta()
    {
        if (UI.MetaScreen.InputProvided)
        {
            ChangeState(GameState.Setup);
        }
    }

    private void Setup()
    {
        Data = new();

        SetupPlayerCharacter();
        SetupEnemies();

        ChangeState(GameState.Fight);

        void SetupPlayerCharacter()
        {
            Character character = Instantiate(Config.PlayerPrefab, Vector3.zero, Quaternion.identity);
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
                Character character = Instantiate(Config.EnemyPrefab, position, Quaternion.identity);
                character.Initialize();

                HealthWidget healthWidget = Instantiate(Config.HealthWidgetWorldSpace, character.transform, false);
                healthWidget.Bind(character.Health);

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

    private void Fight()
    {
        Data.Player.Health.Current = Mathf.Max(0, Data.Player.Health.Current - 20f * Time.deltaTime);

        if (WinCondition())
        {
            ChangeState(GameState.Victory);
        }
        else if (LoseCondition())
        {
            ChangeState(GameState.Defeat);
        }

        bool WinCondition() => Data.Enemies.All(enemy => enemy.IsDead);

        bool LoseCondition() => Data.Player.IsDead;
    }

    private void Victory()
    {
        if (UI.VictoryScreen.InputProvided)
        {
            ChangeState(GameState.Cleanup);
        }
    }

    private void Defeat()
    {
        if (UI.DefeatScreen.InputProvided)
        {
            ChangeState(GameState.Cleanup);
        }
    }

    private void Cleanup()
    {
        Data.Dispose();
        ChangeState(GameState.Meta);
    }
}
