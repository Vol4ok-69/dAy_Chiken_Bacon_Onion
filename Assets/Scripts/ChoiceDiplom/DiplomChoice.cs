using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiplomChoice : MonoBehaviour
{
    public static DiplomChoice Instance;
    public DiplomItem[] diplomaItems;

    [Header("UI кнопки")]
    public GameObject backButton;
    public GameObject acceptButton;

    [Header("UI текст")]
    public TMP_Text diplomaDesc;   // 🟢 сюда будем выводить описание

    private DiplomItem focusedDiploma;

    void Awake()
    {
        Instance = this;

        if (backButton != null) backButton.SetActive(false);
        if (acceptButton != null) acceptButton.SetActive(false);

        if (diplomaDesc != null) diplomaDesc.text = "";
    }

    public void SelectDiploma(DiplomItem diploma)
    {
        if (focusedDiploma != null)
            focusedDiploma.Unfocus();

        focusedDiploma = diploma;
        focusedDiploma.Focus();

        foreach (var item in diplomaItems)
        {
            if (item != diploma)
                item.gameObject.SetActive(false);
        }

        if (backButton != null) backButton.SetActive(true);
        if (acceptButton != null) acceptButton.SetActive(true);

        // 🟢 выводим описание в текстовый UI
        if (diplomaDesc != null) diplomaDesc.text = diploma.description;
    }

    public void ResetSelection()
    {
        if (focusedDiploma != null)
            focusedDiploma.Unfocus();

        foreach (var item in diplomaItems)
        {
            item.gameObject.SetActive(true);
        }

        focusedDiploma = null;

        if (backButton != null) backButton.SetActive(false);
        if (acceptButton != null) acceptButton.SetActive(false);

        if (diplomaDesc != null) diplomaDesc.text = ""; // очищаем текст
    }
    public void AcceptSelection()
    {
        if (focusedDiploma != null)
        {
            Dictionary<string, int> diplomaStats = new Dictionary<string, int>();

            foreach (var stat in focusedDiploma.startStats)
            {
                diplomaStats[stat.statName] = stat.value;
            }

            PlayerStats.Instance.SetStats(diplomaStats);

            Debug.Log("Выбран диплом: " + focusedDiploma.diplomaName);
            foreach (var kvp in diplomaStats)
            {
                Debug.Log(kvp.Key + " = " + kvp.Value);
            }
            SceneManager.LoadScene("SkillStart");
        }
    }
}

