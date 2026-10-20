using System.Collections.Generic;
using UnityEngine;

public class WorldGen : BuildingSystem
{
    [SerializeField] private BuildingGrid grid;
    [SerializeField] private WorldObjData[] trees;
    [SerializeField] private WorldObjData[] rocks;
    [SerializeField] private WorldObjData[] fuel;
    [SerializeField] private WorldObjData[] plants;
    [SerializeField] private (int x, int y) gridSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridSize = grid.getSize();
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                // Use some refactored buildingsystem code to place the world objects in each position, for testing.
                // Implement weighting on the data files and add all the weights of each type of wObject and do some weighting system type stuff
                // have it place a world object based on what the weight is
                // yeah
            }
        }
    }

}
