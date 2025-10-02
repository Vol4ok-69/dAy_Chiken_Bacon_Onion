using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    //  Словарь для хранения параметров
    private Dictionary<string, int> stats = new Dictionary<string, int>();

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

    // Установить стартовые значения (например, при выборе диплома)
    public void SetStats(Dictionary<string, int> newStats)
    {
        stats = new Dictionary<string, int>(newStats); // копируем, а не ссылаемся
    }

    // Получить значение
    public int GetStat(string statName)
    {
        if (stats.ContainsKey(statName))
            return stats[statName];
        return 0;
    }

    // Изменить значение
    public void ChangeStat(string statName, int amount)
    {
        if (stats.ContainsKey(statName))
            stats[statName] = Mathf.Clamp(stats[statName] + amount, 0, 100);
    }

    // Получить все параметры (например, для UI)
    public Dictionary<string, int> GetAllStats()
    {
        return new Dictionary<string, int>(stats);
    }
}
