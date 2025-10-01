using UnityEngine;
using UnityEngine.UI;

public class DiplomChoice : MonoBehaviour
{
    public static DiplomChoice Instance;
    public DiplomItem[] diplomaItems; // все дипломы на сцене
    public GameObject backButton;     // кнопка "Назад"

    private DiplomItem focusedDiploma;

    void Awake()
    {
        Instance = this;

        // Кнопка скрыта по умолчанию
        if (backButton != null)
            backButton.SetActive(false);
    }

    public void SelectDiploma(DiplomItem diploma)
    {
        // снимаем фокус с предыдущего
        if (focusedDiploma != null)
            focusedDiploma.Unfocus();

        focusedDiploma = diploma;
        focusedDiploma.Focus();

        // Скрываем все остальные дипломы
        foreach (var item in diplomaItems)
        {
            if (item != diploma)
                item.gameObject.SetActive(false);
        }

        // Показываем кнопку "Назад"
        if (backButton != null)
            backButton.SetActive(true);
    }

    public void ResetSelection()
    {
        // Снимаем фокус с выбранного
        if (focusedDiploma != null)
            focusedDiploma.Unfocus();

        // Показываем все дипломы
        foreach (var item in diplomaItems)
        {
            item.gameObject.SetActive(true);
        }

        focusedDiploma = null;

        // Скрываем кнопку "Назад"
        if (backButton != null)
            backButton.SetActive(false);

        // Можно скрыть UI с информацией о дипломе
        // DiplomaUI.Instance.HideInfo();
    }
}
