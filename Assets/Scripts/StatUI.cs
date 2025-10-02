using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;
    public Button addButton;
    public Button subtractButton;

    [HideInInspector]
    public StatsSpawner.StatData statData; // ссылка на данные статы

    private StatsSpawner spawner;

    public void Setup(string statName, int initialValue, StatsSpawner statsSpawner)
    {
        statData = new StatsSpawner.StatData { name = statName, value = initialValue };
        spawner = statsSpawner;

        if (nameText != null) nameText.text = statName;
        if (valueText != null) valueText.text = initialValue.ToString();

        addButton.onClick.RemoveAllListeners();
        subtractButton.onClick.RemoveAllListeners();

        addButton.onClick.AddListener(AddPoint);
        subtractButton.onClick.AddListener(RemovePoint);
    }

    void AddPoint()
    {
        if (spawner.totalPoints <= 0) return;

        statData.value++;
        spawner.ChangePoints(-1);
        UpdateUI();
    }

    void RemovePoint()
    {
        if (statData.value <= 0) return;

        statData.value--;
        spawner.ChangePoints(1);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (valueText != null && statData != null)
            valueText.text = statData.value.ToString();
    }
}
