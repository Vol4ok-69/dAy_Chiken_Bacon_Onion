using UnityEngine;
using TMPro;

public class AutoResumeSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ResumeData
    {
        public string post = "Название должности";
        public string desiredSalary = "50000 руб.";
        public string educationalInstitution = "НГТУ"; // Заменили на учебное заведение
    }

    public GameObject resumePrefab; // Перетащи сюда твой префаб
    public ResumeData resumeData = new ResumeData();

    void Start()
    {
        SpawnResume();
    }

    public void SpawnResume()
    {
        if (resumePrefab == null)
        {
            Debug.LogError("Префаб не назначен!");
            return;
        }

        // Создаем экземпляр префаба
        GameObject resumeInstance = Instantiate(resumePrefab, transform);
        resumeInstance.name = "MyResume";
        
        // Заполняем только нужные поля
        FillSpecificFields(resumeInstance);
        
        Debug.Log("Резюме автоматически создано при загрузке сцены!");
    }

    private void FillSpecificFields(GameObject resumeInstance)
    {
        FillPost(resumeInstance);
        FillSalary(resumeInstance);
        FillEducationalInstitution(resumeInstance); // Заменили метод
    }

    private void FillPost(GameObject resumeInstance)
    {
        Transform postTransform = FindDeepChild(resumeInstance.transform, "Post");
        if (postTransform != null)
        {
            TMP_Text postText = postTransform.GetComponent<TMP_Text>();
            if (postText != null)
            {
                postText.text = "Резюме на должность: "+resumeData.post;
            }
        }
    }

    private void FillSalary(GameObject resumeInstance)
    {
        Transform salaryTransform = FindDeepChild(resumeInstance.transform, "Salary");
        if (salaryTransform != null)
        {
            TMP_Text salaryText = salaryTransform.GetComponentInChildren<TMP_Text>();
            if (salaryText != null)
            {
                salaryText.text = resumeData.desiredSalary + " руб.";
            }
        }
    }

    private void FillEducationalInstitution(GameObject resumeInstance) // Новый метод
    {
        // Ищем объект EducationalInstitution в иерархии
        Transform eduTransform = FindDeepChild(resumeInstance.transform, "EducationalInstitution");
        if (eduTransform != null)
        {
            TMP_Text eduText = eduTransform.GetComponent<TMP_Text>();
            if (eduText != null)
            {
                eduText.text = "Название учебного заведения: "+resumeData.educationalInstitution;
            }
        }
        else
        {
            Debug.LogWarning("Не найден объект EducationalInstitution в префабе");
        }
    }

    private Transform FindDeepChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;
            
            Transform result = FindDeepChild(child, childName);
            if (result != null)
                return result;
        }
        return null;
    }

    // Метод для ручного обновления если нужно
    [ContextMenu("Обновить данные резюме")]
    public void UpdateResumeData()
    {
        GameObject resumeInstance = GameObject.Find("MyResume");
        if (resumeInstance != null)
        {
            FillSpecificFields(resumeInstance);
            Debug.Log("Данные резюме обновлены!");
        }
    }
}