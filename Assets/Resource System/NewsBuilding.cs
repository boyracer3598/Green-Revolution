using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class NewsBuilding : BuildingObject
{
    public string[] firstNames;
    public string[] lastNames;
    public string[] sources;
    public string[] jobs;
    public string[] historicalEvents;
    public string[] years;
    public string[] articles;
    private void Start()
    {
        articles[0] = "{name} found dead at {age}";
    }
    private string ArticleCreation()
    {
        return articles[(int)Random.Range(0, articles.Length)];
    }

    public override void Upkeep()
    {
        Debug.Log(ArticleCreation());
    }
}
