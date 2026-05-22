using UnityEngine;

public class StatisticsCollector
{
    public Statistics Statistics { get; }

    public StatisticsCollector()
    {
        Statistics = new();
    }

    public void Tick()
    {
        Statistics.TimePlayed += Time.deltaTime;
    }
}
