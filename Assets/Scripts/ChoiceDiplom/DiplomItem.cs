using UnityEngine;

[System.Serializable]
public class StatValue
{
    public string statName;
    public int value;
}

public class DiplomItem : MonoBehaviour
{
    [Header("Данные диплома")]
    public string diplomaName;
    public string description;

    [Header("Стартовые статы диплома")]
    public StatValue[] startStats;

    [Header("Анимация")]
    public Transform focusPoint;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isFocused = false;
    private bool isHovered = false;
    private float moveSpeed = 5f;
    private float scaleSpeed = 5f;

    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 targetPos = isFocused ? focusPoint.position : originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        Vector3 targetScale;
        if (isFocused)
            targetScale = originalScale * 1.5f;
        else if (isHovered)
            targetScale = originalScale * 1.2f;
        else
            targetScale = originalScale;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

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

    private void OnMouseEnter()
    {
        if (!isFocused)
            isHovered = true;
    }

    private void OnMouseExit()
    {
        if (!isFocused)
            isHovered = false;
    }

    public void OnClick()
    {
        DiplomChoice.Instance.SelectDiploma(this);
        Focus();
    }

    public void Focus()
    {
        isFocused = true;
    }

    public void Unfocus()
    {
        isFocused = false;
        isHovered = false;
    }
}
