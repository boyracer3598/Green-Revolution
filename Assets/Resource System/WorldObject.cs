using System;
using UnityEngine;
using static UnityEditor.MaterialProperty;


public class WorldObject : BaseObject
{
    [SerializeField] private float maxHP; // the max hp of the object before the diminish touches it
    [SerializeField] private float objectHp; // the object's current hp
    [SerializeField] private int HpDiminish = 0; // the permanent damage dealt to each object
    public void AddData(string Name, string desc, int popHap, string produces, int produceAmt, float BaseHP, float low, float high)
    {
        this.ObjectName = Name;
        this.name = string.Join(Name, "-", UnityEngine.Random.Range(0, 100).ToString());
        this.Description = desc;
        this.populationHappiness = popHap;
        this.maxHP = BaseHP + (float)Math.Round(UnityEngine.Random.Range(low, high), 1);
        this.objectHp = maxHP;
        this.produceAmount = produceAmt;
        switch (produces)
        {
            case "wood":
                this.produceType = ProduceTypes.WOOD;
                break;
            case "stone":
                this.produceType = ProduceTypes.STONE;
                break;
            case "food":
                this.produceType = ProduceTypes.FOOD;
                break;
            case "fuel":
                this.produceType = ProduceTypes.FUEL;
                break;
            case "crops":
                this.produceType = ProduceTypes.CROPS;
                break;
            default:
                this.produceType = ProduceTypes.NONE;
                break;
        }
    }
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
