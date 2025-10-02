using UnityEngine;
using UnityEngine.SceneManagement;

public class StartToDiplom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void LoadDiplomScene()
    {
        SceneManager.LoadScene("ChoiceDiplomScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
