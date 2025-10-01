using UnityEngine;

public class DiplomItem : MonoBehaviour
{
    [Header("������ �������")]
    public string diplomaName;
    public Sprite icon;
    public string[] startingSkills;

    [Header("��������")]
    public Transform focusPoint;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isFocused = false;
    private float moveSpeed = 5f;
    private float scaleSpeed = 5f;

    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // ������� ����������� � focusPoint
        Vector3 targetPos = isFocused ? focusPoint.position : originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // ������� ���������������
        Vector3 targetScale = isFocused ? originalScale * 1.5f : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        // �������� ����� ����� Physics2D.Raycast
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                OnClick();
            }
        }
    }

    public void OnClick()
    {
        DiplomChoice.Instance.SelectDiploma(this);
        Debug.Log("������ ������: " + diplomaName);
        Focus();
    }

    public void Focus()
    {
        isFocused = true;
    }

    public void Unfocus()
    {
        isFocused = false;
    }
}
