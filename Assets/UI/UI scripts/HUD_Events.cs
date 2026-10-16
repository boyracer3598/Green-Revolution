using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Events : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button settingsButton;
    private Button pauseButton;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public StyleSheet styleSheet;
    [SerializeField] BuildingSystem buildingSystem;
    
    void OnEnable()
    {     
        //button setup
        UIDoc = gameObject.GetComponent<UIDocument>();
        settingsButton= UIDoc.rootVisualElement.Q<Button>("settingsButton");
        pauseButton= UIDoc.rootVisualElement.Q<Button>("pauseButton");
        settingsButton.RegisterCallback<ClickEvent>(onSettingClick);
        pauseButton.RegisterCallback<ClickEvent>(onPauseClick);

        //tab setup
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("buildingButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("EnvironmentButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("MiscButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("SpecialistButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("CityButtons"));

        //panel setup
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    
    void Update()
    {
        
    }

    //give all the buttons in a tab the funtion 
    private void tabSetup(GroupBox tabs)
    {
        foreach (Button buttons in tabs.Children())
        {
            buttons.RegisterCallback<ClickEvent>(selectBuildingUI);
        }
    }


    // calls the selectBuilding function in building system
    private void selectBuildingUI(ClickEvent evt)
    {
        Button clickedButton = evt.target as Button;
        buildingSystem.SelectBuilding(clickedButton.name);
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
