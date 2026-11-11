using UnityEngine;

public class SpecialReqBuilding : BuildingObject
{
    public enum ExtraType
    {
        POPHAP,
        AIRPOL,
        WATERPOL,
        LANDPOL
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
            case ExtraType.AIRPOL:
                if (pos == TypePos.POS)
                {
                    cont = world.AirPollution >= requirementNum;
                }
                else
                {
                    cont = world.AirPollution <= requirementNum;
                }
                break;
            case ExtraType.WATERPOL:
                if (pos == TypePos.POS) 
                {
                    cont = world.WaterPollution >= requirementNum;
                }
                else
                {
                    cont = world.WaterPollution <= requirementNum;
                }
                break;
                case ExtraType.LANDPOL:
                if (pos == TypePos.POS)
                {
                    cont = world.LandPollution >= requirementNum;
                }
                else
                {
                    cont = world.LandPollution <= requirementNum;
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
