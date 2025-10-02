using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("�������� ��� �������")]
    public GameObject lampIcon; // ������ �� ������ ��������

    private string currentScene = ""; // �������� ����� ��� ��������

    private void Start()
    {
        if (lampIcon != null)
            lampIcon.SetActive(false);
    }

    /// <summary>
    /// ����� ������ � ����
    /// </summary>
    public void EnterZone(string sceneName)
    {
        currentScene = sceneName;

        if (lampIcon != null)
            lampIcon.SetActive(true); // �������� ��������

        Debug.Log("������� E �������� ��� �����: " + currentScene);
    }

    /// <summary>
    /// ����� ������� �� ����
    /// </summary>
    public void ExitZone(string sceneName)
    {
        if (currentScene == sceneName)
        {
            currentScene = "";
            Debug.Log("������� E ������ �� ��������");
        }

        if (lampIcon != null)
            lampIcon.SetActive(false); // ��������� ��������
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(currentScene))
        {
            // ��� ��� �������� ����������� ������� E
            Debug.Log("E �������� ��� �����: " + currentScene);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("������ E, ��������� �����: " + currentScene);
                SceneManager.LoadScene(currentScene);
            }
        }
    }
}
