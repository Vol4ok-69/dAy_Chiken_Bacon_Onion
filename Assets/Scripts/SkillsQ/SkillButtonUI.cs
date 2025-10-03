using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;
    public Button plusButton;

    private string statName;
    private int statValue;
    private bool isUpgradeable = true;

    public void Setup(string name, int value, bool canUpgrade = true)
    {
        statName = name;
        statValue = value;
        isUpgradeable = canUpgrade;

        if (nameText != null) nameText.text = statName;
        if (valueText != null) valueText.text = statValue.ToString();

        plusButton.onClick.RemoveAllListeners();

        if (isUpgradeable)
        {
            plusButton.interactable = true;
            plusButton.onClick.AddListener(OnPlusClicked);
        }
        else
        {
            plusButton.interactable = false;
        }
    }

    private void OnPlusClicked()
    {
        if (!isUpgradeable) return;

        if (PlayerStats.Instance.TryIncreaseStat(statName))
        {
            statValue++;
            valueText.text = statValue.ToString();
        }
        else
        {
            Debug.Log("Нет очков для прокачки!");
        }
    }
}
