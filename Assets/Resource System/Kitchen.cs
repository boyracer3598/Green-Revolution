using UnityEngine;

public class Kitchen : BuildingObject
{
    int unemployedPopulation = 0;
    public override void Upkeep()
    {
        base.Upkeep();
        unemployedPopulation = world.Population - world.EmployedPopulation;
        if (canProvide && unemployedPopulation > 0)
        {
            this.populationHappiness = 5f + 10f * (unemployedPopulation / world.Population);
            this.populationHappiness = Mathf.Round(this.populationHappiness * 10f) / 10f; // Round to 1 decimal places
        }
    }
}
