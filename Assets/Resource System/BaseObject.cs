using System;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public WorldVariables world;
    public string ObjectName;
    public string Description;
    public float populationHappiness = 0f;
    public enum PolProduceTypes
    {
        NONE, 
        AIR,
        WATER,
        LAND
    }
    public enum ProduceTypes
    {
        WOOD,
        STONE,
        FOOD,
        FUEL,
        CROPS,
        NONE
    }  
    public ProduceTypes produceType;
    public int produceAmount;
    public PolProduceTypes PolProduceType;
    public int PolProduce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public virtual void RemoveObject(bool pos)
    {
        world.HappinessLost += populationHappiness;
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
            case PolProduceTypes.NONE:
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
