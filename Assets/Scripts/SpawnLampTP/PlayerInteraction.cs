using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject lampIcon; // Лампочка над игроком
    private List<string> zoneStack = new List<string>(); // зоны, в которых игрок находится

    void Start()
    {
        if (lampIcon != null)
            lampIcon.SetActive(false); // выключаем лампу по умолчанию
    }

    void Update()
    {
        if (zoneStack.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            string scene = zoneStack[zoneStack.Count - 1];
            if (!string.IsNullOrEmpty(scene))
                SceneManager.LoadScene(scene);
        }
    }

    public void EnterZone(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;
        zoneStack.Add(sceneName);
        UpdateLamp();
    }

    public void ExitZone(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return;

        for (int i = zoneStack.Count - 1; i >= 0; i--)
        {
            if (zoneStack[i] == sceneName)
            {
                zoneStack.RemoveAt(i);
                break;
            }
        }
        UpdateLamp();
    }

    private void UpdateLamp()
    {
        bool active = zoneStack.Count > 0;
        if (lampIcon != null)
            lampIcon.SetActive(active);
    }
}
