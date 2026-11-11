using UnityEngine;

public class Damager : BuildingObject
{
    public float damageAmount = -0.5f;
    public float permDamageAmount = 1f;
    public override void Placed()
    {
        base.Placed();
        world.baseDamage += damageAmount;
        world.basePermDamage += permDamageAmount;
    }
    public override void RemoveObject(bool pos)
    {
        base.RemoveObject(pos);
        world.baseDamage -= damageAmount;
        world.basePermDamage -= permDamageAmount;
    }
}
