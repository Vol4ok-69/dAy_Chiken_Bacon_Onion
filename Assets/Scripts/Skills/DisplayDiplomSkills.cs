using System.Text;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TMP_Text statsText;

    void Start()
    {
        if (statsText == null)
        {
            Debug.LogError("StatsText не назначен!");
            return;
        }

        var stats = PlayerStats.Instance.GetAllStats();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Стартовые значения:");

        foreach (var kvp in stats)
        {
            sb.AppendLine(kvp.Key + ": " + kvp.Value);
        }

        statsText.text = sb.ToString();
    }
}
