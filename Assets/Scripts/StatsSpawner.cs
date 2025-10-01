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

    [Header("Настройки")]
    public List<StatData> statsData;
    public GameObject statPrefab;
    public Transform contentParent;
    public TextMeshProUGUI pointsLeftText;
    public int totalPoints = 20;

    [Header("Позиционирование")]
    public float startY = 0f;
    public float spacing = -60f;

    void Start()
    {
        for (int i = 0; i < statsData.Count; i++)
        {
            StatData stat = statsData[i];
            GameObject go = Instantiate(statPrefab, contentParent);
            go.name = $"{statPrefab.name}_{i}";

            // Сброс позиции и масштаб
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;
            rt.anchoredPosition = new Vector2(0, startY + i * spacing);

            // Настройка StatUI
            StatUI ui = go.GetComponent<StatUI>();
            if (ui != null)
                ui.Setup(stat.name, stat.value, this);
            else
                Debug.LogError("На префабе нет компонента StatUI!");
        }

        UpdatePointsUI();
    }

    public void UpdatePointsUI()
    {
        if (pointsLeftText != null)
            pointsLeftText.text = $"Очки: {totalPoints}";
    }
}
