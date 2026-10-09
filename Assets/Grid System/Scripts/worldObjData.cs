using UnityEngine;

[CreateAssetMenu(menuName = "Data/World Object")]
public class WorldObjData : ScriptableObject
{
    public enum ProduceTypes
    {
        WOOD,
        STONE,
        FOOD,
        FUEL
    }
    [field: SerializeField] public string Name { get; private set; } 
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public string SpMats { get; private set; } = string.Empty;
    [field: SerializeField] public float PopulationHappiness { get; private set; }
    [field: SerializeField] public int ProduceAmount { get; private set; }
    [field: SerializeField] public string ProduceType { get; private set; }
    [field: SerializeField] public float BaseHP { get; private set; }
    [field: SerializeField] public float HPVariableLow { get; private set; }
    [field: SerializeField] public float HPVariableHigh { get; private set; }
    [field: SerializeField] public BuildingModel Model { get; private set; }
}
