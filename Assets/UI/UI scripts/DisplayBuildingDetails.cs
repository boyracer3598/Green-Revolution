using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DisplayBuildingDetails : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    InputAction MouseLeftClick;
    BuildingObject hitBuilding;
    [SerializeField] UIDocument BuildingDetailsUI;
    [SerializeField] UIDocument WorldObjectDetailsUI;
    private UIDocument ActiveUI;
    [SerializeField] Vector3 UIPositionOffset = new Vector3(0, 0.4f, 0);
    private Vector3 mainCameraCurrentPos;
    private Vector3 mainCameraScaleOffset;
    private float mainCameraCurrentSize;
    private bool MouseInWindow;
    [SerializeField] Texture[] resourceIcons;

    //UI refences setup
    private VisualElement window;
    private Label buildingNameLabel;
    private Label buildingDescriptionLabel;
    private Label produceLabel;
    private Image produceIcon;
    private Label produceAmountLabel;
    private Label UpkeepWoodLabel;
    private Label UpKeepCropsLabel;
    private Label UpKeepFoodLabel;
    private Label UpKeepStoneLabel;
    private Label UpKeepFuelLabel;
    private Label pollutionLabel;
    private Button HarvestButton;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        MouseLeftClick = InputSystem.actions["build"];
        mainCameraScaleOffset = new Vector3(0.1f, 0.1f, 0.1f);
        BuildingDetailsUI.gameObject.SetActive(false);
        MouseInWindow = false;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoot raycast from main camera see if hits a building
        Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,LayerMask.GetMask("Building"))&MouseLeftClick.WasReleasedThisFrame())
        {
            hitBuilding = hit.transform.gameObject.GetComponentInParent<BuildingObject>();
            //display the building details
            if (hitBuilding != null)
            {
                Debug.Log(hit.transform.gameObject.tag);
                if (hit.transform.gameObject.tag == "Building")
                {
                    ActiveUI = BuildingDetailsUI;
                    
                }else if (hit.transform.gameObject.tag == "WorldObject")
                {
                    ActiveUI = WorldObjectDetailsUI;
                }
                ActiveUI.gameObject.SetActive(true);
                UIRotationChange();
                UIPositionChange();
                UIScaleChange();
                WakeUpUI();
                PopulatingFields();

            }
            


        }
        // click anywhere removes the UI
        else if(MouseLeftClick.WasPressedThisFrame()) {
            if (!MouseInWindow)
            {
                ActiveUI.gameObject.SetActive(false);
            }
            
        }


        if (hitBuilding != null)
        {
            
            //if camera angle changes
            if (mainCamera.transform.position != mainCameraCurrentPos)
            {
                UIRotationChange();
            }
        
            //camera size changes
            if (mainCameraCurrentSize != mainCamera.orthographicSize)
            {
                UIScaleChange();
                UIPositionChange();
            }
        }
        
        
        
    }


    private void UIScaleChange()
    {
        ActiveUI.gameObject.transform.localScale=mainCameraScaleOffset*mainCamera.orthographicSize;
        mainCameraCurrentSize = mainCamera.orthographicSize;
        
    }
    private void UIRotationChange()
    {
        ActiveUI.gameObject.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward, Vector3.up);
        mainCameraCurrentPos = mainCamera.transform.position;
    }
    
    private void UIPositionChange()
    {
        ActiveUI.gameObject.transform.position = (hitBuilding.transform.position+(UIPositionOffset*mainCamera.orthographicSize));
        
    }


    private void WakeUpUI()
    {
        //UI references setup
        buildingNameLabel = ActiveUI.rootVisualElement.Q<Label>("Name");
        buildingDescriptionLabel = ActiveUI.rootVisualElement.Q<Label>("Description");
        produceLabel = ActiveUI.rootVisualElement.Q<Label>("Produce");
        produceIcon = ActiveUI.rootVisualElement.Q<Image>("ProduceIcon");
        produceAmountLabel = ActiveUI.rootVisualElement.Q<Label>("ProduceAmount");

        UpkeepWoodLabel = ActiveUI.rootVisualElement.Q<Label>("UpkeepWood");
        UpKeepCropsLabel=ActiveUI.rootVisualElement.Q<Label>("UpKeepCrops");
        UpKeepFoodLabel=ActiveUI.rootVisualElement.Q<Label>("UpKeepFood");
        UpKeepStoneLabel=ActiveUI.rootVisualElement.Q<Label>("UpKeepStone");
        UpKeepFuelLabel=ActiveUI.rootVisualElement.Q<Label>("UpKeepFuel");

        pollutionLabel = ActiveUI.rootVisualElement.Q<Label>("Pollution");

        HarvestButton = ActiveUI.rootVisualElement.Q<Button>("Harvest");
        Debug.Log(HarvestButton);
        if (HarvestButton != null) {
            HarvestButton.RegisterCallback<ClickEvent>(clickHarvest);
        }
        window = ActiveUI.rootVisualElement.Q<VisualElement>("wrapper");
        
        window.RegisterCallback<PointerEnterEvent>(MouseEnterWindow);
        window.RegisterCallback<PointerLeaveEvent>(MouseExitWindow);
        
    }

    private void MouseEnterWindow(PointerEnterEvent evt)
    {
        MouseInWindow = true;
    }

    private void MouseExitWindow(PointerLeaveEvent evt)
    {
        MouseInWindow = false;
    }


    private void clickHarvest(ClickEvent evt)
    {
        Debug.Log("harvest Funtion");
    }


    private void PopulatingFields()
    {
        
        buildingNameLabel.text= "Name: " +hitBuilding.ObjectName;
        buildingDescriptionLabel.text = "Description: " + hitBuilding.Description;
        produceLabel.text = "Produce: " + hitBuilding.produceType.ToString().ToLower();
        produceAmountLabel.text = hitBuilding.produceAmount.ToString();
        switch (hitBuilding.produceType.ToString().ToLower())
        {
            case "wood":
                produceIcon.image = resourceIcons[0];
                break;
            case "crops":
                produceIcon.image= resourceIcons[1];
                break;
            case "food":
                produceIcon.image=resourceIcons[2];
                break;
            case "stone":
                produceIcon.image = resourceIcons[3];
                break;
            case "fuel":
                produceIcon.image = resourceIcons[4];
                break;

            default:
                produceIcon.image = resourceIcons[5];
                break;
        }
        //maybe add get functions???
        if(UpkeepWoodLabel != null)
        {
            UpkeepWoodLabel.text = hitBuilding.WoodUpkeep1.ToString();
            UpKeepCropsLabel.text = hitBuilding.CropUpkeep1.ToString();
            UpKeepFoodLabel.text = hitBuilding.FoodUpkeep1.ToString();
            UpKeepStoneLabel.text = hitBuilding.StoneUpkeep1.ToString();
            UpKeepFuelLabel.text = hitBuilding.FuelUpkeep1.ToString();
            pollutionLabel.text = "Pollution: " + hitBuilding.PolProduceType.ToString().ToLower() + " " + hitBuilding.PolProduce.ToString();
        }
       


    }
    
}
