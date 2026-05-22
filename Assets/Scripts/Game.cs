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

    private GameData GameData { get; set; }

    private StatisticsCollector StatisticsCollector { get; set; }

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
                GameData.Camera.Tick();
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
        GameData = new();

        SetupPlayerCharacter();
        SetupEnemies();

        StatisticsCollector = new(GameData);

        ChangeState(GameState.Fight);

        void SetupPlayerCharacter()
        {
            GameData.Player = Instantiate(Config.PlayerPrefab, Vector3.zero, Quaternion.identity);
            GameData.Player.Initialize();

            GameData.Camera = Instantiate(Config.CameraController);
            GameData.Camera.Bind(GameData.Player);

            GameData.InputController = Instantiate(Config.CharacterInputController);
            GameData.InputController.Bind(GameData.Player);

            UI.Bind(GameData.Player);
        }

        void SetupEnemies()
        {
            int count = Random.Range(Config.MinEnemiesCount, Config.MaxEnemiesCount + 1);

            for (int i = 0; i < count; i++)
            {
                Vector3 position = NavMeshUtils.GetRandomPositionOnSurface();
                Character character = Instantiate(Config.EnemyPrefab, position, Quaternion.identity);
                character.Initialize();

                var aiController = character.AddComponent<CharacterAIController>();
                aiController.Initialize(character);
                GameData.AIControllers.Add(aiController);

                UI.CreateWorldSpaceHealthWidget(character);

                GameData.Enemies.Add(character);
            }
        }
    }

    private void Fight()
    {
        GameData.InputController.Tick();

        foreach (var aiController in GameData.AIControllers)
        {
            aiController.Tick(GameData.Player);
        }

        GameData.Player.Tick();

        foreach (var enemy in GameData.Enemies)
        {
            enemy.Tick();
        }

        UI.Tick();
        StatisticsCollector.Tick();

        if (WinCondition())
        {
            UI.VictoryScreen.Refresh(StatisticsCollector.Statistics, Config.CoinsForVictory);
            ChangeState(GameState.Victory);
        }
        else if (LoseCondition())
        {
            UI.DefeatScreen.Refresh(StatisticsCollector.Statistics);
            ChangeState(GameState.Defeat);
        }

        bool WinCondition() => GameData.Enemies.All(enemy => enemy.IsDead);

        bool LoseCondition() => GameData.Player.IsDead;
    }

    private void Victory()
    {
        if (UI.VictoryScreen.InputProvided)
        {
            MetaData.Coins += Config.CoinsForVictory;
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
        GameData.Dispose();
        UI.Cleanup();
        MetaData.Statistics += StatisticsCollector.Statistics;
        StatisticsCollector.Dispose();
        MetaDataProvider.Save(MetaData);
        UI.MetaScreen.Refresh(MetaData);
    }
}
