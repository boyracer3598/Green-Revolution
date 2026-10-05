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
    public GameObject SettingsPanel;

    private void Awake()
    {
        SettingsPanel.SetActive(false);
        
        UIDoc = GetComponent<UIDocument>();
        //button setup
        newGameButton = UIDoc.rootVisualElement.Q("newGame") as Button;
        loadGameButton = UIDoc.rootVisualElement.Q("loadGame") as Button;
        settingsButton = UIDoc.rootVisualElement.Q("settings") as Button;
        
        
        // setup button events
        newGameButton.RegisterCallback<ClickEvent>(onNewGameClick);
        loadGameButton.RegisterCallback<ClickEvent>(onLoadGameClick);
        settingsButton.RegisterCallback<ClickEvent>(onSettingsClick);
    }

    private void onNewGameClick(ClickEvent evt)
    {
        //loads a new game
        SceneManager.LoadScene("NewGame");
        Debug.Log("it should start game");
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
        Debug.Log("it will got to settings");
    }
}
