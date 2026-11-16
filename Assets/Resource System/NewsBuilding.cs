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
    public string[] firstNames = {
    "Alex",
    "John",
    "Alice",
    "Jane",
    "Lexus",
    "Ford",
    "Fred",
    "Bonnie",
    "Chica",
    "Foxy",
    "Fritz",
    "Susie",
    "Jeremy",
    "Vanessa",
    "Sally",
    "Mike",
    "Michael",
    "William",
    "Henry",
    "Edwin",
    "Fiona",
    "Jacqueline",
    "Dolly",
    "Smith",
    "Sandra",
    "Erin",
    "Elijah",
    "Kirby",
    "Jenny",
    "Papyr",
    "Emily",
    "Abby",
    "JJ",
    "AJ",
    "Orwell",
    "Richard"
  };
    public string[] lastNames = {
    "Jameson",
    "Jackson",
    "Porter",
    "Smith",
    "Jackson",
    "Afton",
    "Emily",
    "Murray",
    "Jones",
    "Carr",
    "Brown",
    "Black",
    "Allison",
    "Doe",
    "Nosurname",
    "Ether",
    "Belleston",
    "Orter"
    };
    public string[] sources = {
    "Experts",
    "Experts",
    "Experts",
    "{name}, leading expert on the matter,",
    "Scientists",
    "Scientists",
    "{name}, random person with no relation to the matter,",
    "My mother",
    "{name}",
    "My sleep paralysis demon"
    };
    public string[] jobs = {
    "grape farmer",
    "financier",
    "lumberjack",
    "explosives expert",
    "banker",
    "serial killer",
    "{job} extrordinaire",
    "arsonist",
    "known enemy of the {culprit}",
    "mother",
    "father",
    "parent",
    "blacksmith",
    "friend",
    "office worker",
    "actor",
    "windmill expert",
    "EVIL {job}",
    "sandwich butterer",
    "crop tiller",
    "farmer",
    "doughmaker",
    "hunter",
    "knight",
    "carpenter",
    "construction worker"
    };
    public string[] culprits = {
    "ten thousand bees",
    "ravenous tree-huggers",
    "king of the west",
    "arsonists",
    "drought",
    "black market",
    "recent developments",
    "you",
    "news agency",
    "adventurer's guild",
    "aggressive mining operations",
    "recent landscaping efforts",
    "knights",
    "the sky gods",
    "the sea gods",
    "army of endless ghouls"
    };
    public string[] articles =
    {
        "In press meeting this morning {source} stated that we may be in a {job} shortage due to {culprit}.",
    "{name}, beloved {job}, mysteriously found dead in their apartment. {source} suggests {culprit} to blame.",
    "The {culprit} are coming...",
    "I made a sandwich... wait this isn't news.",
    "Slow news day.",
    "The {job}s and {job2}s have decided to create a shared union.",
    "In press meeting this morning, {source} stated that we may have too many people working as {job}s.",
    "Due to {culprit}, there may be too many bees in the wild.",
    "{name} missing from graveyard. {source} suggests {culprit} to blame.",
    "{name} found in two places at once. {source} suggests {job} to blame",
    "{source} claims that families have been having less than six children.",
    "{source} claims that you shouldn't listen to the news.",
    "{source} claims that windmills aren't effective uses of wind.",
    "{source} claims that the ocean is scary.",
    "{source} claims that in {number} years, {job} may become obsolete.",
    "{source} claims that {name} is a liar and a cheat.",
    "{name}, is that your horse staring out into the ocean?",
    "There are less horses in the ocean than there are on land. {source} claims we may be stupid for thinking otherwise",
    "{source} claims that duplication spells may be possible in {number} years",
    "People who are {job}s found to be less happy.",
    "People who are {job}s found to be happier in life.",
    "{name} ({age}) looking to become a {job}.",
    "{name} ({age}) looking to settle down.",
    "{source} claims that {job} may actually be a front for {culprit}"
    };
 
    public override void Placed()
    {
        //    base.Placed();
        //    string filePathJson = Application.streamingAssetsPath + "/ArticleData.json";
        //    string[] fileText = File.ReadAllLines(filePathJson);
        //    string fileTextOneLine = "";
        //    for (int i = 0; i < fileText.Length; i++)
        //    {
        //        fileTextOneLine += fileText[i];
        //    }
        //    Articles temp = JsonConvert.DeserializeObject<Articles>(fileTextOneLine);
        //    this.firstNames = temp.firstNames;
        //    this.lastNames = temp.lastNames;
        //    this.sources = temp.sources;
        //    this.jobs = temp.jobs;
        //    this.culprits = temp.culprits;
        //    this.articles = temp.articles;
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
            int age = UnityEngine.Random.Range(5, 59);
            if (age < 20) age = UnityEngine.Random.Range(5, 59);
            newArticle = newArticle.Replace("{name}", newName);
            newArticle = newArticle.Replace("{source}", source);
            newArticle = newArticle.Replace("{job}", job);
            newArticle = newArticle.Replace("{culprit}", culprit);
            newArticle = newArticle.Replace("{age}", age.ToString());
            newArticle = newArticle.Replace("{number}", UnityEngine.Random.Range(1, 15).ToString());
            newArticle = newArticle.Replace("2}", "}");
        }
        return newArticle;
    }

    public override void Upkeep()
    {
        string article = ArticleCreation();
        Debug.Log(article);
        GameObject popup = Instantiate(GameObject.FindAnyObjectByType<DataStorage>().UI[0]);
        PopupEvents actualPopup = popup.GetComponent<PopupEvents>();
        actualPopup.UpdatePopUp(article);
        
    }

    

}
