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
    public List<StatData> statsData;          // список статов для этой сцены
    public GameObject statPrefab;             // префаб UI для отдельной статы
    public Transform contentParent;           // родительский объект для UI
    public TextMeshProUGUI pointsLeftText;    // текст для оставшихся очков
    public int totalPoints = 20;              // доступные очки

    [Header("Позиционирование")]
    public float startY = 0f;
    public float spacing = -60f;

    void Start()
    {
        // Спавним UI для каждой статы
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
            {
                ui.Setup(stat.name, stat.value, this);
                ui.statData = stat; // связываем с элементом statsData
            }
            else
            {
                Debug.LogError("На префабе нет компонента StatUI!");
            }
        }

        UpdatePointsUI();
    }

    // Обновляем текст очков
    public void UpdatePointsUI()
    {
        if (pointsLeftText != null)
            pointsLeftText.text = $"Очки: {totalPoints}";
    }

    // Метод для изменения очков
    public void ChangePoints(int amount)
    {
        totalPoints = Mathf.Clamp(totalPoints + amount, 0, 1000);
        UpdatePointsUI();
    }

    // Сохраняем все значения в глобальный PlayerStats
    public void SaveStatsToPlayer()
    {
        foreach (var stat in statsData)
        {
            PlayerStats.Instance.ChangeStat(stat.name, stat.value);
        }

        Debug.Log("Статы сцены добавлены в общую статистику игрока");
    }

    // Метод для кнопки "Принять"
    public void OnAcceptButtonClicked()
    {
        SaveStatsToPlayer(); // сохраняем значения
        // Переход на следующую сцену
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
