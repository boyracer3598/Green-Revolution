using UnityEngine;

public class Building : MonoBehaviour
{
    public string Name => data.Name;
    public string Description=>data.Description;
    public int WoodCost => data.WoodCost;
    public int StoneCost => data.StoneCost;
    public int FoodCost => data.FoodCost;
    public int FuelCost => data.FuelCost;
    public int WoodUpkeep => data.WoodUpkeep;
    public int StoneUpkeep => data.StoneUpkeep;
    public int FoodUpkeep => data.FoodUpkeep;
    public int FuelUpkeep => data.FuelUpkeep;
    public int CropUpkeep => data.CropUpkeep;
    public string SpMats => data.SpMats;
    public string SpMatsNeeds => data.SpMatsNeeds;
    public float PopulationHappiness => data.PopulationHappiness;

    private BuildingModel model;
    private BuildingData data;

    public void Setup(BuildingData data, float rotation)
    {
        this.data = data;
        model=Instantiate(data.Model, transform.position,Quaternion.identity,transform);
        model.Rotate(rotation);
    }
}
