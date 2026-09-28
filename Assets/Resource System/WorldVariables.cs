using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class WorldVariables : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string worldName {  get; set; }
    public int food;
    public int wood;
    public int stone;
    public int fuel;
    public List<string> SpecialMaterials;
    void Start()
    {
        food = 0;
        wood = 100;
        stone = 100;
        fuel = 0;
    }
    public void changeSpMats(bool Add, string Material)
    {
        if (Add && !SpecialMaterials.Contains(Material))
        {
            this.SpecialMaterials.Add(Material);
        }
        else if (!Add && SpecialMaterials.Contains(Material))
        {
            this.SpecialMaterials.Remove(Material);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
