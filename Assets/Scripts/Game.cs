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
                break;
            case GameState.Victory:
                break;
            case GameState.Defeat:
                break;
            case GameState.Clenup:
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
            Data.Player = Instantiate(Config.PlayerPrefab);
            Data.Camera = Instantiate(Config.CameraController);
            Data.Camera.Bind(Data.Player);
        }
    }
}
