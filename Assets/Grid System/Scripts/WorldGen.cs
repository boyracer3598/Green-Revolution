using System;
using System.Collections.Generic;
using UnityEngine;


public class WorldGen : MonoBehaviour
{
    [SerializeField] private BuildingGrid grid;
    [SerializeField] private WorldObj wObjPrefab;
    [SerializeField] private WorldObjData[] trees;
    private WorldObjData[] treeWeights;
    [SerializeField] private WorldObjData[] rocks;
    private WorldObjData[] rockWeights;
    [SerializeField] private WorldObjData[] fuel;
    private WorldObjData[] fuelWeights;
    [SerializeField] private WorldObjData[] plants;
    private WorldObjData[] plantWeights;
    [SerializeField] private (int x, int y) gridSize;
    bool hasGenerated = false;
    

    public void WorldStart()
    {
        if (!hasGenerated)
        {
            hasGenerated = true;
            gridSize = grid.getSize();

            treeWeights = BuildWeightedObjects(trees);
            rockWeights = BuildWeightedObjects(rocks);
            fuelWeights = BuildWeightedObjects(fuel);
            plantWeights = BuildWeightedObjects(plants);

            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    System.Random beep = new System.Random();
                    int objectSelectWeight = beep.Next(0, 200);
                    WorldObjData data = null;
                    if (objectSelectWeight <= 15 && treeWeights.Length > 0)
                    {
                        int x = beep.Next(0, treeWeights.Length);
                        data = treeWeights[x];
                    }
                    else if (objectSelectWeight > 15 && objectSelectWeight <= 27 && rockWeights.Length > 0)
                    {
                        int x = beep.Next(0, rockWeights.Length);
                        data = rockWeights[x];
                    }
                    else if (objectSelectWeight > 27 && objectSelectWeight <= 40 && fuelWeights.Length > 0)
                    {
                        int x = beep.Next(0, fuelWeights.Length);
                        data = fuelWeights[x];
                    }
                    else if (objectSelectWeight > 40 && objectSelectWeight <= 75 && plantWeights.Length > 0)
                    {
                        int x = beep.Next(0, plantWeights.Length);
                        data = plantWeights[x];
                    }
                    if (data != null) {
                        PlaceObject(data, new Vector3(i, 0, j));
                    }
                }
            }
        }
    }

    private static WorldObjData[] BuildWeightedObjects(WorldObjData[] objects)
    {
        List<WorldObjData> weightedObjects = new List<WorldObjData>();

        if (objects == null)
        {
            return weightedObjects.ToArray();
        }

        foreach (WorldObjData obj in objects)
        {
            if (obj == null)
            {
                continue;
            }

            int weight = Mathf.Max(0, Mathf.CeilToInt(obj.Weighting));
            for (int i = 0; i < weight; i++)
            {
                weightedObjects.Add(obj);
            }
        }

        return weightedObjects.ToArray();
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
