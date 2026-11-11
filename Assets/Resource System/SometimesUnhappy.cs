using UnityEngine;

public class SometimesUnhappy : BuildingObject
{
    public float chanceToBeUnhappy = 0.5f; // 50% chance to be unhappy
    float realHappiness = 0;
    public override void Placed()
    {
        base.Placed();
        realHappiness = this.populationHappiness;
    }
    public override void Upkeep()
    {
        base.Upkeep();
        System.Random random = new System.Random();
        if (canProvide && random.NextDouble() < chanceToBeUnhappy)
        {
            this.populationHappiness = realHappiness * -1;
        } else
        {
            this.populationHappiness = realHappiness;
        }
    }
}
