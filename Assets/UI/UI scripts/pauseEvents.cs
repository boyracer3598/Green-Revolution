using UnityEngine;
using UnityEngine.UIElements;

public class pauseEvents : MonoBehaviour
{
    public GameObject settingsPanel;
    private UIDocument UIDoc;
    private Button resumeButton;
    private Button settingsButton;
    private Button loadButton;
    void OnEnable()
    {
        //hides panels
        gameObject.SetActive(false);
        settingsPanel.SetActive(false);
        
        //button setup
        UIDoc = gameObject.GetComponent<UIDocument>();
        //resumeButton = UIDoc.rootVisualElement.Q<Button>("resumeGame");
        settingsButton = UIDoc.rootVisualElement.Q<Button>("settings");
        loadButton = UIDoc.rootVisualElement.Q<Button>("loadGame");
        //event setup
        resumeButton.RegisterCallback<ClickEvent>(onResumeClick);
        settingsButton.RegisterCallback<ClickEvent>(onSettingsClik);
        loadButton.RegisterCallback<ClickEvent>(onLoadClick);
        
        
    }

    private void onSettingsClik(ClickEvent evt)
    {
        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void onResumeClick(ClickEvent evt)
    {
        gameObject.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void onLoadClick(ClickEvent evt)
    {
        Debug.Log("Load game");
    }
    
    
    
    void Update()
    {
        
    }
}
