using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;
    public Button addButton;
    public Button subtractButton;

    private int statValue;
    private StatsSpawner spawner;

    public void Setup(string statName, int initialValue, StatsSpawner statsSpawner)
    {
        statValue = initialValue;
        spawner = statsSpawner;
        nameText.text = statName;
        valueText.text = statValue.ToString();

        addButton.onClick.RemoveAllListeners();
        subtractButton.onClick.RemoveAllListeners();

        addButton.onClick.AddListener(AddPoint);
        subtractButton.onClick.AddListener(RemovePoint);
    }

    void AddPoint()
    {
        if (spawner.totalPoints <= 0) return;
        statValue++;
        spawner.totalPoints--;
        valueText.text = statValue.ToString();
        spawner.UpdatePointsUI();
    }

    void RemovePoint()
    {
        if (statValue <= 0) return;
        statValue--;
        spawner.totalPoints++;
        valueText.text = statValue.ToString();
        spawner.UpdatePointsUI();
    }
}
