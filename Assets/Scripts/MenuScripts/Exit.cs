using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
