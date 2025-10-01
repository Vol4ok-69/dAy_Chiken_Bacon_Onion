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
        // ��������� ������
        if (skillButtonPrefab == null || positivePanel == null || negativePanel == null || pointsText == null)
        {
            Debug.LogError("SkillSelectionManager: �� ��� ���� ���������!");
            return;
        }

        // ������ �������
        var positiveSkillsData = new Dictionary<string, string>
        {
            {"������������������", "����� �������� ����� ���� � ��������� � �����������."},
            {"��������� ��������", "������� ����� ����������� �������������."},
            {"�������������������", "������ ���������, ������ ������ ��������."},
            {"������� �����������", "������� �������� ����� ������."},
            {"����������������", "���� ��������������, ������ ������� �� ��������."},
            {"���������������", "������ �������, ���� ���� ������� ������."},
            {"����������� ������", "�������� �������� �� �����."},
            {"����������� ����", "��������� ���� ��������."},
            {"������������", "�������� ����� � ���������� ����������."},
            {"�������", "����� �������� �������������."}
        };

        var negativeSkillsData = new Dictionary<string, string>
        {
            {"����", "������ ��������������, ������ ����������� ������."},
            {"��������������", "���� ��������� �������."},
            {"�������������", "���� ������ � ���������."},
            {"������ �������������������", "������� ���������."},
            {"������ ������������", "���� ����������� ������."},
            {"���������� �����", "������ ��������� ��������."},
            {"����������� �� ��������", "����� ������ �� �������."},
            {"������������� � ����", "���� ���������� �� ��������������."},
            {"����������� ���������", "����� � ���������� � �����������."},
            {"���������� ����������� �����������", "������� ���������� � ������� ��������."}
        };

        GenerateSkills(positiveSkillsData, positivePanel, -2); // ������������� ������ ��������� ����
        GenerateSkills(negativeSkillsData, negativePanel, +4); // ������������� ������ ���� ����

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
            pointsText.text = "����: " + points;
    }
}
