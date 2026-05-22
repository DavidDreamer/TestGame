using System;

[Serializable]
public class Statistics
{
    public float TimePlayed;
    public float DamageDealt;
    public float DamageReceived;
    public int EnemyKilled;

    public static Statistics operator +(Statistics left, Statistics right)
    {
        return new Statistics
        {
            TimePlayed = left.TimePlayed + right.TimePlayed,
            DamageDealt = left.DamageDealt + right.DamageDealt,
            DamageReceived = left.DamageReceived + right.DamageReceived,
            EnemyKilled = left.EnemyKilled + right.EnemyKilled
        };
    }
}
