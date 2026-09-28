using UnityEngine;

public class Building : MonoBehaviour
{
    public string Description=>data.Description;
    public int WoodCost=>data.WoodCost;
    public int StoneCost => data.StoneCost;
    public int FoodCost => data.FoodCost;
    public int FuelCost => data.FuelCost;

    private BuildingModel model;
    private BuildingData data;

    public void Setup(BuildingData data, float rotation)
    {
        this.data = data;
        model=Instantiate(data.Model, transform.position,Quaternion.identity,transform);
        model.Rotate(rotation);
    }
}
