using UnityEngine;
using UnityEngine.SceneManagement;

public class InterviewInteraction : MonoBehaviour
{
    [Header("Требования для собеса")]
    public int requiredIntellect = 5;
    public int requiredCharisma = 5;

    [Header("Настройки сцен")]
    public string nextSceneName = "Interview";   // Сцена собеса
    public string failSceneName = "inhouse";     // Сцена при нехватке навыков

    // Вызывается при нажатии кнопки
    public void OnStartInterview()
    {
        int playerIntellect = PlayerStats.Instance.GetStat("Интеллект");
        int playerCharisma = PlayerStats.Instance.GetStat("Харизма");

        Debug.Log($"Проверка навыков: Интеллект={playerIntellect}, Харизма={playerCharisma}");

        if (playerIntellect >= requiredIntellect && playerCharisma >= requiredCharisma)
        {
            Debug.Log("Навыки подходят, переход на собес.");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Навыков не хватает, выкидываем на другую сцену.");
            SceneManager.LoadScene(failSceneName);
        }
    }
}
