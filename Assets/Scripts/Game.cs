using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [field: SerializeField]
    private GameConfig Config { get; set; }

    [field: SerializeField]
    private UI UI { get; set; }

    private IMetaDataProvider MetaDataProvider { get; set; }

    private MetaData MetaData { get; set; }

    private GameState State { get; set; }

    private GameData Data { get; set; }

    private void Start()
    {
        MetaDataProvider = new PlayerPrefsMetaDataProvider();
        MetaData = MetaDataProvider.Load();

        UI.MetaScreen.Refresh(MetaData);

        ChangeState(GameState.Meta);
    }

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
            default: throw new System.Exception($"Unknown State : {State}");
        }
    }

    private void LateUpdate()
    {
        switch (State)
        {
            case GameState.Fight:
                Data.Camera.Tick();
                break;
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
            Data.Player = Instantiate(Config.PlayerPrefab, Vector3.zero, Quaternion.identity);
            Data.Player.Initialize();

            Data.Camera = Instantiate(Config.CameraController);
            Data.Camera.Bind(Data.Player);

            Data.InputController = Instantiate(Config.CharacterInputController);
            Data.InputController.Bind(Data.Player);

            UI.Bind(Data.Player);
        }

        void SetupEnemies()
        {
            int count = UnityEngine.Random.Range(Config.MinEnemiesCount, Config.MaxEnemiesCount + 1);

            for (int i = 0; i < count; i++)
            {
                Vector3 position = NavMeshUtils.GetRandomPositionOnSurface();
                Character character = Instantiate(Config.EnemyPrefab, position, Quaternion.identity);
                character.Initialize();

                var aiController = character.AddComponent<CharacterAIController>();
                aiController.Initialize(character);
                Data.AIControllers.Add(aiController);

                UI.CreateWorldSpaceHealthWidget(character);

                Data.Enemies.Add(character);
            }
        }
    }

    private void Fight()
    {
        Data.InputController.Tick();

        foreach (var aiController in Data.AIControllers)
        {
            aiController.Tick(Data.Player);
        }

        Data.Player.Tick();

        foreach (var enemy in Data.Enemies)
        {
            enemy.Tick();
        }

        UI.Tick();

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
            Cleanup();
            ChangeState(GameState.Meta);
        }
    }

    private void Defeat()
    {
        if (UI.DefeatScreen.InputProvided)
        {
            Cleanup();
            ChangeState(GameState.Meta);
        }
    }

    private void Cleanup()
    {
        Data.Dispose();
        UI.Cleanup();
    }
}
