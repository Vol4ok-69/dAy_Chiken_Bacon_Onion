using UnityEngine;

public class DiplomItem : MonoBehaviour
{
    [Header("Данные диплома")]
    public string diplomaName;
    public Sprite icon;
    [TextArea(3, 6)] public string description; // 🟢 описание диплома


    [Header("Анимация")]
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
        // Плавное приближение к focusPoint
        Vector3 targetPos = isFocused ? focusPoint.position : originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        // Плавное масштабирование
        Vector3 targetScale = isFocused ? originalScale * 1.5f : originalScale;
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
}
