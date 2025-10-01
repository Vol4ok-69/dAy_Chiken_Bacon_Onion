using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text costText;
    public Button button;

    private SkillSO skill;
    private SkillSelectionManager manager;

    public void Init(SkillSO skill, SkillSelectionManager manager)
    {
        this.skill = skill;
        this.manager = manager;

        // Проверяем, что все поля назначены
        if (nameText != null) nameText.text = skill.skillName;
        if (costText != null) costText.text = skill.pointDelta >= 0 ? "+" + skill.pointDelta : skill.pointDelta.ToString();
        if (icon != null) icon.sprite = skill.icon;

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => manager.ToggleSkill(skill));
        }
    }
}
