using UnityEngine;
using UnityEngine.SceneManagement;

public class InterviewInteraction : MonoBehaviour
{
    [Header("���������� ��� ������")]
    public int requiredIntellect = 5;
    public int requiredCharisma = 5;

    [Header("��������� ����")]
    public string nextSceneName = "Interview";   // ����� ������
    public string failSceneName = "inhouse";     // ����� ��� �������� �������

    // ���������� ��� ������� ������
    public void OnStartInterview()
    {
        int playerIntellect = PlayerStats.Instance.GetStat("���������");
        int playerCharisma = PlayerStats.Instance.GetStat("�������");

        Debug.Log($"�������� �������: ���������={playerIntellect}, �������={playerCharisma}");

        if (playerIntellect >= requiredIntellect && playerCharisma >= requiredCharisma)
        {
            Debug.Log("������ ��������, ������� �� �����.");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("������� �� �������, ���������� �� ������ �����.");
            SceneManager.LoadScene(failSceneName);
        }
    }
}
