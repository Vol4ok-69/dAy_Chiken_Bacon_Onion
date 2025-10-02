using System.Collections.Generic;
using UnityEngine;

public class ShowAllStatsDebug : MonoBehaviour
{
    // Вызываем вывод при старте сцены
    void Start()
    {
        PrintAllStats();
    }

    // Метод для вывода всех статов
    public void PrintAllStats()
    {
        Dictionary<string, int> allStats = PlayerStats.Instance.GetAllStats();

        if (allStats.Count == 0)
        {
            Debug.Log("Статы игрока пусты!");
            return;
        }

        Debug.Log("=== Все статы игрока ===");
        foreach (var kvp in allStats)
        {
            Debug.Log($"{kvp.Key} = {kvp.Value}");
        }
        Debug.Log("========================");
    }
}

