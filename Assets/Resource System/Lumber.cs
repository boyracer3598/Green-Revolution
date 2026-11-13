using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Lumber : BuildingObject
{
    private WorldGen worldGen;
    private WorldObjData[] treeList;
    private BuildingGrid grid;

    public override void Placed()
    {
        base.Placed();
        worldGen = GameObject.FindFirstObjectByType<WorldGen>();
        treeList = worldGen.trees;
        grid = GameObject.FindFirstObjectByType<BuildingGrid>();
    }
    public override void Upkeep()
    {
        base.Upkeep();
        if (canProvide)
        {
            System.Random rand = new System.Random();
            (int x, int y) treePos = this.gridPos;
            List<Vector3> buildingPositions = new List<Vector3> { new Vector3(treePos.x, 0, treePos.y) };
            while (treePos == this.gridPos && grid.CanBuild(buildingPositions))
            {
                treePos = (rand.Next(this.gridPos.x -1, this.gridPos.x + 1), rand.Next(this.gridPos.y - 1, this.gridPos.y + 1));
                buildingPositions = new List<Vector3> { new Vector3(treePos.x, 0, treePos.y) };
            }
            int treeIndex = rand.Next(0, treeList.Length);
            WorldObjData treeData = treeList[treeIndex];
            worldGen.PlaceObject(treeData, new Vector3(treePos.x, 0, treePos.y));
        }
    }
}
