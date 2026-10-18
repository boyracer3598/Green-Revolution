using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System;

public class Articles
{
    public string[] firstNames;
    public string[] lastNames;
    public string[] sources;
    public string[] jobs;
    public string[] culprits;
    public string[] articles;
}
public class NewsBuilding : BuildingObject
{
    public string[] firstNames;
    public string[] lastNames;
    public string[] sources;
    public string[] jobs;
    public string[] culprits;
    public string[] articles;
    bool hasPlaced = false;
 
    public override void Placed()
    {
        string filePathJson = Application.streamingAssetsPath + "/ArticleData.json";
        string[] fileText = File.ReadAllLines(filePathJson);
        string fileTextOneLine = "";
        for (int i = 0; i < fileText.Length; i++)
        {
            fileTextOneLine += fileText[i];
        }
        Articles temp = JsonConvert.DeserializeObject<Articles>(fileTextOneLine);
        this.firstNames = temp.firstNames;
        this.lastNames = temp.lastNames;
        this.sources = temp.sources;
        this.jobs = temp.jobs;
        this.culprits = temp.culprits;
        this.articles = temp.articles;
    }
    private string ArticleCreation()
    {
        string newArticle = articles[UnityEngine.Random.Range(0, articles.Length)];
        while (newArticle.Contains("{name}") || newArticle.Contains("{source}") || newArticle.Contains("{job}") || newArticle.Contains("{culprit}") )
        {
            string fName = firstNames[UnityEngine.Random.Range(0, firstNames.Length)];
            string lName = lastNames[UnityEngine.Random.Range(0, lastNames.Length)];
            string newName = String.Concat(fName, " ", lName);
            string source = sources[UnityEngine.Random.Range(0, sources.Length)];
            string job = jobs[UnityEngine.Random.Range(0, jobs.Length)];
            string culprit = culprits[UnityEngine.Random.Range(0, culprits.Length)];
            newArticle = newArticle.Replace("{name}", newName);
            newArticle = newArticle.Replace("{source}", source);
            newArticle = newArticle.Replace("{job}", job);
            newArticle = newArticle.Replace("{culprit}", culprit);
        }
        return newArticle;
    }

    public override void Upkeep()
    {
        Debug.Log(ArticleCreation());
    }
}
