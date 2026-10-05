using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public const float CellSize = 1f;
    public WorldVariables world;
    [SerializeField] private BuildingData buildingData1;
    [SerializeField] private BuildingData buildingData2;
    [SerializeField] private BuildingData buildingData3;
    [SerializeField] private BuildingPreview previewPrefab;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private BuildingGrid grid;
    private BuildingPreview preview;
    [SerializeField] private Camera mainCamera;
    InputAction selectBuilding1;
    InputAction selectBuilding2;
    InputAction selectBuilding3;
    InputAction rotateBuilding; 
    InputAction buildInput;
     
    void Start()
    {
        selectBuilding1 = InputSystem.actions["SelectBuilding1"];
        selectBuilding2 = InputSystem.actions["SelectBuilding2"];
        selectBuilding3 = InputSystem.actions["SelectBuilding3"];
        rotateBuilding = InputSystem.actions["rotateBuilding"];
        buildInput = InputSystem.actions["build"];
    }
    
    
    private void Update()
    {
        Vector3 mousePos = GetMousePosition();
        Debug.Log($"Mouse Position: {mousePos}");
        if (preview != null)
        {
            HandlePreview(mousePos);
        }
        
        if (selectBuilding1.IsPressed())
        {
            if (preview != null)
            {
                Destroy(preview.gameObject); 
            }
            preview = CreatePreview(buildingData1, mousePos);
        }else if (selectBuilding2.IsPressed())
        {
            if (preview != null)
            {
                Destroy(preview.gameObject);
            }
            preview = CreatePreview(buildingData2, mousePos);
        }else if (selectBuilding3.IsPressed())
        {
            print("press 3");
            if (preview != null)
            {
                Destroy(preview.gameObject);
            }
            preview = CreatePreview(buildingData3, mousePos);
        }
    }

    private void HandlePreview(Vector3 mouseWorldPosition)
    {
        preview.transform.position = mouseWorldPosition;
        List<Vector3> buildPositions = preview.BuildingModel.GetAllBuildingPosition();
        // Checks whether the building can be placed in that spot on the grid, and also checks if they have the correct resources available
        bool canBuild = grid.CanBuild(buildPositions) && CanPlaceBuilding();

        if (canBuild)
        {
            preview.transform.position = GetSnappedCenterPosition(buildPositions);
            preview.ChangeState(BuildingPreview.BuildingPreviewState.POSITIVE);
            if (buildInput.IsPressed())
            {
                // this places the building on the grid and uses up the spendable resources
                PlaceBuilding(buildPositions);
            }
        }
        else
        {
            preview.ChangeState(BuildingPreview.BuildingPreviewState.NEGATIVE);
        }
        if (rotateBuilding.IsPressed())
        {
            // rotates the building preview by 90 degrees
            preview.Rotate(90);
        }
    }
    private bool CanPlaceBuilding()
    {
        // puts all the current resources and the required resources into variables to check if the player has enough resources to place the building
        int currentWood = world.wood, currentStone = world.stone, currentFood = world.food, currentFuel = world.fuel;
        int woodCost = preview.Data.WoodCost, stoneCost = preview.Data.StoneCost, foodCost = preview.Data.FoodCost, fuelCost = preview.Data.FuelCost;
        int matsAvailable = 0;
        int matsCount = 0;
        // makes sure the player is able to produce all the special materials
        if (!string.IsNullOrEmpty(preview.Data.SpMats))
        {
            string[] requiredMaterials = preview.Data.SpMats.Split(", ");
            matsCount = requiredMaterials.Length;
            // iterates over the required materials and checks if they are all available to the player
            foreach (string item in requiredMaterials)
            {
                if (world.HasSpMat(item)) matsAvailable++;
            }
        }
        return matsCount == matsAvailable && currentWood >= woodCost && currentStone >= stoneCost && currentFood >= foodCost  && currentFuel >= fuelCost;
    }
    private void SpendMaterials(int woodCost, int stoneCost, int foodCost, int fuelCost)
    {
        // subtracts the required resources from the player's current resources
        world.wood -= woodCost;
        world.stone -= stoneCost;
        world.food -= foodCost;
        world.fuel -= fuelCost;
    }
    private void PlaceBuilding(List<Vector3> buildingPositions)
    {
        // creates the building at the preview's position and gives it the required data and rotation
        Building building = Instantiate(buildingPrefab, preview.transform.position, Quaternion.identity);
        building.Setup(preview.Data, preview.BuildingModel.Rotation);
        grid.SetBuilding(building, buildingPositions);
        // deducts the resources from the player and destroys the preview object
        SpendMaterials(preview.Data.WoodCost, preview.Data.StoneCost, preview.Data.FoodCost, preview.Data.FuelCost);
        Destroy(preview.gameObject);
        preview = null;
    }

    private Vector3 GetSnappedCenterPosition(List<Vector3> allBuildingPositions)
    {
        List<int> xs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.x)).ToList();
        List<int> zs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.z)).ToList();
        float centerX = (xs.Min() + xs.Max()) / 2f + CellSize / 2f;
        float centerZ = (zs.Min() + zs.Max()) / 2f + CellSize / 2f;
        return new(centerX,0,centerZ);

    }
    private Vector3 GetMousePosition()
    {
        // grabs where the mouse is pointing to on the ground plane
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y,0));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    private BuildingPreview CreatePreview(BuildingData data, Vector3 position)
    {
        BuildingPreview buildingPreview = Instantiate(previewPrefab, position, Quaternion.identity);
        buildingPreview.Setup(data);
        buildingPreview.ChangeState(BuildingPreview.BuildingPreviewState.NEGATIVE);
        return buildingPreview;
    }
    
}
