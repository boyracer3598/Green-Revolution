using UnityEngine;

public class Hospital : BuildingObject
{
    public override void Placed()
    {
        world.sickChance -= 0.1;
        world.sickChance = Mathf.Clamp((float)world.sickChance, 0.1f, 0.5f);
    }
    public override void RemoveObject(bool pos)
    {
        world.sickChance += 0.1;
        world.sickChance = Mathf.Clamp((float)world.sickChance, 0.1f, 0.5f);
        base.RemoveObject(pos);
    }
}
