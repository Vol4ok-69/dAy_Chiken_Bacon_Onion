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

    public System.Action OnTransitionStart;
    public System.Action OnTransitionComplete;
    public System.Action OnFadeInComplete;
    public System.Action OnFadeOutComplete;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (transitionAnimator == null)
                transitionAnimator = GetComponent<Animator>();
            if (fadePanel == null)
                fadePanel = transform.GetChild(0).gameObject;
                
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

    if (string.IsNullOrEmpty(targetSceneName))
    {
        Debug.LogError("Target scene name is empty!");
        isTransitioning = false;
        yield break;
    }

    if (fadePanel != null)
        fadePanel.SetActive(true);
        
    if (transitionAnimator != null)
    {
        transitionAnimator.SetTrigger("StartFadeIn");
        transitionAnimator.speed = transitionSpeed;
    }
    
    yield return new WaitForSeconds(GetAnimationLength("FadeIn") / transitionSpeed);
    
    OnFadeInComplete?.Invoke();
    
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
    
    if (asyncLoad == null)
    {
        Debug.LogError($"Failed to load scene: {targetSceneName}. Scene not found in build settings!");
        
    
        if (transitionAnimator != null)
            transitionAnimator.SetTrigger("StartFadeOut");
            
        yield return new WaitForSeconds(GetAnimationLength("FadeOut") / transitionSpeed);
        
        if (fadePanel != null)
            fadePanel.SetActive(false);
            
        isTransitioning = false;
        yield break;
    }
    
    asyncLoad.allowSceneActivation = false;
    
    while (!asyncLoad.isDone)
    {
        if (asyncLoad.progress >= 0.9f)
            asyncLoad.allowSceneActivation = true;
        
        yield return null;
    }
    
    yield return new WaitForEndOfFrame();

    if (transitionAnimator != null)
        transitionAnimator.SetTrigger("StartFadeOut");
   
    yield return new WaitForSeconds(GetAnimationLength("FadeOut") / transitionSpeed);
    
    OnFadeOutComplete?.Invoke();
    
    if (fadePanel != null)
        fadePanel.SetActive(false);
        
    OnTransitionComplete?.Invoke();
    
    isTransitioning = false;
}

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
        return 1f;
    }

    public void OnFadeInAnimationComplete()
    {
    }

    public void OnFadeOutAnimationComplete()
    {
    }
}