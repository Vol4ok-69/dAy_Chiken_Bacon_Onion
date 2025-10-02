using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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
        SpawnFilteredVacancies(); // Создаем все вакансии при старте
    }

    // ФИЛЬТРАЦИЯ С ПОЛНЫМ ПЕРЕСОЗДАНИЕМ
    public void ApplyFilters()
    {
        SpawnFilteredVacancies();
    }

    void SpawnFilteredVacancies()
    {
        // УДАЛЯЕМ ВСЕ СТАРЫЕ КАРТОЧКИ
        foreach (Transform child in vacanciesContainer)
        {
            Destroy(child.gameObject);
        }

        // Получаем параметры фильтрации
        string selectedSpecialty = specialtyDropdown.options[specialtyDropdown.value].text;
        int salaryFrom = string.IsNullOrEmpty(salaryFromInput.text) ? 0 : int.Parse(salaryFromInput.text);
        int salaryTo = string.IsNullOrEmpty(salaryToInput.text) ? int.MaxValue : int.Parse(salaryToInput.text);
        string selectedExperience = experienceDropdown.options[experienceDropdown.value].text;

        // Настройки сетки
        float startX = 0f;
        float startY = 400f;
        float spacingX = 650f;
        float spacingY = -400f;
        int itemsPerRow = 2;

        int createdCount = 0;

        // СОЗДАЕМ ТОЛЬКО ОТФИЛЬТРОВАННЫЕ КАРТОЧКИ
        for (int i = 0; i < allVacancies.Count; i++)
        {
            VacancyData vacancyData = allVacancies[i];

            // Проверяем фильтры
            if (!CheckFilters(vacancyData, selectedSpecialty, salaryFrom, salaryTo, selectedExperience))
                continue;

            // СОЗДАЕМ НОВУЮ КАРТОЧКУ
            GameObject vacancyObj = Instantiate(vacancyPrefab, vacanciesContainer);
            
            // Позиционируем в сетку
            RectTransform rt = vacancyObj.GetComponent<RectTransform>();
            if (rt != null)
            {
                int row = createdCount / itemsPerRow;
                int col = createdCount % itemsPerRow;
                
                float posX = startX + (col * spacingX);
                float posY = startY + (row * spacingY);
                
                rt.anchoredPosition = new Vector2(posX, posY);
                rt.localScale = Vector3.one;
            }

            // Настраиваем UI
            SetupVacancyUI(vacancyObj, vacancyData);
            createdCount++;
        }
    }

    bool CheckFilters(VacancyData data, string specialty, int salaryFrom, int salaryTo, string experience)
    {
        // По специальности
        if (specialty != "Все специальности" && data.specialty != specialty)
            return false;

        // По зарплате
        if (data.salary < salaryFrom || data.salary > salaryTo)
            return false;

        // По опыту
        if (experience != "Любой опыт" && data.experience != experience)
            return false;

        return true;
    }

    void SetupVacancyUI(GameObject vacancyObj, VacancyData data)
    {
        // Находим текстовые элементы (подставь свои имена)
        TextMeshProUGUI titleText = vacancyObj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI specialtyText = vacancyObj.transform.Find("Specialty").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI salaryText = vacancyObj.transform.Find("Salary").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI experienceText = vacancyObj.transform.Find("Experience").GetComponent<TextMeshProUGUI>();

        // Заполняем данными
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
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Все специальности"));
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Программист"));
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Инженер"));
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Технолог"));
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Агроном"));
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Юрист"));
            specialtyDropdown.options.Add(new TMP_Dropdown.OptionData("Врач"));
        }
        if (experienceDropdown != null)
        {
            experienceDropdown.options.Clear();
            experienceDropdown.options.Add(new TMP_Dropdown.OptionData("Любой опыт"));
            experienceDropdown.options.Add(new TMP_Dropdown.OptionData("Без опыта"));
            experienceDropdown.options.Add(new TMP_Dropdown.OptionData("1-3 года"));
            experienceDropdown.options.Add(new TMP_Dropdown.OptionData("3-5 лет"));
            experienceDropdown.options.Add(new TMP_Dropdown.OptionData("5+ лет"));
        }
    }
}