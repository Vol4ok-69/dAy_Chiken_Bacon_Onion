using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Лампочка над игроком")]
    public GameObject lampIcon;

    [Header("UI панели (только для ZZRU)")]
    public GameObject uiPanelZZRU;

    private string currentScene = ""; 
    private bool canUseE = false;   

    private void Start()
    {
        if (lampIcon != null)
            lampIcon.SetActive(false);

        if (uiPanelZZRU != null)
            uiPanelZZRU.SetActive(false); 
    }

    /// <summary>
    /// Игрок входит в зону
    /// </summary>
    public void EnterZone(string zoneName)
    {
        currentScene = zoneName;

        if (lampIcon != null)
            lampIcon.SetActive(true);

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
            lampIcon.SetActive(false);

        canUseE = false;
        Debug.Log("E больше не доступна");
    }

    private void Update()
    {
        if (canUseE && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(currentScene))
            {
                if (currentScene == "ZZRU" && uiPanelZZRU != null)
                {
                    bool isActive = uiPanelZZRU.activeSelf;
                    uiPanelZZRU.SetActive(!isActive);
                    Debug.Log("UI ZZRU " + (isActive ? "скрыт" : "показан"));
                }
                else
                {
                    SceneManager.LoadScene(currentScene);
                }
            }
        }
    }

    /// <summary>
    /// Метод для кнопки UI, чтобы закрыть ZZRU
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
