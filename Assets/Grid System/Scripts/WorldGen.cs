using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField] private BuildingGrid grid;
    [SerializeField] private WorldObj wObjPrefab;
    [SerializeField] private WorldObjData[] trees;
    [SerializeField] private WorldObjData[] rocks;
    [SerializeField] private WorldObjData[] fuel;
    [SerializeField] private WorldObjData[] plants;
    [SerializeField] private (int x, int y) gridSize;
    bool hasGenerated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void WorldStart()
    {
        if (!hasGenerated)
        {
            hasGenerated = true;
            gridSize = grid.getSize();
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    if (UnityEngine.Random.Range(0, 10) == 1)
                    {
                        WorldObjData data = trees[0];
                        PlaceObject(data, new Vector3(i, 0, j));
                    }
                }
            }
        }
    }
    private void PlaceObject(WorldObjData data, Vector3 position)
    {
        // creates the building at the preview's position and gives it the required data and rotation
        float rotation = UnityEngine.Random.Range(0, 360);
        WorldObj wObject = Instantiate(wObjPrefab, position, Quaternion.identity);
        wObject.Setup(data, rotation);
        WorldObject wObjObject = wObject.gameObject.GetComponent<WorldObject>();
        wObjObject.AddData(data.Name, data.Description, data.PopulationHappiness, data.ProduceType, data.ProduceAmount, data.BaseHP, data.HPVariableLow, data.HPVariableHigh);
        grid.SetObject(wObject, (int)position.x, (int)position.z);
    }
}
