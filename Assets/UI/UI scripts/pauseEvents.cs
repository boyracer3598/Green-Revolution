using UnityEngine;
using UnityEngine.UIElements;

public class pauseEvents : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject ChallangePanel;
    private UIDocument UIDoc;
    private Button resumeButton;
    private Button settingsButton;
    private Button loadButton;
    private Button QuitButton;


    private void Start()
    {
        
    }

    void OnEnable()
    {
        //hides panels
        settingsPanel.SetActive(false);
        ChallangePanel.SetActive(true);
        Time.timeScale = 0;
        //button setup
        UIDoc = gameObject.GetComponent<UIDocument>();
        resumeButton = UIDoc.rootVisualElement.Q<Button>("resumeGame");
        settingsButton = UIDoc.rootVisualElement.Q<Button>("settings");
        loadButton = UIDoc.rootVisualElement.Q<Button>("loadGame");
        QuitButton = UIDoc.rootVisualElement.Q<Button>("QuitGame");
        //event setup
        resumeButton.RegisterCallback<ClickEvent>(onResumeClick);
        settingsButton.RegisterCallback<ClickEvent>(onSettingsClick);
        loadButton.RegisterCallback<ClickEvent>(onLoadClick);
        QuitButton.RegisterCallback<ClickEvent>(OnQuitClick);


    }

    private void onSettingsClick(ClickEvent evt)
    {
        settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void onResumeClick(ClickEvent evt)
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void onLoadClick(ClickEvent evt)
    {
        Debug.Log("Load game");
    }

    private void OnQuitClick(ClickEvent evt)
    {
        Application.Quit();
    }


}
