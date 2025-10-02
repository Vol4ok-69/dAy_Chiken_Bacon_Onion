using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SimpleTransitionForButton : MonoBehaviour
{
    [Header("Scene Name")]
    public string sceneName = "MainMenu";
    
    [Header("Events")]
    public UnityEvent OnClick;
    
    private void Start()
    {
        // Проверяем что сцена существует в build settings
        if (!IsSceneInBuildSettings(sceneName) && sceneName != "")
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
        }
    }
    
    public void OnButtonClicked()
    {
        // Проверяем что имя сцены не пустое
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty! Please set scene name in inspector.");
            return;
        }
        
        // Проверяем что сцена существует
        if (!IsSceneInBuildSettings(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
            return;
        }
        
        // Выполняем кастомные события
        OnClick?.Invoke();
        
        // Запускаем переход
        if (AnimationSceneTransition.Instance != null)
        {
            AnimationSceneTransition.Instance.LoadSceneWithTransition(sceneName);
        }
        else
        {
            Debug.LogError("AnimationSceneTransition.Instance is null! Make sure transition system is in scene.");
            // Fallback: загружаем сцену без анимации
            SceneManager.LoadScene(sceneName);
        }
    }
    
    // Проверяем есть ли сцена в Build Settings
    private bool IsSceneInBuildSettings(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName)) return false;
        
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (scene == sceneName)
                return true;
        }
        return false;
    }
}