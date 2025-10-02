using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Лампочка над игроком")]
    public GameObject lampIcon; // лампочка над игроком

    [Header("UI панели (только для ZZRU)")]
    public GameObject uiPanelZZRU; // UI объект для ZZRU

    private string currentScene = ""; // текущая зона/сцена
    private bool canUseE = false;    // доступность клавиши E

    private void Start()
    {
        if (lampIcon != null)
            lampIcon.SetActive(false);

        if (uiPanelZZRU != null)
            uiPanelZZRU.SetActive(false); // скрываем UI по умолчанию
    }

    /// <summary>
    /// Игрок входит в зону
    /// </summary>
    public void EnterZone(string zoneName)
    {
        currentScene = zoneName;

        if (lampIcon != null)
            lampIcon.SetActive(true); // включаем лампочку

        canUseE = true;
        Debug.Log("E доступна для зоны: " + zoneName);
    }

    /// <summary>
    /// Игрок выходит из зоны
    /// </summary>
    public void ExitZone(string zoneName)
    {
        if (currentScene == zoneName)
            currentScene = "";

        if (lampIcon != null)
            lampIcon.SetActive(false); // выключаем лампочку

        canUseE = false;
        Debug.Log("E больше не доступна");
    }

    private void Update()
    {
        if (canUseE && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(currentScene))
            {
                // Если зона ZZRU — переключаем UI
                if (currentScene == "ZZRU" && uiPanelZZRU != null)
                {
                    bool isActive = uiPanelZZRU.activeSelf;
                    uiPanelZZRU.SetActive(!isActive);
                    Debug.Log("UI ZZRU " + (isActive ? "скрыт" : "показан"));
                }
                else
                {
                    // Для остальных зон загружаем сцену
                    SceneManager.LoadScene(currentScene);
                }
            }
        }
    }

    /// <summary>
    /// Метод для кнопки UI, чтобы закрыть ZZRU (можно повесить на кнопку)
    /// </summary>
    public void CloseZZRU()
    {
        if (uiPanelZZRU != null)
        {
            uiPanelZZRU.SetActive(false);
            Debug.Log("UI ZZRU закрыт кнопкой");
        }
    }
}
