using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobFilterManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Dropdown specialtyDropdown;
    public TMP_InputField salaryFromInput;
    public TMP_InputField salaryToInput;
    public TMP_Dropdown experienceDropdown;
    public Button applyFilterButton;

    [Header("Vacancies")]
    public GameObject vacancyPrefab;
    public Transform vacanciesContainer;
    public List<VacancyData> allVacancies = new List<VacancyData>();

    void Start()
    {
        if (vacanciesContainer == null)
        {
            vacanciesContainer = GameObject.Find("VacanciesContainer").transform;
        }
        FindTMPElements();
        if (applyFilterButton != null)
            applyFilterButton.onClick.AddListener(ApplyFilters);
        FillDropdowns();
        SpawnFilteredVacancies();
    }

    public void ApplyFilters()
    {
        SpawnFilteredVacancies();
    }

    void SpawnFilteredVacancies()
    {
        foreach (Transform child in vacanciesContainer)
        {
            Destroy(child.gameObject);
        }
        string selectedSpecialty = specialtyDropdown.options[specialtyDropdown.value].text;
        int salaryFrom = string.IsNullOrEmpty(salaryFromInput.text) ? 0 : int.Parse(salaryFromInput.text);
        int salaryTo = string.IsNullOrEmpty(salaryToInput.text) ? int.MaxValue : int.Parse(salaryToInput.text);
        string selectedExperience = experienceDropdown.options[experienceDropdown.value].text;

        float startX = 0f;
        float startY = 400f;
        float spacingX = 650f;
        float spacingY = -400f;
        int itemsPerRow = 2;

        int createdCount = 0;

        for (int i = 0; i < allVacancies.Count; i++)
        {
            VacancyData vacancyData = allVacancies[i];

            if (!CheckFilters(vacancyData, selectedSpecialty, salaryFrom, salaryTo, selectedExperience))
                continue;

            GameObject vacancyObj = Instantiate(vacancyPrefab, vacanciesContainer);

            if (vacancyObj.TryGetComponent<RectTransform>(out var rt))
            {
                int row = createdCount / itemsPerRow;
                int col = createdCount % itemsPerRow;

                float posX = startX + (col * spacingX);
                float posY = startY + (row * spacingY);

                rt.anchoredPosition = new Vector2(posX, posY);
                rt.localScale = Vector3.one;
            }

            SetupVacancyUI(vacancyObj, vacancyData);
            createdCount++;
        }
    }

    bool CheckFilters(VacancyData data, string specialty, int salaryFrom, int salaryTo, string experience)
    {
        if (specialty != "Все специальности" && data.specialty != specialty)
            return false;

        if (data.salary < salaryFrom || data.salary > salaryTo)
            return false;

        if (experience != "Любой опыт" && data.experience != experience)
            return false;

        return true;
    }

    void SetupVacancyUI(GameObject vacancyObj, VacancyData data)
    {
        TextMeshProUGUI titleText = vacancyObj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI specialtyText = vacancyObj.transform.Find("Specialty").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI salaryText = vacancyObj.transform.Find("Salary").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI experienceText = vacancyObj.transform.Find("Experience").GetComponent<TextMeshProUGUI>();

        titleText.text = data.title;
        specialtyText.text = data.specialty;
        salaryText.text = data.salary.ToString() + " руб.";
        experienceText.text = data.experience;
    }

    void FindTMPElements()
    {
        specialtyDropdown = GameObject.Find("SpecialtyDropdown").GetComponent<TMP_Dropdown>();
        salaryFromInput = GameObject.Find("SalaryFromInput").GetComponent<TMP_InputField>();
        salaryToInput = GameObject.Find("SalaryToInput").GetComponent<TMP_InputField>();
        experienceDropdown = GameObject.Find("ExperienceDropdown").GetComponent<TMP_Dropdown>();
        applyFilterButton = GameObject.Find("ApplyFilterButton").GetComponent<Button>();
    }

    void FillDropdowns()
    {
        if (specialtyDropdown != null)
        {
            specialtyDropdown.options.Clear();
            specialtyDropdown.options.AddRange(new List<TMP_Dropdown.OptionData>
            {
                new("Все специальности"),
                new("Программист"),
                new("Инженер"),
                new("Технолог"),
                new("Агроном"),
                new("Юрист"),
                new("Врач")
                });
        }
        if (experienceDropdown != null)
        {
            experienceDropdown.options.Clear();
            experienceDropdown.options.AddRange(new List<TMP_Dropdown.OptionData>
            {
                new("Любой опыт"),
                new("Без опыта"),
                new("1-3 годв"),
                new("3-5 лет"),
                new("5+ лет"),
            });
        }
    }
}