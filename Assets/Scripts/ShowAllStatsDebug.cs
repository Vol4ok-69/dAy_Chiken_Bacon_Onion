using System.Collections.Generic;
using UnityEngine;

public class ShowAllStatsDebug : MonoBehaviour
{
    // �������� ����� ��� ������ �����
    void Start()
    {
        PrintAllStats();
    }

    // ����� ��� ������ ���� ������
    public void PrintAllStats()
    {
        Dictionary<string, int> allStats = PlayerStats.Instance.GetAllStats();

        if (allStats.Count == 0)
        {
            Debug.Log("����� ������ �����!");
            return;
        }

        Debug.Log("=== ��� ����� ������ ===");
        foreach (var kvp in allStats)
        {
            Debug.Log($"{kvp.Key} = {kvp.Value}");
        }
        Debug.Log("========================");
    }
}

