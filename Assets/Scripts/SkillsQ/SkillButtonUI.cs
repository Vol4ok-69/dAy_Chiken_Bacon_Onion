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

    public void Setup(string name, int value)
    {
        statName = name;
        statValue = value;

        if (nameText != null) nameText.text = statName;
        if (valueText != null) valueText.text = statValue.ToString();

        plusButton.onClick.RemoveAllListeners();
        plusButton.onClick.AddListener(OnPlusClicked);
    }

    private void OnPlusClicked()
    {
        if (PlayerStats.Instance.TryIncreaseSkill(statName))
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
