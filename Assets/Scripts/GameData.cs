using System;
using System.Collections.Generic;

public class GameData : IDisposable
{
    public Character Player { get; set; }

    public CameraController Camera { get; set; }

    public List<Character> Enemies { get; set; } = new();

    public void Dispose()
    {
        UnityEngine.Object.Destroy(Player.gameObject);
        UnityEngine.Object.Destroy(Camera.gameObject);

        foreach (Character character in Enemies)
        {
            UnityEngine.Object.Destroy(character.gameObject);
        }
    }
}
