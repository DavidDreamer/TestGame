using System;
using UnityEngine;

public class StatisticsCollector : IDisposable
{
    private GameData GameData { get; set; }

    public Statistics Statistics { get; private set; }

    public StatisticsCollector(GameData gameData)
    {
        GameData = gameData;

        Statistics = new();

        DamageService.OnDamageApply += OnDamageApplied;
        DamageService.OnCharacterKill += OnCharacterKilled;
    }

    public void Dispose()
    {
        DamageService.OnDamageApply -= OnDamageApplied;
        DamageService.OnCharacterKill -= OnCharacterKilled;
    }

    private void OnDamageApplied(DamageEventArgs damageEventArgs)
    {
        if (damageEventArgs.Source == GameData.Player)
        {
            Statistics.DamageDealt += damageEventArgs.Amount;
        }
        else if (damageEventArgs.Target == GameData.Player)
        {
            Statistics.DamageReceived += damageEventArgs.Amount;
        }
    }

    private void OnCharacterKilled(KillEventArgs killEventArgs)
    {
        if (killEventArgs.Source == GameData.Player)
        {
            Statistics.EnemiesKilled ++;
        }
    }

    public void Tick()
    {
        Statistics.TimePlayed += Time.deltaTime;
    }

    public void Reset()
    {
        Statistics = new();
    }
}
