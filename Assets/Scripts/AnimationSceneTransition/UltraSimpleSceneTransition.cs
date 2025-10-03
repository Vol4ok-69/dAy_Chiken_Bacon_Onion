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
        if (!IsSceneInBuildSettings(sceneName) && sceneName != "")
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
        }
    }
    
    public void OnButtonClicked()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is empty! Please set scene name in inspector.");
            return;
        }
        
        if (!IsSceneInBuildSettings(sceneName))
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
            return;
        }
        
        OnClick?.Invoke();
        
        if (AnimationSceneTransition.Instance != null)
        {
            AnimationSceneTransition.Instance.LoadSceneWithTransition(sceneName);
        }
        else
        {
            Debug.LogError("AnimationSceneTransition.Instance is null! Make sure transition system is in scene.");
            SceneManager.LoadScene(sceneName);
        }
    }
    
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