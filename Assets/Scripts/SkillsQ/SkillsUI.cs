using System.Collections.Generic;
using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    [Header("UI Настройки")]
    public GameObject panel;              // Панель для навыков
    public Transform contentParent;       // Родитель для кнопок (должен быть внутри Canvas)
    public GameObject skillButtonPrefab;  // Префаб кнопки навыка (Button + SkillButtonUI)

    [Header("Смещение кнопок")]
    public float startY = 0f;
    public float spacing = -50f;

    private List<GameObject> spawnedButtons = new List<GameObject>();
    private bool isVisible = false;

    void Start()
    {
        if (panel == null) Debug.LogError("Panel не назначена!");
        if (contentParent == null) Debug.LogError("ContentParent не назначен!");
        if (skillButtonPrefab == null) Debug.LogError("SkillButtonPrefab не назначен!");

        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ToggleSkills();
    }

    public void ToggleSkills()
    {
        isVisible = !isVisible;
        panel.SetActive(isVisible);

        if (isVisible)
            UpdateSkillsUI();
        else
            ClearButtons();
    }

    private void UpdateSkillsUI()
    {
        ClearButtons();

        Dictionary<string, int> stats = PlayerStats.Instance.GetAllStats();
        Debug.Log("=== Обновление UI навыков ===");
        Debug.Log("Количество статов в словаре: " + stats.Count);

        if (stats.Count == 0)
        {
            Debug.LogWarning("PlayerStats пустой!");
            return;
        }

        int index = 0;
        foreach (var kvp in stats)
        {
            Debug.Log($"[ШАГ 1] Создаю кнопку для {kvp.Key} = {kvp.Value}");

            if (skillButtonPrefab == null)
            {
                Debug.LogError("skillButtonPrefab НЕ назначен!");
                return;
            }

            if (contentParent == null)
            {
                Debug.LogError("contentParent НЕ назначен!");
                return;
            }

            GameObject go = Instantiate(skillButtonPrefab, contentParent);
            spawnedButtons.Add(go);
            Debug.Log($"[ШАГ 2] Префаб {skillButtonPrefab.name} заспавнен -> {go.name}");

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = new Vector2(0, startY + index * spacing);
                rt.localScale = Vector3.one;
                Debug.Log($"[ШАГ 3] RectTransform найден. Позиция: {rt.anchoredPosition}");
            }
            else
            {
                Debug.LogError($"[ОШИБКА] У объекта {go.name} нет RectTransform!");
            }

            SkillButtonUI ui = go.GetComponent<SkillButtonUI>();
            if (ui != null)
            {
                Debug.Log($"[ШАГ 4] SkillButtonUI найден. Заполняю {kvp.Key} = {kvp.Value}");
                ui.Setup(kvp.Key, kvp.Value);
            }
            else
            {
                Debug.LogError($"[ОШИБКА] У объекта {go.name} нет SkillButtonUI!");
            }

            index++;
        }

        Debug.Log("=== Конец обновления UI навыков ===");
    }


    private void ClearButtons()
    {
        foreach (var btn in spawnedButtons)
            Destroy(btn);
        spawnedButtons.Clear();
    }
}
