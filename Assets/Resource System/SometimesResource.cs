using UnityEngine;
using UnityEngine.UIElements;

public class SometimesResource : BuildingObject
{
    public int resourceAmount = 0;
    public float chanceToProvide = 0.5f; // 50% chance to provide resources
    public string resourceName = "Resource";
    public override void Upkeep()
    {
        base.Upkeep();
        System.Random random = new System.Random();
        if (random.NextDouble() <= chanceToProvide)
        {
            switch (resourceName)
            {
                case "Food":
                    world.food += resourceAmount;
                    break;
                case "Fuel":
                    world.fuel += resourceAmount;
                    break;
                case "Wood":
                    world.wood += resourceAmount;
                    break;
                case "Stone":
                    world.stone += resourceAmount;
                    break;
                case "Crops":
                    world.crops += resourceAmount;
                    break;
                default:
                    Debug.LogWarning($"Unknown resource name: {resourceName}");
                    break;
            }
        }
        else
        {
            Debug.Log($"{resourceName} not provided this time.");
        }
    }
}
