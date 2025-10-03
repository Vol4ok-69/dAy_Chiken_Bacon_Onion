using UnityEngine;
using TMPro;

public class AutoResumeSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ResumeData
    {
        public string post = "Название должности";
        public string desiredSalary = "50000 руб.";
        public string educationalInstitution = "НГТУ";
    }

    public GameObject resumePrefab;
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
        GameObject resumeInstance = Instantiate(resumePrefab, transform);
        resumeInstance.name = "MyResume";
        FillSpecificFields(resumeInstance);
    }

    private void FillSpecificFields(GameObject resumeInstance)
    {
        FillPost(resumeInstance);
        FillSalary(resumeInstance);
        FillEducationalInstitution(resumeInstance);
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

    private void FillEducationalInstitution(GameObject resumeInstance)
    {
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
}