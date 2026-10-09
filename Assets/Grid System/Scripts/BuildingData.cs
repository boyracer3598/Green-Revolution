using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class BuildingData : ScriptableObject
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
    [field: SerializeField] public int WoodCost { get; private set; }
    [field: SerializeField] public int FoodCost { get; private set; }
    [field: SerializeField] public int StoneCost { get; private set; }
    [field: SerializeField] public int FuelCost { get; private set; }
    [field: SerializeField] public float PopulationHappiness { get; private set; }
    [field: SerializeField] public int ProduceAmount { get; private set; }
    [field: SerializeField] public string ProduceType { get; private set; }
    [field: SerializeField] public BuildingModel Model { get; private set; }
}
