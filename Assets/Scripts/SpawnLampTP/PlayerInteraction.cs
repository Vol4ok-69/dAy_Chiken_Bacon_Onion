using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Лампочка над игроком")]
    public GameObject lampIcon; // ссылка на объект лампочки

    private string currentScene = ""; // активная сцена для перехода

    private void Start()
    {
        if (lampIcon != null)
            lampIcon.SetActive(false);
    }

    /// <summary>
    /// Игрок входит в зону
    /// </summary>
    public void EnterZone(string sceneName)
    {
        currentScene = sceneName;

        if (lampIcon != null)
            lampIcon.SetActive(true); // включаем лампочку

        Debug.Log("Клавиша E доступна для сцены: " + currentScene);
    }

    /// <summary>
    /// Игрок выходит из зоны
    /// </summary>
    public void ExitZone(string sceneName)
    {
        if (currentScene == sceneName)
        {
            currentScene = "";
            Debug.Log("Клавиша E больше не доступна");
        }

        if (lampIcon != null)
            lampIcon.SetActive(false); // выключаем лампочку
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(currentScene))
        {
            // Лог для проверки доступности клавиши E
            Debug.Log("E доступна для сцены: " + currentScene);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Нажата E, загружаем сцену: " + currentScene);
                SceneManager.LoadScene(currentScene);
            }
        }
    }
}
