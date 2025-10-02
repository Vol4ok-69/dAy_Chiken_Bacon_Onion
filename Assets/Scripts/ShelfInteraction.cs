using UnityEngine;
using TMPro;

public class ShelfInteraction : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text popupText;          // ����� ��� ������ ��������
    public float popupDuration = 2f;    // ������� ������ ����������
    public float jumpHeight = 30f;      // ������ �������������
    public float jumpFrequency = 4f;    // �������� �������������

    [Header("��������� �������")]
    public string[] skillKeys = { "������������������", "���������", "�������" };
    public int minPoints = 1;
    public int maxPoints = 3;

    private bool isPopupActive = false;
    private float popupTimer = 0f;
    private Vector3 popupStartPos;


    void Start()
    {
        if (popupText != null)
        {
            popupText.gameObject.SetActive(false);
            popupStartPos = popupText.transform.position;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            int spent = 1; // ������� ������ �� �������
            if (PlayerStats.Instance.TrySpendTimePoints(spent))
            {
                // ���������� ��������� ���������� �������
                PlayerStats.Instance.ChangeStat("������������������", Random.Range(1, 4));
                PlayerStats.Instance.ChangeStat("���������", Random.Range(1, 4));
                PlayerStats.Instance.ChangeStat("�������", Random.Range(1, 4));
                Debug.Log("���� ���������, ������ ���������!");
            }
            else
            {
                Debug.Log("��� ����� �������!");
            }
        }

        if (isPopupActive && popupText != null)
        {
            popupTimer -= Time.deltaTime;
            // ������������� ����� �����
            float progress = 1f - (popupTimer / popupDuration);
            float offsetY = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
            popupText.transform.position = popupStartPos + Vector3.up * offsetY;

            if (popupTimer <= 0f)
            {
                popupText.gameObject.SetActive(false);
                popupText.transform.position = popupStartPos;
                isPopupActive = false;
            }
        }
    }

    void GiveRandomSkills()
    {
        string logText = "";
        foreach (var key in skillKeys)
        {
            int points = Random.Range(minPoints, maxPoints + 1); // ������ 1�3
            PlayerStats.Instance.ChangeStat(key, points);
            logText += $"+{points} � {key}\n";
        }

        Debug.Log(logText);

        if (popupText != null)
        {
            popupText.text = logText;
            popupText.gameObject.SetActive(true);
            popupTimer = popupDuration;
            isPopupActive = true;
        }
    }
}
