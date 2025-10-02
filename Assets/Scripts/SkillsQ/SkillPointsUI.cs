using TMPro;
using UnityEngine;

public class SkillPointsUI : MonoBehaviour
{
    public TMP_Text pointsText;

    void Update()
    {
        if (PlayerStats.Instance != null && pointsText != null)
            pointsText.text = $"���� ��������: {PlayerStats.Instance.skillPoints}";
    }
}
