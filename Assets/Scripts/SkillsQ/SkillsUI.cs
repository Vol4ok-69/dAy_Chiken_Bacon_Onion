using System.Collections.Generic;
using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    [Header("UI ���������")]
    public GameObject panel;             
    public RectTransform contentParent;   
    public GameObject skillButtonPrefab;  

    [Header("�������� ������")]
    public float startY = 0f;
    public float spacing = -50f;

    private List<GameObject> spawnedButtons = new List<GameObject>();
    private bool isVisible = false;

    void Start()
    {
        if (panel == null) Debug.LogError("Panel �� ��������� � SkillsUI!");
        if (contentParent == null) Debug.LogError("ContentParent �� ��������!");
        if (skillButtonPrefab == null) Debug.LogError("SkillButtonPrefab �� ��������!");

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
        if (stats.Count == 0) return;

        int index = 0;
        foreach (var kvp in stats)
        {
            GameObject go = Instantiate(skillButtonPrefab, contentParent);
            spawnedButtons.Add(go);

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = new Vector2(0, startY + index * spacing);
                rt.localScale = Vector3.one;
            }

            SkillButtonUI ui = go.GetComponent<SkillButtonUI>();
            if (ui != null)
            {
                bool canUpgrade = index >= 2;
                ui.Setup(kvp.Key, kvp.Value, canUpgrade);
            }
            else
            {
                Debug.LogError("�� ������� ������ ��� SkillButtonUI!");
            }

            index++;
        }
    }

    private void ClearButtons()
    {
        foreach (var btn in spawnedButtons)
            Destroy(btn);
        spawnedButtons.Clear();
    }
}
