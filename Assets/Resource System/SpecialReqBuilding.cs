using UnityEngine;

public class SpecialReqBuilding : BuildingObject
{
    public enum ExtraType
    {
        POPHAP
    }
    public enum TypePos
    {
        POS,
        NEG
    }
    public ExtraType reqType;
    public TypePos pos;
    public float requirementNum;
    public override void Upkeep() {
        bool cont;
        switch (reqType)
        {
            case ExtraType.POPHAP:
                if (pos == TypePos.POS)
                {
                    cont = world.PopulationHappiness >= requirementNum;
                } else
                {
                    cont = world.PopulationHappiness <= requirementNum;
                }
                break;
            default:
                cont = true;
                break;
        }
        if (cont) {
            this.PayUpkeepCosts();
        } else
        {
            this.canProvide = false;
        }
        if (this.canProvide && cont)
        {
            this.Harvest(false);
            world.ChangeSpMats(true, this.SpMats);
        }
        else
        {
            world.ChangeSpMats(false, this.SpMats);
        }
    }
}
