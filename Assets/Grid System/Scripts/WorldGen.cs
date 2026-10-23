using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WorldGen : MonoBehaviour
{
    [SerializeField] private BuildingGrid grid;
    [SerializeField] private WorldObj wObjPrefab;
    [SerializeField] private WorldObjData[] trees;
    private float[] treeWeights;
    [SerializeField] private WorldObjData[] rocks;
    private float[] rockWeights;
    [SerializeField] private WorldObjData[] fuel;
    private float[] fuelWeights;
    [SerializeField] private WorldObjData[] plants;
    private float[] plantWeights;
    [SerializeField] private (int x, int y) gridSize;
    bool hasGenerated = false;
    

    public void WorldStart()
    {
        if (!hasGenerated)
        {
            hasGenerated = true;
            gridSize = grid.getSize();
            for (int spot = 0; spot < trees.Length; spot++)
            {
                treeWeights[spot] = trees[spot].Weighting;
            }
            //for (int spot = 0; spot < trees.Length; spot++)
            //{
            //    rockWeights[spot] = rocks[spot].Weighting;
            //}
            //for (int spot = 0; spot < trees.Length; spot++)
            //{
            //    fuelWeights[spot] = fuel[spot].Weighting;
            //}
            //for (int spot = 0; spot < trees.Length; spot++)
            //{
            //    plantWeights[spot] = plants[spot].Weighting;
            //}
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    int objectSelectWeight = UnityEngine.Random.Range(0, 100);
                    WorldObjData data = null;
                    if (objectSelectWeight <= 15)
                    {
                        data = trees[0];
                    }
                    else if (objectSelectWeight > 15 && objectSelectWeight <= 27)
                    {
                        data = rocks[0];
                    }
                    else if (objectSelectWeight > 27 && objectSelectWeight <= 40)
                    {
                        //data = fuel[0];
                    }
                    else if (objectSelectWeight > 40 && objectSelectWeight <= 75)
                    {
                        //data = plants[0];
                    }
                    if (data != null) { 
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
