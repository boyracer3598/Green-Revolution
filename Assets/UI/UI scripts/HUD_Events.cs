using UnityEngine;
using UnityEngine.UIElements;

public class HUD_Events : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button settingsButton;
    private Button pauseButton;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    //staus bar variables
    private ProgressBar waterPollutionStatus;
    private ProgressBar airPollutionStatus;
    private ProgressBar powerStatus;
    private ProgressBar happinessStatus;
    private ProgressBar inspirationStatus;
    
    // resource variables
    private Label woodAmount;
    private Label foodAmount;
    private Label stoneAmount;
    private Label fuelAmount;
    private Label cropsAmountS;
    
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
        
        //status bars setup
        waterPollutionStatus = UIDoc.rootVisualElement.Q<ProgressBar>("WaterPollutionStatus");
        airPollutionStatus = UIDoc.rootVisualElement.Q<ProgressBar>("AirPollutionStatus");
        powerStatus = UIDoc.rootVisualElement.Q<ProgressBar>("PowerStatus");
        happinessStatus = UIDoc.rootVisualElement.Q<ProgressBar>("HappinessStatus");
        inspirationStatus= UIDoc.rootVisualElement.Q<ProgressBar>("InspirationStatus");
        
        //resource amount setup
        woodAmount = UIDoc.rootVisualElement.Q<Label>("WoodAmount");
        woodAmount.text = "23";

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
        UpdateStatusBar(airPollutionStatus,20);

    }

    
    
    //update status bar
    private void UpdateStatusBar(ProgressBar statusBar, float value)
    {
        statusBar.title = value.ToString();
        statusBar.value = value;
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
