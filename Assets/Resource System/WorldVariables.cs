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
    void Start()
    {
        // just testing values
        food = 0;
        wood = 100;
        stone = 100;
        fuel = 0;
        ChangeSpMats(true, "Wool");
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
    public bool HasSpMat(string Material)
    {
        // checks if the material is in the list of special materials
        return SpecialMaterials.Contains(Material);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
