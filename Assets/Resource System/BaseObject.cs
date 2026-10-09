using System;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public WorldVariables world;
    public string ObjectName;
    public string Description;
    public int populationHappiness;
    public enum PolProduceTypes
    {
        WATER,
        AIR,
        LAND
    }
    public enum produceTypes
    {
        WOOD,
        STONE,
        FOOD,
        FUEL,
        NONE
    }  
    public produceTypes produceType;
    public int produceAmount;
    public PolProduceTypes PolProduceType;
    public int PolProduce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        world = GameObject.FindFirstObjectByType<WorldVariables>();
    }
    public void RemoveObject(bool pos)
    {
        world.PopulationHappiness -= world.calculateHappiness(this.populationHappiness);
        UpdatePollution(pos);
        Destroy(this.gameObject);
    }
    void UpdatePollution(bool pos)
    {
        int polChange = pos ? PolProduce : -PolProduce;
        switch (PolProduceType)
        {
            case PolProduceTypes.WATER:
                world.WaterPollution += polChange;
                break;
            case PolProduceTypes.AIR:
                world.AirPollution += polChange;
                break;
            case PolProduceTypes.LAND:
                world.LandPollution += polChange;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
