using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class BuildingObject : BaseObject
{
    [SerializeField] private int WoodCost = 0;
    [SerializeField] private int StoneCost = 0;
    [SerializeField] private int FoodCost = 0;
    [SerializeField] private int FuelCost = 0;
    public string SpMats = string.Empty;
    [SerializeField] private string SpMatsNeeds = string.Empty;
    [SerializeField] private int WoodUpkeep = 0;
    [SerializeField] private int StoneUpkeep = 0;
    [SerializeField] private int FoodUpkeep = 0;
    [SerializeField] private int FuelUpkeep = 0;
    [SerializeField] private int CropUpkeep = 0;
    public bool canProvide = true;
    public int[] Inspiration = null;
    
    // This function is used to add data to the building object, it is called from the BuildingSystem script's PlaceBuilding function.
    public void addData(string name, string description, int wood, int stone, int food, int fuel, int produceAmt, string prodType, int popHap, int[] inspiration, int upkeepWood, int upkeepStone, int upkeepFuel, int upkeepFood, int upkeepCrops, string spMatsNeeds)
    {
        this.ObjectName = name;
        this.Description = description;
        this.WoodCost = wood;
        this.StoneCost = stone;
        this.FoodCost = food;
        this.FuelCost = fuel;
        this.produceAmount = produceAmt;
        this.populationHappiness = popHap;
        this.Inspiration = inspiration;
        this.WoodUpkeep = upkeepWood;
        this.StoneUpkeep = upkeepStone;
        this.FuelCost = upkeepFuel;
        this.FoodUpkeep = upkeepFood;
        this.CropUpkeep = upkeepCrops; // cropkeep
        this.SpMatsNeeds = spMatsNeeds;
        switch(prodType)
        {
            case "wood":
                this.produceType = ProduceTypes.WOOD;
                break;
            case "stone":
                this.produceType = ProduceTypes.STONE;
                break;
            case "food":
                this.produceType = ProduceTypes.FOOD;
                break;
            case "fuel":
                this.produceType = ProduceTypes.FUEL;
                break;
            case "crops":
                this.produceType = ProduceTypes.CROPS;
                break;
            default:
                this.produceType = ProduceTypes.NONE;
                break;
        }
    }
    // This function is used to harvest the building object, it adds the resources back to the world variables and removes the object if destroy is true.
    public void Harvest(bool destroy)
    {
        switch (produceType)
        {
            case ProduceTypes.WOOD:
                world.wood += produceAmount;
                break;
            case ProduceTypes.STONE:
                world.stone += produceAmount;
                break;
            case ProduceTypes.FOOD:
                world.food += produceAmount;
                break;
            case ProduceTypes.FUEL:
                world.fuel += produceAmount;
                break;
            case ProduceTypes.CROPS:
                world.crops += produceAmount;
                break;
        }
        if (destroy)
        {
            RemoveObject(world.safeDestroy); // removes it and negatively affects (increases) pollution
        }
    }
    public void PayUpkeepCosts()
    {
        int worldGotIt = 0;
        string[] requiredMaterials = SpMatsNeeds.Split(", ");
        for (int i = 0; i < requiredMaterials.Length; i++)
        {
            if (world.HasSpMat(requiredMaterials[i]))
            {
                worldGotIt++;
            }
        }
        if (worldGotIt == requiredMaterials.Length && world.wood >= WoodUpkeep && world.stone >= StoneUpkeep && world.food >= FoodUpkeep && world.fuel >= FuelUpkeep && world.crops >= CropUpkeep)
        {
            world.wood -= WoodUpkeep;
            world.stone -= StoneUpkeep;
            world.food -= FoodUpkeep;
            world.fuel -= FuelUpkeep;
            world.crops -= CropUpkeep;
            canProvide = true;
        }
        else
        {
            canProvide = false;
        }
    }
    public virtual void Placed()
    {

    }
    public virtual void Upkeep()
    {
        this.PayUpkeepCosts();
        if (this.canProvide)
        {
            this.Harvest(false);
            world.ChangeSpMats(true, this.SpMats);
        }
        else
        {
            world.ChangeSpMats(false, this.SpMats);
        }
    }
}
