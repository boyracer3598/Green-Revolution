using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HUD_Events : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button settingsButton;
    private Button pauseButton;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    //staus bar variables
    [SerializeField] private WorldVariables worldVariables;
    private ProgressBar waterPollutionStatus;
    private ProgressBar airPollutionStatus;
    private ProgressBar landPollution;
    private ProgressBar happinessStatus;
    [SerializeField] Texture[] happinessIcons;
    private ProgressBar inspirationStatus;
    private ProgressBar populationStatus;
    private ProgressBar employedPopulationStatus;
    
    // resource variables
    private Label woodAmount;
    private Label foodAmount;
    private Label stoneAmount;
    private Label fuelAmount;
    private Label cropsAmount;
    private Label happinessAmount;
    private Image happinessIcon;

    private InputAction cameraZoom;
    private ScrollView statView;
    public StyleSheet styleSheet;

    [SerializeField] BuildingSystem buildingSystem;
    [SerializeField] UIDocument buildDetailUI;
    [SerializeField] private DataStorage storage;

    void OnEnable()
    {
        cameraZoom = InputSystem.actions["Zoom"];


        //button setup
        UIDoc = gameObject.GetComponent<UIDocument>();
        settingsButton= UIDoc.rootVisualElement.Q<Button>("settingsButton");
        pauseButton= UIDoc.rootVisualElement.Q<Button>("pauseButton");
        settingsButton.RegisterCallback<ClickEvent>(onSettingClick);
        pauseButton.RegisterCallback<ClickEvent>(onPauseClick);
        
        //status bars setup
        waterPollutionStatus = UIDoc.rootVisualElement.Q<ProgressBar>("WaterPollutionStatus");
        airPollutionStatus = UIDoc.rootVisualElement.Q<ProgressBar>("AirPollutionStatus");
        landPollution = UIDoc.rootVisualElement.Q<ProgressBar>("LandPollution");
        happinessStatus = UIDoc.rootVisualElement.Q<ProgressBar>("HappinessStatus");
        inspirationStatus= UIDoc.rootVisualElement.Q<ProgressBar>("InspirationStatus");
        populationStatus = UIDoc.rootVisualElement.Q<ProgressBar>("PopulationStatus");
        employedPopulationStatus = UIDoc.rootVisualElement.Q<ProgressBar>("EmployedPopulationStatus");

        
        //resource amount setup
        woodAmount = UIDoc.rootVisualElement.Q<Label>("WoodAmount");
        foodAmount = UIDoc.rootVisualElement.Q<Label>("FoodAmount");
        stoneAmount = UIDoc.rootVisualElement.Q<Label>("StoneAmount");
        fuelAmount = UIDoc.rootVisualElement.Q<Label>("FuelAmount");
        cropsAmount = UIDoc.rootVisualElement.Q<Label>("CropsAmount");
        happinessAmount = UIDoc.rootVisualElement.Q<Label>("Happiness");
        happinessIcon = UIDoc.rootVisualElement.Q<Image>("HappyIcon");

        //tab setup
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("buildingButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("EnvironmentButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("MiscButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("SpecialistButtons"));
        tabSetup(UIDoc.rootVisualElement.Q<GroupBox>("CityButtons"));

        //panel setup
        settingsPanel.SetActive(false);
        pausePanel.SetActive(false);

        //scoll handling
        cameraZoom = InputSystem.actions["Zoom"];
        statView = UIDoc.rootVisualElement.Q<ScrollView>("StatView");
        statView.RegisterCallback<PointerEnterEvent>(EnterStat);
        statView.RegisterCallback<PointerLeaveEvent>(LeaveStat);

    }
    
    void Update()
    {
        
        UpdateResource(cropsAmount, worldVariables.crops);
        UpdateResource(foodAmount, worldVariables.food);
        UpdateResource(fuelAmount, worldVariables.fuel);
        UpdateResource(stoneAmount, worldVariables.stone);
        UpdateResource(woodAmount, worldVariables.wood);
        UpdateResource(happinessAmount, (int)(worldVariables.PopulationHappiness*100));
        UpdadeHappinessIcon(worldVariables.PopulationHappiness);
        
        UpdateStatusBar(landPollution, worldVariables.LandPollution);
        UpdateStatusBar(airPollutionStatus,worldVariables.AirPollution);
        UpdateStatusBar(happinessStatus, worldVariables.PopulationHappiness);
        UpdateStatusBar(waterPollutionStatus,worldVariables.WaterPollution);
        UpdateStatusBar(happinessStatus,(int)(worldVariables.PopulationHappiness*100));
        UpdateStatusBar(inspirationStatus,worldVariables.Inspiration);
        UpdateStatusBar(populationStatus,worldVariables.Population);
        UpdateStatusBar(employedPopulationStatus,worldVariables.EmployedPopulation);



    }


    private void UpdadeHappinessIcon(float happiness)
    {
        if (happiness <=0.3)
        {
            happinessIcon.image = happinessIcons[0];
        }else if(happiness < 0.5)
        {
            happinessIcon.image = happinessIcons[1];
        }
        else if(happiness >=0.5) {
            happinessIcon.image = happinessIcons[2];
        }
    }
    
    void UpdateResource(Label resource, int vaule)
    {
        resource.text=vaule.ToString();
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
            buttons.RegisterCallback<PointerEnterEvent>(ShowBuildDetials);
            buttons.RegisterCallback<PointerLeaveEvent>(HideBuildDetails);
        }
    }


    // calls the selectBuilding function in building system
    private void selectBuildingUI(ClickEvent evt)
    {
        Button clickedButton = evt.target as Button;
        buildingSystem.SelectBuilding(clickedButton.name);
    }


    private void ShowBuildDetials(PointerEnterEvent evt)
    {
        BuildingData buildingData=new BuildingData();
        buildDetailUI.gameObject.SetActive(true);
        Button button = evt.target as Button;
        buildDetailUI.rootVisualElement.Q<Label>("Name").text = "Name: "+button.name;
        foreach (BuildingData building in storage.buildingDatas)
        {
            if (building.Name == button.name) {
                buildingData = building;
            }
        }
        buildDetailUI.rootVisualElement.Q<Label>("WoodAmount").text = buildingData.WoodCost.ToString();
        buildDetailUI.rootVisualElement.Q<Label>("CropAmount").text = "0";
        buildDetailUI.rootVisualElement.Q<Label>("FoodAmount").text = buildingData.FoodCost.ToString();
        buildDetailUI.rootVisualElement.Q<Label>("StoneAmount").text = buildingData.StoneCost.ToString();
        buildDetailUI.rootVisualElement.Q<Label>("FuelAmount").text = buildingData.FuelCost.ToString();
    }

    private void HideBuildDetails(PointerLeaveEvent evt)
    {
        buildDetailUI.gameObject.SetActive(false);
    }

    private void onSettingClick(ClickEvent evt)
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void onPauseClick(ClickEvent evt)
    {
        pausePanel.SetActive(true);
        Time.timeScale= 0;
    }

    private void EnterStat(PointerEnterEvent evt)
    {
        cameraZoom.Disable();
    }


    private void LeaveStat(PointerLeaveEvent evt)
    {
        cameraZoom.Enable();
    }
}
