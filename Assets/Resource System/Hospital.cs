using UnityEngine;

public class Hospital : BuildingObject
{
    public float sickReduction = 0.1f;
    public override void Placed()
    {
        world.sickChance -= sickReduction;
        world.sickChance = Mathf.Clamp((float)world.sickChance, 0.1f, 0.5f);
    }
    public override void RemoveObject(bool pos)
    {
        world.sickChance += sickReduction;
        world.sickChance = Mathf.Clamp((float)world.sickChance, 0.1f, 0.5f);
        base.RemoveObject(pos);
    }
}
