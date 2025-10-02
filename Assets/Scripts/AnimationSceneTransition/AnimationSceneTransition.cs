using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnimationSceneTransition : MonoBehaviour
{
    public static AnimationSceneTransition Instance;
    
    [Header("References")]
    public Animator transitionAnimator;
    public GameObject fadePanel;
    
    [Header("Settings")]
    public float transitionSpeed = 1f;
    
    private string targetSceneName;
    public bool isTransitioning = false;
    
    // События для отслеживания состояния перехода
    public System.Action OnTransitionStart;
    public System.Action OnTransitionComplete;
    public System.Action OnFadeInComplete;
    public System.Action OnFadeOutComplete;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Автоматическое нахождение компонентов если не установлены
            if (transitionAnimator == null)
                transitionAnimator = GetComponent<Animator>();
            if (fadePanel == null)
                fadePanel = transform.GetChild(0).gameObject;
                
            // Скрываем панель в начале
            fadePanel.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        if (!isTransitioning)
        {
            targetSceneName = sceneName;
            StartCoroutine(TransitionCoroutine());
        }
    }

   private IEnumerator TransitionCoroutine()
{
    isTransitioning = true;
    OnTransitionStart?.Invoke();
    
    // Проверка на пустое имя сцены
    if (string.IsNullOrEmpty(targetSceneName))
    {
        Debug.LogError("Target scene name is empty!");
        isTransitioning = false;
        yield break;
    }
    
    // Активируем панель и запускаем затемнение
    if (fadePanel != null)
        fadePanel.SetActive(true);
        
    if (transitionAnimator != null)
    {
        transitionAnimator.SetTrigger("StartFadeIn");
        transitionAnimator.speed = transitionSpeed;
    }
    
    // Ждем завершения FadeIn
    yield return new WaitForSeconds(GetAnimationLength("FadeIn") / transitionSpeed);
    
    OnFadeInComplete?.Invoke();
    
    // ЗАГРУЗКА СЦЕНЫ С ПРОВЕРКАМИ:
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
    
    // Проверяем что asyncLoad не null
    if (asyncLoad == null)
    {
        Debug.LogError($"Failed to load scene: {targetSceneName}. Scene not found in build settings!");
        
        // Осветляем экран обратно
        if (transitionAnimator != null)
            transitionAnimator.SetTrigger("StartFadeOut");
            
        yield return new WaitForSeconds(GetAnimationLength("FadeOut") / transitionSpeed);
        
        if (fadePanel != null)
            fadePanel.SetActive(false);
            
        isTransitioning = false;
        yield break;
    }
    
    asyncLoad.allowSceneActivation = false; // ← БЫВШАЯ СТРОКА 76
    
    // Ждем загрузки
    while (!asyncLoad.isDone)
    {
        // Проверяем прогресс загрузки
        if (asyncLoad.progress >= 0.9f)
        {
            // Когда загрузка почти завершена, активируем сцену
            asyncLoad.allowSceneActivation = true;
        }
        yield return null;
    }
    
    // Ждем кадр для инициализации новой сцены
    yield return new WaitForEndOfFrame();
    
    // Запускаем осветление
    if (transitionAnimator != null)
        transitionAnimator.SetTrigger("StartFadeOut");
    
    // Ждем завершения FadeOut
    yield return new WaitForSeconds(GetAnimationLength("FadeOut") / transitionSpeed);
    
    OnFadeOutComplete?.Invoke();
    
    // Скрываем панель
    if (fadePanel != null)
        fadePanel.SetActive(false);
        
    OnTransitionComplete?.Invoke();
    
    isTransitioning = false;
}

    // Методы для ручного управления анимациями
    public void StartFadeIn()
    {
        transitionAnimator.SetTrigger("StartFadeIn");
        transitionAnimator.speed = transitionSpeed;
    }

    public void StartFadeOut()
    {
        transitionAnimator.SetTrigger("StartFadeOut");
        transitionAnimator.speed = transitionSpeed;
    }

    // Получить длительность анимации
    private float GetAnimationLength(string animationName)
    {
        RuntimeAnimatorController runtimeController = transitionAnimator.runtimeAnimatorController;
        foreach (AnimationClip clip in runtimeController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 1f; // значение по умолчанию
    }

    // Метод для вызова через Animation Event в конце FadeIn анимации
    public void OnFadeInAnimationComplete()
    {
        // Можно использовать вместо корутины
    }

    // Метод для вызова через Animation Event в конце FadeOut анимации
    public void OnFadeOutAnimationComplete()
    {
        // Можно использовать вместо корутины
    }
}