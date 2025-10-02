using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private Dictionary<string, int> stats = new Dictionary<string, int>();
    public int GetTimePoints() => GetStat("TimePoints");
    public bool TrySpendTimePoints(int amount)
    {
        int current = GetTimePoints();
        if (current >= amount)
        {
            ChangeStat("TimePoints", -amount);
            return true;
        }
        return false;
    }
    public int skillPoints = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Инициализация стартовых очков времени
            if (!stats.ContainsKey("TimePoints"))
                stats.Add("TimePoints", 300); // стартовое количество часов
            if (!stats.ContainsKey("SkillPoints"))
                stats.Add("SkillPoints", 0); // стартовые очки навыков, если нужны
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Добавляем или обновляем значения в словаре
    public void AddOrUpdateStats(Dictionary<string, int> newStats)
    {
        foreach (var kvp in newStats)
        {
            if (stats.ContainsKey(kvp.Key))
                stats[kvp.Key] = kvp.Value; // обновляем существующую стату
            else
                stats.Add(kvp.Key, kvp.Value); // добавляем новую
        }
    }

    // Получить значение конкретной статы
    public int GetStat(string statName)
    {
        if (stats.ContainsKey(statName))
            return stats[statName];
        return 0;
    }

    // Получить все статы
    public Dictionary<string, int> GetAllStats()
    {
        return new Dictionary<string, int>(stats);
    }

    // Изменить значение конкретной статы на amount
    public void ChangeStat(string statName, int amount)
    {
        if (stats.ContainsKey(statName))
            stats[statName] = Mathf.Clamp(stats[statName] + amount, 0, 100);
    }

    // Попытка прокачать (с проверкой очков)
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
