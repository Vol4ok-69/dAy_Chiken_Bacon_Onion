using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("�������� ��� �������")]
    public GameObject lampIcon; // �������� ��� �������

    [Header("UI ������ (������ ��� ZZRU)")]
    public GameObject uiPanelZZRU; // UI ������ ��� ZZRU

    private string currentScene = ""; // ������� ����/�����
    private bool canUseE = false;    // ����������� ������� E

    private void Start()
    {
        if (lampIcon != null)
            lampIcon.SetActive(false);

        if (uiPanelZZRU != null)
            uiPanelZZRU.SetActive(false); // �������� UI �� ���������
    }

    /// <summary>
    /// ����� ������ � ����
    /// </summary>
    public void EnterZone(string zoneName)
    {
        currentScene = zoneName;

        if (lampIcon != null)
            lampIcon.SetActive(true); // �������� ��������

        canUseE = true;
        Debug.Log("E �������� ��� ����: " + zoneName);
    }

    /// <summary>
    /// ����� ������� �� ����
    /// </summary>
    public void ExitZone(string zoneName)
    {
        if (currentScene == zoneName)
            currentScene = "";

        if (lampIcon != null)
            lampIcon.SetActive(false); // ��������� ��������

        canUseE = false;
        Debug.Log("E ������ �� ��������");
    }

    private void Update()
    {
        if (canUseE && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(currentScene))
            {
                // ���� ���� ZZRU � ����������� UI
                if (currentScene == "ZZRU" && uiPanelZZRU != null)
                {
                    bool isActive = uiPanelZZRU.activeSelf;
                    uiPanelZZRU.SetActive(!isActive);
                    Debug.Log("UI ZZRU " + (isActive ? "�����" : "�������"));
                }
                else
                {
                    // ��� ��������� ��� ��������� �����
                    SceneManager.LoadScene(currentScene);
                }
            }
        }
    }

    /// <summary>
    /// ����� ��� ������ UI, ����� ������� ZZRU (����� �������� �� ������)
    /// </summary>
    public void CloseZZRU()
    {
        if (uiPanelZZRU != null)
        {
            uiPanelZZRU.SetActive(false);
            Debug.Log("UI ZZRU ������ �������");
        }
    }
}
