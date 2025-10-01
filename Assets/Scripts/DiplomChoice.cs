using UnityEngine;
using UnityEngine.UI;

public class DiplomChoice : MonoBehaviour
{
    public static DiplomChoice Instance;
    public DiplomItem[] diplomaItems; // ��� ������� �� �����
    public GameObject backButton;     // ������ "�����"

    private DiplomItem focusedDiploma;

    void Awake()
    {
        Instance = this;

        // ������ ������ �� ���������
        if (backButton != null)
            backButton.SetActive(false);
    }

    public void SelectDiploma(DiplomItem diploma)
    {
        // ������� ����� � �����������
        if (focusedDiploma != null)
            focusedDiploma.Unfocus();

        focusedDiploma = diploma;
        focusedDiploma.Focus();

        // �������� ��� ��������� �������
        foreach (var item in diplomaItems)
        {
            if (item != diploma)
                item.gameObject.SetActive(false);
        }

        // ���������� ������ "�����"
        if (backButton != null)
            backButton.SetActive(true);
    }

    public void ResetSelection()
    {
        // ������� ����� � ����������
        if (focusedDiploma != null)
            focusedDiploma.Unfocus();

        // ���������� ��� �������
        foreach (var item in diplomaItems)
        {
            item.gameObject.SetActive(true);
        }

        focusedDiploma = null;

        // �������� ������ "�����"
        if (backButton != null)
            backButton.SetActive(false);

        // ����� ������ UI � ����������� � �������
        // DiplomaUI.Instance.HideInfo();
    }
}
