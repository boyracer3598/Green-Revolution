using UnityEngine;
using System.Collections.Generic;
public class BuildingObject : BaseObject
{
    [SerializeField] private int WoodCost = 0;
    [SerializeField] private int StoneCost = 0;
    [SerializeField] private int FoodCost = 0;
    [SerializeField] private int FuelCost = 0;
    [SerializeField] private string SpMats = string.Empty;
    // This function is used to add data to the building object, it is called from the BuildingSystem script's PlaceBuilding function.
    public void addData(string name, string description, int wood, int stone, int food, int fuel, int produceAmt, string prodType)
    {
        this.ObjectName = name;
        this.Description = description;
        this.WoodCost = wood;
        this.StoneCost = stone;
        this.FoodCost = food;
        this.FuelCost = fuel;
        this.produceAmount = produceAmt;
        switch (prodType)
        {
            case "wood":
                this.produceType = produceTypes.WOOD;
                break;
            case "stone":
                this.produceType = produceTypes.STONE;
                break;
            case "food":
                this.produceType = produceTypes.FOOD;
                break;
            case "fuel":
                this.produceType = produceTypes.FUEL;
                break;
            default:
                this.produceType = produceTypes.NONE;
                break;
        }
    }
    // This function is used to harvest the building object, it adds the resources back to the world variables and removes the object if destroy is true.
    public void Harvest(bool destroy)
    {
        switch (produceType)
        {
            case produceTypes.WOOD:
                world.wood += produceAmount;
                break;
            case produceTypes.STONE:
                world.stone += produceAmount;
                break;
            case produceTypes.FOOD:
                world.food += produceAmount;
                break;
            case produceTypes.FUEL:
                world.fuel += produceAmount;
                break;
        }
        if (destroy)
        {
            RemoveObject();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
