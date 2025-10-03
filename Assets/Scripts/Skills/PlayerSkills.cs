using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private Dictionary<string, int> stats = new Dictionary<string, int>();
    public int skillPoints = 0; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddOrUpdateStats(Dictionary<string, int> newStats)
    {
        foreach (var kvp in newStats)
        {
            if (stats.ContainsKey(kvp.Key))
                stats[kvp.Key] = kvp.Value;
            else
                stats.Add(kvp.Key, kvp.Value);
        }
    }

    public int GetStat(string statName)
    {
        if (stats.ContainsKey(statName))
            return stats[statName];
        return 0;
    }

    public Dictionary<string, int> GetAllStats()
    {
        return new Dictionary<string, int>(stats);
    }

    public void ChangeStat(string statName, int amount)
    {
        if (stats.ContainsKey(statName))
            stats[statName] = Mathf.Clamp(stats[statName] + amount, 0, 100);
    }

    public bool TryIncreaseStat(string statName)
    {
        if (skillPoints > 0 && stats.ContainsKey(statName))
        {
            stats[statName] = Mathf.Clamp(stats[statName] + 1, 0, 100);
            skillPoints--;
            return true;
        }
        return false;
    }
}
