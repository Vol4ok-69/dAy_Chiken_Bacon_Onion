using UnityEngine;

public class ZZRU_UIController : MonoBehaviour
{
    public void ClosePanel()
    {
        gameObject.SetActive(false); // или загружать предыдущую сцену
        Debug.Log("ZZRU UI закрыт кнопкой");
    }
}
