using UnityEngine;
using TMPro;

public class ShelfInteraction : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text popupText;          // Текст для показа прироста
    public float popupDuration = 2f;    // Сколько секунд показывать
    public float jumpHeight = 30f;      // Высота подпрыгивания
    public float jumpFrequency = 4f;    // Скорость подпрыгивания

    [Header("Настройки навыков")]
    public string[] skillKeys = { "Коммуникабельность", "Интеллект", "Харизма" };
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
            int spent = 1; // сколько тратим за нажатие
            if (PlayerStats.Instance.TrySpendTimePoints(spent))
            {
                // Генерируем случайное увеличение навыков
                PlayerStats.Instance.ChangeStat("Коммуникабельность", Random.Range(1, 4));
                PlayerStats.Instance.ChangeStat("Интеллект", Random.Range(1, 4));
                PlayerStats.Instance.ChangeStat("Харизма", Random.Range(1, 4));
                Debug.Log("Очки потрачены, навыки увеличены!");
            }
            else
            {
                Debug.Log("Нет очков времени!");
            }
        }

        if (isPopupActive && popupText != null)
        {
            popupTimer -= Time.deltaTime;
            // Подпрыгивание через синус
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
            int points = Random.Range(minPoints, maxPoints + 1); // рандом 1–3
            PlayerStats.Instance.ChangeStat(key, points);
            logText += $"+{points} к {key}\n";
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
