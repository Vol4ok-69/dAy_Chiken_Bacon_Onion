using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class StatsSpawner : MonoBehaviour
{
    [System.Serializable]
    public class StatData
    {
        public string name;
        public int value;
    }

    [Header("���������")]
    public List<StatData> statsData;          // ������ ������ ��� ���� �����
    public GameObject statPrefab;             // ������ UI ��� ��������� �����
    public Transform contentParent;           // ������������ ������ ��� UI
    public TextMeshProUGUI pointsLeftText;    // ����� ��� ���������� �����
    public int totalPoints = 20;              // ��������� ����

    [Header("����������������")]
    public float startY = 0f;
    public float spacing = -60f;

    void Start()
    {
        // ������� UI ��� ������ �����
        for (int i = 0; i < statsData.Count; i++)
        {
            StatData stat = statsData[i];
            GameObject go = Instantiate(statPrefab, contentParent);
            go.name = $"{statPrefab.name}_{i}";

            // ����� ������� � �������
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            rt.anchoredPosition = new Vector2(0, startY + i * spacing);

            // ��������� StatUI
            StatUI ui = go.GetComponent<StatUI>();
            if (ui != null)
            {
                ui.Setup(stat.name, stat.value, this);
                ui.statData = stat; // ��������� � ��������� statsData
            }
            else
            {
                Debug.LogError("�� ������� ��� ���������� StatUI!");
            }
        }

        UpdatePointsUI();
    }

    // ��������� ����� �����
    public void UpdatePointsUI()
    {
        if (pointsLeftText != null)
            pointsLeftText.text = $"����: {totalPoints}";
    }

    // ����� ��� ��������� �����
    public void ChangePoints(int amount)
    {
        totalPoints = Mathf.Clamp(totalPoints + amount, 0, 1000);
        UpdatePointsUI();
    }

    // ��������� ��� �������� � ���������� PlayerStats
    public void SaveStatsToPlayer()
    {
        foreach (var stat in statsData)
        {
            PlayerStats.Instance.ChangeStat(stat.name, stat.value);
        }

        Debug.Log("����� ����� ��������� � ����� ���������� ������");
    }

    // ����� ��� ������ "�������"
    public void OnAcceptButtonClicked()
    {
        SaveStatsToPlayer(); // ��������� ��������
        // ������� �� ��������� �����
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
