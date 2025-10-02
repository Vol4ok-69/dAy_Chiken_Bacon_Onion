using System.Text;
using UnityEngine;
using TMPro;

public class ShowAllPlayerStats : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text statsText;   // текст для вывода
    public GameObject panel;     // панель, которую можно скрывать/показывать

    void Start()
    {
        if (panel != null)
            panel.SetActive(false); // скрываем панель по умолчанию
    }

    // Вызывается кнопкой через OnClick
    public void ShowStats()
    {
        if (panel != null)
            panel.SetActive(true);

        if (statsText == null) return;

        // Получаем все актуальные статы из PlayerStats
        var allStats = PlayerStats.Instance.GetAllStats();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Текущие статы и навыки:");

        if (allStats.Count == 0)
        {
            sb.AppendLine("Нет доступных статов.");
        }
        else
        {
            foreach (var kvp in allStats)
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        statsText.text = sb.ToString();
    }

    // Дополнительно: скрыть панель
    public void HideStats()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}
