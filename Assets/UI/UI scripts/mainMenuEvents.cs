using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class mainMenuEvents : MonoBehaviour
{
    
    private UIDocument UIDoc;
    private Button newGameButton;
    private Button loadGameButton;
    private Button settingsButton;
    private Button QuitButton;
    public GameObject SettingsPanel;
    public GameObject loadingPanel;
    private UIDocument LoadingUIDoc;
    private ProgressBar loadingBar;


    
    private void OnEnable()
    {
        
        SettingsPanel.SetActive(false);
        UIDoc = GetComponent<UIDocument>();
        //button setup
        newGameButton = UIDoc.rootVisualElement.Q("newGame") as Button;
        loadGameButton = UIDoc.rootVisualElement.Q("loadGame") as Button;
        settingsButton = UIDoc.rootVisualElement.Q("settings") as Button;
        QuitButton = UIDoc.rootVisualElement.Q("QuitGame") as Button;
        
        
        // setup button events
        newGameButton.RegisterCallback<ClickEvent>(onNewGameClick);
        loadGameButton.RegisterCallback<ClickEvent>(onLoadGameClick);
        settingsButton.RegisterCallback<ClickEvent>(onSettingsClick);
        QuitButton.RegisterCallback<ClickEvent>(OnQuitClick);
    }

    


    private void onNewGameClick(ClickEvent evt)
    {
        //loads a new game
        loadingPanel.SetActive(true);
        LoadingUIDoc= loadingPanel.GetComponent<UIDocument>();
        loadingBar = LoadingUIDoc.rootVisualElement.Q("Loading")as ProgressBar;
        //start load async
        StartCoroutine(LoadGameAsync("NewGame v4"));
        
    }
    public IEnumerator LoadGameAsync(string levelName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelName);
        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
    }





    private void onLoadGameClick(ClickEvent evt)
    {
        //loads from a saved game
        SceneManager.LoadScene("LoadGame");
        Debug.Log("it will load game");
    }

    private void onSettingsClick(ClickEvent evt)
    {
        //Hides mainmenu and show the settings panel
        SettingsPanel.SetActive(true);
        gameObject.SetActive(false); 
    }

    private void OnQuitClick(ClickEvent evt) {
        Application.Quit();
    }
}
