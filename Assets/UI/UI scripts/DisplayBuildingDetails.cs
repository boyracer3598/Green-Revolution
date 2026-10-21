using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DisplayBuildingDetails : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    InputAction MouseLeftClick;
    BuildingObject hitBuilding;
    [SerializeField] UIDocument UIObject;
    [SerializeField] Vector3 UIPositionOffset = new Vector3(0, 0.4f, 0);
    private Vector3 mainCameraCurrentPos;
    private Vector3 mainCameraScaleOffset;
    private float mainCameraCurrentSize;
    
    [SerializeField] Texture[] resourceIcons;
    
    //UI refences setup
    private Label buildingNameLabel;
    private Label buildingDescriptionLabel;
    private Label produceLabel;
    private Image produceIcon;
    private Label produceAmountLabel;
    private Label pollutionLabel;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        MouseLeftClick = InputSystem.actions["build"];
        mainCameraScaleOffset = new Vector3(0.1f, 0.1f, 0.1f);
        UIObject.gameObject.SetActive(false);
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoot raycast from main camera see if hits a building
        Vector3 mousePosition = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0);
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100,LayerMask.GetMask("Building"))&MouseLeftClick.WasPressedThisFrame())
        {
            hitBuilding = hit.transform.gameObject.GetComponentInParent<BuildingObject>();
            Debug.Log(hitBuilding);
            //display the building details
            if (hitBuilding != null)
            {
                UIObject.gameObject.SetActive(true);
                UIRotationChange();
                UIPositionChange();
                UIScaleChange();
                WakeUpUI();
                PopulatingFields();

            }
            


        }
        // click anywhere removes the UI
        else if(MouseLeftClick.WasPressedThisFrame()) {
            UIObject.gameObject.SetActive(false);
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
        UIObject.gameObject.transform.localScale=mainCameraScaleOffset*mainCamera.orthographicSize;
        mainCameraCurrentSize = mainCamera.orthographicSize;
        
    }
    private void UIRotationChange()
    {
        UIObject.gameObject.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward, Vector3.up);
        mainCameraCurrentPos = mainCamera.transform.position;
    }
    
    private void UIPositionChange()
    {
        UIObject.gameObject.transform.position = (hitBuilding.transform.position+(UIPositionOffset*mainCamera.orthographicSize));
        
    }


    private void WakeUpUI()
    {
        //UI references setup
        buildingNameLabel = UIObject.rootVisualElement.Q<Label>("Name");
        buildingDescriptionLabel = UIObject.rootVisualElement.Q<Label>("Description");
        produceLabel = UIObject.rootVisualElement.Q<Label>("produce");
        produceIcon = UIObject.rootVisualElement.Q<Image>("produceIcon");
        produceAmountLabel = UIObject.rootVisualElement.Q<Label>("produceAmount");
        pollutionLabel = UIObject.rootVisualElement.Q<Label>("Pollution");
        
    }
    private void PopulatingFields()
    {
        
        buildingNameLabel.text= "Name: " +hitBuilding.ObjectName;
        buildingDescriptionLabel.text = "Description: " + hitBuilding.Description;
        //produceLabel.text = "Produce: " + hitBuilding.pr
        //produceAmountLabel.text = hitBuilding.produceAmount.ToString();
        //maybe add get functions???
        pollutionLabel.text = "Pollution: "+hitBuilding.PolProduceType.ToString().ToLower() +hitBuilding.PolProduce.ToString();


    }
    
}
