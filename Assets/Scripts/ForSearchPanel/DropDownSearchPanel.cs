using UnityEngine;
using UnityEngine.UI;

public class DropDownSearchPanel : MonoBehaviour
{   
    public GameObject _button;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }
    
    public void OnButtonClick()
    {
        if (_button.activeInHierarchy)
        {
            _button.SetActive(false);
        }
        else
        {
            _button.SetActive(true);
        }
    }
}