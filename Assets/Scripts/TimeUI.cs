using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Update()
    {
        if (timeText != null && PlayerStats.Instance != null)
        {
            timeText.text = $"Часы: {PlayerStats.Instance.GetStat("TimePoints")}";
        }
    }
}
