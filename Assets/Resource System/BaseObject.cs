using System;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public WorldVariables world;
    public string ObjectName;
    public string Description;
    public enum ProduceTypes
    {
        WATER,
        AIR,
        LAND
    }
    public ProduceTypes ProduceType;
    public int PolProduce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        world = GameObject.FindFirstObjectByType<WorldVariables>();
    }
    void UpdatePollution(bool pos)
    {
        int polChange = pos ? PolProduce : -PolProduce;
        switch (ProduceType)
        {
            case ProduceTypes.WATER:
                world.WaterPollution += polChange;
                break;
            case ProduceTypes.AIR:
                world.AirPollution += polChange;
                break;
            case ProduceTypes.LAND:
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
