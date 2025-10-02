using System.Collections.Generic;
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
    public Sprite icon;
    [TextArea(3, 6)] public string description;

    [Header("Стартовые параметры диплома")]
    public List<StatValue> startStats = new List<StatValue>();

    [Header("Анимация")]
    public Transform focusPoint;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isFocused = false;
    private float moveSpeed = 5f;
    private float scaleSpeed = 5f;

    [Header("Hover эффект")]
    public float hoverScaleFactor = 1.1f; // увеличение при наведении
    private bool isHovered = false;

    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Плавное приближение к focusPoint
        Vector3 targetPos = isFocused ? focusPoint.position : originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // Выбираем какую цель для масштаба применять
        Vector3 targetScale = originalScale;

        if (isFocused)
            targetScale = originalScale * 1.5f;          // выбранный диплом увеличен сильнее
        else if (isHovered)
            targetScale = originalScale * hoverScaleFactor; // наведение мыши = небольшой "пульс"

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);

        // Проверка клика через Physics2D.Raycast
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
        Debug.Log("Выбран диплом: " + diplomaName);
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

    // 🔹 Наведение мыши (для 2D объектов с коллайдером)
    void OnMouseEnter()
    {
        if (!isFocused)
        {
            isHovered = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // курсор "рука" можно заменить, если есть спрайт
        }
    }

    void OnMouseExit()
    {
        isHovered = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // возвращаем обычный курсор
    }
}
