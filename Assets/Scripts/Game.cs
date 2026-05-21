using UnityEngine;

public class Game : MonoBehaviour
{
    [field: SerializeField]
    private GameConfig Config { get; set; }

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

        State = GameState.Running;

        void SetupPlayerCharacter()
        {
            Character character = Instantiate(Config.PlayerPrefab);
            character.Initialize();

            CameraController cameraController = Instantiate(Config.CameraController);
            cameraController.Bind(character);

            Data.Player = character;
            Data.Camera = cameraController;
        }
    }

    private void Running()
    {
        if (WinCondition())
        {
            State = GameState.Victory;
        }
        else if (LoseCondition())
        {
            State = GameState.Defeat;
        }

        bool WinCondition() => false;

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

        Data = null;

        State = GameState.Setup;
    }
}
