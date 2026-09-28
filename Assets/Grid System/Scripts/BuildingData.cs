using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class BuildingData : ScriptableObject
{
   [field: SerializeField] public string Description { get; private set; }
   [field: SerializeField] public int WoodCost { get; private set; }
   [field: SerializeField] public int FoodCost { get; private set; }
   [field: SerializeField] public int StoneCost { get; private set; }
   [field: SerializeField] public int FuelCost { get; private set; }
    [field: SerializeField] public BuildingModel Model { get; private set; }
}
