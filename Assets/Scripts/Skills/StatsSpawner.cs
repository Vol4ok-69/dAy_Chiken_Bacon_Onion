using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsSpawner : MonoBehaviour
{
    [System.Serializable]
    public class StatData
    {
        public string name;
        public int value;
    }

    [Header("Настройки")]
    public List<StatData> statsData;
    public GameObject statPrefab; 
    public Transform contentParent;         
    public TextMeshProUGUI pointsLeftText;    
    public int totalPoints = 20;             

    [Header("Позиционирование")]
    public float startY = 0f;
    public float spacing = -60f;

    private StatUI[] spawnedUI;

    void Start()
    {
        spawnedUI = new StatUI[statsData.Count];

        for (int i = 0; i < statsData.Count; i++)
        {
            StatData stat = statsData[i];
            GameObject go = Instantiate(statPrefab, contentParent);
            go.name = $"{statPrefab.name}_{i}";

            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            rt.anchoredPosition = new Vector2(0, startY + i * spacing);

            StatUI ui = go.GetComponent<StatUI>();
            if (ui != null)
            {
                ui.Setup(stat.name, stat.value, this);
                spawnedUI[i] = ui; 
            }
            else
            {
                Debug.LogError("На префабе нет компонента StatUI!");
            }
        }

        UpdatePointsUI();
    }

    public void UpdatePointsUI()
    {
        if (pointsLeftText != null)
            pointsLeftText.text = $"Очки: {totalPoints}";
    }

    public void ChangePoints(int amount)
    {
        totalPoints = Mathf.Clamp(totalPoints + amount, 0, 1000);
        UpdatePointsUI();
    }

    public void OnAcceptButtonClicked()
    {
        Dictionary<string, int> newStats = new();

        foreach (var ui in spawnedUI)
        {
            if (ui != null)
            {
                newStats[ui.statName] = ui.statValue;
            }
        }
        PlayerStats.Instance.AddOrUpdateStats(newStats);

        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
