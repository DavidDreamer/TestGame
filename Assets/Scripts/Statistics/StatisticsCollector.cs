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
    }

    public void Dispose()
    {
        DamageService.OnDamageApply -= OnDamageApplied;
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

    public void Tick()
    {
        Statistics.TimePlayed += Time.deltaTime;
    }

    public void Reset()
    {
        Statistics = new();
    }
}
