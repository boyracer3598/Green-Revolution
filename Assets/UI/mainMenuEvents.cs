using UnityEngine;
using UnityEngine.UIElements;

public class mainMenuEvents : MonoBehaviour
{
    
    private UIDocument UIDoc;
    private Button newGameButton;
    private Button loadGameButton;
    private Button settingsButton;

    private void Awake()
    {
        UIDoc = GetComponent<UIDocument>();

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
        Debug.Log("it should start game");
    }


    private void onLoadGameClick(ClickEvent evt)
    {
        Debug.Log("it will load game");
    }

    private void onSettingsClick(ClickEvent evt)
    {
        Debug.Log("it will got to settings");
    }
}
