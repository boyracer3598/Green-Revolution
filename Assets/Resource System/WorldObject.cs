using UnityEngine;
using System;

public class WorldObject : BaseObject
{
    [SerializeField] private float HpBase; // The base hp for the object
    [SerializeField] private float[,] HpBounds; // The variation added to each object based on two floats
    [SerializeField] private float maxHP; // the max hp of the object before the diminish touches it
    [SerializeField] private float objectHp; // the object's current hp
    [SerializeField] private int HpDiminish = 0; // the permanent damage dealt to each object
    public void Harvest()
    {
        int randnum = UnityEngine.Random.Range(0, 10);
        float variableOnDmg = randnum / 10;
        float damage = world.baseDamage + variableOnDmg;
        if (world.basePermDamage >= damage)
        {
            HpDiminish += (int)Math.Floor(damage);
            damage = 0;
        } else
        {
            HpDiminish += (int)Math.Floor(world.basePermDamage);
            damage -= world.basePermDamage;
        }
        objectHp -= damage;
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
        }
        if ((objectHp - HpDiminish) <= 0)
        {
            RemoveObject(true); // removes it and negatively affects (increases) pollution
        }
    }
}
