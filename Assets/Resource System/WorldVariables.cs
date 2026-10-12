using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class WorldVariables : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string WorldName {  get; set; }
    public int food;
    public int wood;
    public int stone;
    public int fuel;
    public List<string> SpecialMaterials;
    public int AirPollution { get; set; }
    public int WaterPollution { get; set; }
    public int LandPollution { get; set; }
    public float PopulationHappiness { get; set; } = 0.5f; // with world creation, maybe add a setting to choose your starting happiness?
    public int Population { get; set; } = 0;
    public int EmployedPopulation { get; set; } = 0;
    public int Inspiration { get; set; } = 0;
    public float baseDamage;
    public float basePermDamage;
    public bool safeDestroy = false;
    void Start()
    {

    }
    public void ChangeSpMats(bool Add, string Material)
    {
        // If Add is true, adds the material to the list of special materials, if it isn't, it removes it.
        // Does nothing if the material is already in the list and Add is true, or if the material is not in the list and Add is false.
        if (Add && !SpecialMaterials.Contains(Material))
        {
            this.SpecialMaterials.Add(Material);
        }
        else if (!Add && SpecialMaterials.Contains(Material))
        {
            this.SpecialMaterials.Remove(Material);
        }
    }
    public float calculateHappiness(float input)
    {
        // calculates the happiness as a float between 0 and 1, based on the input value, which is clamped between 0 and 100.
        return Mathf.Clamp(input, 0, 100) / 100f;
    }
    public bool HasSpMat(string Material)
    {
        // checks if the material is in the list of special materials
        return SpecialMaterials.Contains(Material);
    }
    public void UpdateInspiration()
    {
        BuildingObject[] buildings = GetComponents<BuildingObject>().ToArray();
        int totalInspiration = 0;
        string[] buildingNames = new string[buildings.Length];
        Dictionary<string, int> buildingCounts = new Dictionary<string, int>();
        for (int i = 0; i < buildings.Length; i++)
        {
            buildingNames[i] = buildings[i].name;
            if (buildings[i].Inspiration != null)
            {
                if (buildingCounts.ContainsKey(buildings[i].name))
                {
                    buildingCounts[buildings[i].name] += 1;
                }
                else
                {
                    buildingCounts[buildings[i].name] = 1;
                }
            }
            foreach (var kvp in buildingCounts)
            {
                //int thisBuildingsInspiration = buildings.Where(b => b.name == kvp.Key).FirstOrDefault()?.Inspiration[kvp.Value] ?? 0;
                //totalInspiration += thisBuildingsInspiration;
            }
        }
        this.Inspiration = totalInspiration;
    }
    // Update is called once per frame
    void Update()
    {
        if (PopulationHappiness < 0)
        {
            PopulationHappiness = 0;
        }
        else if (PopulationHappiness > 1)
        {
            PopulationHappiness = 1;
        }
        Debug.Log(this.Inspiration);
    }
}
