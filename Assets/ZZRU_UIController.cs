using UnityEngine;

public class ZZRU_UIController : MonoBehaviour
{
    public void ClosePanel()
    {
        gameObject.SetActive(false); // ��� ��������� ���������� �����
        Debug.Log("ZZRU UI ������ �������");
    }
}
