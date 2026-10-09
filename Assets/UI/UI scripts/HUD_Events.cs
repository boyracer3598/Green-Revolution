using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Events : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button settingsButton;
    private Button pauseButton;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    
    void OnEnable()
    {
        //button setup
        UIDoc = gameObject.GetComponent<UIDocument>();
        settingsButton= UIDoc.rootVisualElement.Q<Button>("settingsButton");
        pauseButton= UIDoc.rootVisualElement.Q<Button>("pauseButton");
        settingsButton.RegisterCallback<ClickEvent>(onSettingClick);
        pauseButton.RegisterCallback<ClickEvent>(onPauseClick);
        
        //panel setup
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    
    void Update()
    {
        
    }

    private void onSettingClick(ClickEvent evt)
    {
        settingsPanel.SetActive(true);
    }

    private void onPauseClick(ClickEvent evt)
    {
        pausePanel.SetActive(true);
    }
}
