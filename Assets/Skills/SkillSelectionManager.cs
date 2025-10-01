using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillSelectionManager : MonoBehaviour
{
    public Transform positivePanel;
    public Transform negativePanel;
    public TMP_Text pointsText;
    public GameObject skillButtonPrefab;

    private List<SkillSO> selected = new List<SkillSO>();
    private int points = 0;

    void Start()
    {
        // Проверяем ссылки
        if (skillButtonPrefab == null || positivePanel == null || negativePanel == null || pointsText == null)
        {
            Debug.LogError("SkillSelectionManager: Не все поля назначены!");
            return;
        }

        // Данные навыков
        var positiveSkillsData = new Dictionary<string, string>
        {
            {"Коммуникабельность", "Легче находить общий язык с коллегами и начальством."},
            {"Лидерские качества", "Быстрее можно становиться руководителем."},
            {"Стрессоустойчивость", "Меньше выгорания, дольше можешь работать."},
            {"Быстрая обучаемость", "Быстрее качаются новые навыки."},
            {"Организованность", "Выше продуктивность, меньше штрафов за дедлайны."},
            {"Ответственность", "Больше доверия, чаще дают сложные задачи."},
            {"Технические навыки", "Повышают ценность на рынке."},
            {"Иностранный язык", "Расширяет круг вакансий."},
            {"Креативность", "Повышает шансы в творческих профессиях."},
            {"Харизма", "Легче проходит собеседования."}
        };

        var negativeSkillsData = new Dictionary<string, string>
        {
            {"Лень", "Меньше продуктивность, дольше выполняются задачи."},
            {"Прокрастинация", "Шанс провалить дедлайн."},
            {"Конфликтность", "Хуже ладишь с коллегами."},
            {"Низкая стрессоустойчивость", "Быстрее выгораешь."},
            {"Плохая концентрация", "Чаще совершаются ошибки."},
            {"Отсутствие опыта", "Меньше стартовых вакансий."},
            {"Зависимость от соцсетей", "Время уходит “в никуда”."},
            {"Неуверенность в себе", "Хуже результаты на собеседованиях."},
            {"Хронические опоздания", "Штраф к отношениям с начальством."},
            {"Отсутствие профильного образования", "Сложнее устроиться в крупные компании."}
        };

        GenerateSkills(positiveSkillsData, positivePanel, -2); // Положительные навыки уменьшают очки
        GenerateSkills(negativeSkillsData, negativePanel, +4); // Отрицательные навыки дают очки

        UpdateUI();
    }

    void GenerateSkills(Dictionary<string, string> skillsData, Transform parent, int pointDelta)
    {
        foreach (var kvp in skillsData)
        {
            SkillSO skill = ScriptableObject.CreateInstance<SkillSO>();
            skill.skillName = kvp.Key;
            skill.description = kvp.Value;
            skill.pointDelta = pointDelta;
            skill.id = kvp.Key;

            var obj = Instantiate(skillButtonPrefab, parent);
            var ui = obj.GetComponent<SkillButtonUI>();
            if (ui != null)
                ui.Init(skill, this);
        }
    }

    public void ToggleSkill(SkillSO skill)
    {
        if (selected.Contains(skill))
        {
            selected.Remove(skill);
            points -= skill.pointDelta;
        }
        else
        {
            selected.Add(skill);
            points += skill.pointDelta;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (pointsText != null)
            pointsText.text = "Очки: " + points;
    }
}
