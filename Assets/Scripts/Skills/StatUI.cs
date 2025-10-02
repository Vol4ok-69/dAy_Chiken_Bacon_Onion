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
    public string statName;
    [HideInInspector]
    public int statValue;

    private StatsSpawner spawner;

    public void Setup(string name, int initialValue, StatsSpawner statsSpawner)
    {
        statName = name;
        statValue = initialValue;
        spawner = statsSpawner;

        if (nameText != null) nameText.text = statName;
        if (valueText != null) valueText.text = statValue.ToString();

        addButton.onClick.RemoveAllListeners();
        subtractButton.onClick.RemoveAllListeners();

        addButton.onClick.AddListener(AddPoint);
        subtractButton.onClick.AddListener(RemovePoint);
    }

    void AddPoint()
    {
        if (spawner.totalPoints <= 0) return;

        statValue++;
        spawner.ChangePoints(-1);
        UpdateUI();
    }

    void RemovePoint()
    {
        if (statValue <= 0) return;

        statValue--;
        spawner.ChangePoints(1);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (valueText != null)
            valueText.text = statValue.ToString();
    }
}
