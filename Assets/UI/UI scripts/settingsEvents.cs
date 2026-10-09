using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class settingsEvents : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button backButton; 
    public GameObject prevPanel;
    void OnEnable()
    {
        UIDoc = GetComponent<UIDocument>();
        
        //button setup
        backButton = UIDoc.rootVisualElement.Q<Button>("backButton");
        backButton.RegisterCallback<ClickEvent>(onBackClick);
    }

    public void onBackClick(ClickEvent evt)
    {
        prevPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    
    
    void Update()
    {
        
    }
}
