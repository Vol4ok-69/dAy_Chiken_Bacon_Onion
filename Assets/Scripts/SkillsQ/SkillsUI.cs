using System.Collections.Generic;
using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    [Header("UI ���������")]
    public GameObject panel;              // ������ ��� �������
    public Transform contentParent;       // �������� ��� ������ (������ ���� ������ Canvas)
    public GameObject skillButtonPrefab;  // ������ ������ ������ (Button + SkillButtonUI)

    [Header("�������� ������")]
    public float startY = 0f;
    public float spacing = -50f;

    private List<GameObject> spawnedButtons = new List<GameObject>();
    private bool isVisible = false;

    void Start()
    {
        if (panel == null) Debug.LogError("Panel �� ���������!");
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
        Debug.Log("=== ���������� UI ������� ===");
        Debug.Log("���������� ������ � �������: " + stats.Count);

        if (stats.Count == 0)
        {
            Debug.LogWarning("PlayerStats ������!");
            return;
        }

        int index = 0;
        foreach (var kvp in stats)
        {
            Debug.Log($"[��� 1] ������ ������ ��� {kvp.Key} = {kvp.Value}");

            if (skillButtonPrefab == null)
            {
                Debug.LogError("skillButtonPrefab �� ��������!");
                return;
            }

            if (contentParent == null)
            {
                Debug.LogError("contentParent �� ��������!");
                return;
            }

            GameObject go = Instantiate(skillButtonPrefab, contentParent);
            spawnedButtons.Add(go);
            Debug.Log($"[��� 2] ������ {skillButtonPrefab.name} ��������� -> {go.name}");

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition = new Vector2(0, startY + index * spacing);
                rt.localScale = Vector3.one;
                Debug.Log($"[��� 3] RectTransform ������. �������: {rt.anchoredPosition}");
            }
            else
            {
                Debug.LogError($"[������] � ������� {go.name} ��� RectTransform!");
            }

            SkillButtonUI ui = go.GetComponent<SkillButtonUI>();
            if (ui != null)
            {
                Debug.Log($"[��� 4] SkillButtonUI ������. �������� {kvp.Key} = {kvp.Value}");
                ui.Setup(kvp.Key, kvp.Value);
            }
            else
            {
                Debug.LogError($"[������] � ������� {go.name} ��� SkillButtonUI!");
            }

            index++;
        }

        Debug.Log("=== ����� ���������� UI ������� ===");
    }


    private void ClearButtons()
    {
        foreach (var btn in spawnedButtons)
            Destroy(btn);
        spawnedButtons.Clear();
    }
}
