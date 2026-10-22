using UnityEngine;

public class WorldObj : MonoBehaviour
{
    public string Name => data.Name;
    public string Description=>data.Description;
    public int PopulationHappiness => data.PopulationHappiness;
    public int ProduceAmount => data.ProduceAmount;
    public string ProduceType => data.ProduceType;
    public float BaseHP => data.BaseHP;
    public float HPVariableLow => data.HPVariableLow;
    public float HPVariableHigh => data.HPVariableHigh;

    private BuildingModel model;
    private WorldObjData data;

    public void Setup(WorldObjData data, float rotation)
    {
        this.data = data;
        model=Instantiate(data.Model, transform.position,Quaternion.identity,transform);
        model.Rotate(rotation);
    }
}
