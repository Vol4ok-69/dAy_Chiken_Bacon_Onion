using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // нужно для загрузки сцены

public class DiplomChoice : MonoBehaviour
{
    public static DiplomChoice Instance;
    public DiplomItem[] diplomaItems; // все дипломы на сцене
    public GameObject backButton;     // кнопка "Назад"
    public GameObject acceptButton;   // кнопка "Принять"

    private DiplomItem focusedDiploma;

    void Awake()
    {
        Instance = this;

        // Кнопки скрыты по умолчанию
        if (backButton != null)
            backButton.SetActive(false);
        if (acceptButton != null)
            acceptButton.SetActive(false);
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

        // Показываем кнопки
        if (backButton != null) backButton.SetActive(true);
        if (acceptButton != null) acceptButton.SetActive(true);
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

        // Скрываем кнопки
        if (backButton != null) backButton.SetActive(false);
        if (acceptButton != null) acceptButton.SetActive(false);
    }

    public void AcceptSelection()
    {
        if (focusedDiploma == null) return;

        Debug.Log("Принят диплом: " + focusedDiploma.diplomaName);

       // Вставь название сцены, которую хочешь загрузить
        SceneManager.LoadScene("SampleScene");
    }
}
