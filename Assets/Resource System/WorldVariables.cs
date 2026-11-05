using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class WorldVariables : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string WorldName {  get; set; }
    public int food;
    public int wood;
    public int stone;
    public int fuel;
    public int crops;
    public List<string> SpecialMaterials;
    public int AirPollution { get; set; }
    public int WaterPollution { get; set; }
    public int LandPollution { get; set; }
    public float PopulationHappiness { get; set; } = 0.5f; // with world creation, maybe add a setting to choose your starting happiness?
    public int Population { get; set; } = 0;
    public int EmployedPopulation { get; set; } = 0;
    public int RequiredPopulation { get; set; } = 0;
    public int RequiredPollution { get; set; } = 0;
    public int RequiredHappiness { get; set; } = 50;
    public int RequiredInspiration { get; set; } = 0;
    public enum PollutionType { Air, Water, Land }
    public PollutionType ChallengePollutionType { get; set; } = PollutionType.Air;
    public enum ChallengeType { Pollution, Happiness, Population, Inspiration }
    public ChallengeType ChallengePt1 { get; set; } = ChallengeType.Pollution;
    public ChallengeType ChallengePt2 { get; set; } = ChallengeType.Population;
    [SerializeField] public int Inspiration = 0;
    public float baseDamage;
    public float basePermDamage;
    public bool safeDestroy = false;
    public float TimeSinceLastUpkeep = 0;
    [SerializeField] private WorldGen worldGen;
    [SerializeField] private float whenUpdate = 10f;

    public void ChangeSpMats(bool Add, string Material)
    {
        // If Add is true, adds the material to the list of special materials, if it isn't, it removes it.
        // Does nothing if the material is already in the list and Add is true, or if the material is not in the list and Add is false.
        if (Add && !SpecialMaterials.Contains(Material))
        {
            this.SpecialMaterials.Add(Material);
        }
        else if (!Add && SpecialMaterials.Contains(Material))
        {
            this.SpecialMaterials.Remove(Material);
        }
    }
    public void setDayLength(float length)
    {
        whenUpdate = length;
    }
    public float CalculateHappiness(float input)
    {
        // calculates the happiness as a float between 0 and 1, based on the input value, which is clamped between 0 and 100.
        return Mathf.Clamp(input, 0, 100) / 100f;
    }
    public bool HasSpMat(string Material)
    {
        // checks if the material is in the list of special materials
        return SpecialMaterials.Contains(Material);
    }
    public void UpdateInspiration()
    {
        // Get all BuildingObject components in the scene (or adjust as needed)
        BuildingObject[] buildings = FindObjectsByType<BuildingObject>(FindObjectsSortMode.None).ToArray();

        int totalInspiration = 0;

        // Count how many buildings of each name have inspiration
        Dictionary<string, int> buildingCounts = new Dictionary<string, int>();

        // Cache inspiration arrays for each building name to avoid repeated queries
        Dictionary<string, int[]> inspirationCache = new Dictionary<string, int[]>();

        // Count buildings with inspiration
        foreach (var building in buildings)
        {
            if (building.Inspiration != null && building.Inspiration.Length > 0 && building.canProvide)
            {
                if (buildingCounts.ContainsKey(building.name))
                {
                    buildingCounts[building.name]++;
                }
                else
                {
                    buildingCounts[building.name] = 1;
                    inspirationCache[building.name] = building.Inspiration;
                }
            }
        }

        // Calculate total inspiration based on counts and inspiration arrays
        foreach (var kvp in buildingCounts)
        {
            string buildingName = kvp.Key;
            int count = kvp.Value;
            int[] inspirationArray = inspirationCache[buildingName];

            // Loop up to the minimum of count and inspiration array length
            int loops = Mathf.Min(count, inspirationArray.Length);

            for (int i = 0; i < loops; i++)
            {
                totalInspiration += inspirationArray[i];
            }
        }

        this.Inspiration = totalInspiration;
    }
    void TriggerUpkeep()
    {
        BuildingObject[] buildings = FindObjectsByType<BuildingObject>(FindObjectsSortMode.None).ToArray();
        foreach (var building in buildings)
        {
            building.Upkeep();
        }
        UpdateInspiration();
    }
    public void CreateChallenge()
    {
        ChallengePt1 = (ChallengeType)Random.Range(0, 4);
        ChallengePt2 = (ChallengeType)Random.Range(0, 4);
        while (ChallengePt2 == ChallengePt1)
        {
            ChallengePt2 = (ChallengeType)Random.Range(0, 4);
        }
        ChallengeType[] built = { ChallengePt1, ChallengePt2 };
        foreach (ChallengeType challenge in built)
        {
            switch (challenge)
            {
                case ChallengeType.Pollution:
                    ChallengePollutionType = (PollutionType)Random.Range(0, 3);
                    RequiredPollution = Random.Range(0, 25);
                    break;
                case ChallengeType.Happiness:
                    RequiredHappiness = Random.Range(70, 101);
                    break;
                case ChallengeType.Population:
                    RequiredPopulation = Random.Range(500, 1000);
                    break;
                case ChallengeType.Inspiration:
                    RequiredInspiration = Random.Range(30, 51);
                    break;
            }
        }
    }

    public string ChallengeToString()
    {
        string strPt1 = string.Empty;
        string strPt2 = string.Empty;
        switch (ChallengePt1)
        {
            case ChallengeType.Pollution:
                strPt1 = "less than " + RequiredPollution + " " + ChallengePollutionType.ToString();
                break;
            case ChallengeType.Happiness:
                strPt1 = "minimum of " + RequiredHappiness + " total happiness";
                break;
            case ChallengeType.Population:
                strPt1 = "population of " + RequiredPopulation;
                break;
            case ChallengeType.Inspiration:
                strPt1 = "at least " + RequiredInspiration + "inspiration";
                break;
        }
        switch (ChallengePt2)
        {
            case ChallengeType.Pollution:
                strPt2 = "less than " + RequiredPollution + " " + ChallengePollutionType.ToString();
                break;
            case ChallengeType.Happiness:
                strPt2 = "minimum of " + RequiredHappiness + " total happiness";
                break;
            case ChallengeType.Population:
                strPt2 = "population of " + RequiredPopulation;
                break;
            case ChallengeType.Inspiration:
                strPt2 = "at least " + RequiredInspiration + " inspiration";
                break;
        }
        return "Have" + strPt1 + " with " + strPt2;
    }
    // Update is called once per frame
    void Update()
    {
        worldGen.WorldStart();
        //Debug.Log(ChallengeToString());
        if (PopulationHappiness < 0)
        {
            PopulationHappiness = 0;
        }
        else if (PopulationHappiness > 1)
        {
            PopulationHappiness = 1;
        }
    }
    private void FixedUpdate()
    {
        if (TimeSinceLastUpkeep >= whenUpdate)
        {
            TriggerUpkeep();
            TimeSinceLastUpkeep = 0;
        }
        else
        {
            TimeSinceLastUpkeep += Time.deltaTime;
        }
    }
}
