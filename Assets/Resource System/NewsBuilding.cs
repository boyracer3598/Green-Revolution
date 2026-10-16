using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class Articles
{
    public string[] firstNames;
    public string[] lastNames;
    public string[] sources;
    public string[] jobs;
    public string[] historicalEvents;
    public string[] years;
    public string[] articles;
}
public class NewsBuilding : BuildingObject
{
    public string[] firstNames;
    public string[] lastNames;
    public string[] sources;
    public string[] jobs;
    public string[] historicalEvents;
    public string[] years;
    public string[] articles;
    bool hasPlaced = false;
    private void Update()
    {
        if (hasPlaced)
        {
            hasPlaced = true;
            
        }
    }
    private string ArticleCreation()
    {
        return articles[Random.Range(0, articles.Length)];
    }

    public override void Upkeep()
    {
        Debug.Log(ArticleCreation());
    }
}
