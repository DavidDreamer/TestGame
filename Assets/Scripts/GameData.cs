using System.Collections.Generic;

public class GameData
{
    public Character Player { get; set; }

    public CameraController Camera { get; set; }

    public List<Character> Enemies { get; set; } = new();
}
