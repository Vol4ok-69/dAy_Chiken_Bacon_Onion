using UnityEngine;

public class Instruction : MonoBehaviour
{
    public GameObject tutorialPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
