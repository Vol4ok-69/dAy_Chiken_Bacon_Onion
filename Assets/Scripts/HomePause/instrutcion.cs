using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class instrutcion : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject Panel;
    public GameObject ConfirmPanel;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        Panel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        Panel.SetActive(false);
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false);
        isPaused = false;
    }
    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
        isPaused = true;
    }
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        isPaused = false;
    }

    public void OpenConfirmPanel()
    {
        ConfirmPanel.SetActive(true);
    }

    public void CloseConfirmPanel()
    {
        ConfirmPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
