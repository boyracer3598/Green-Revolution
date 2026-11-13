using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine.Serialization;
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
    public float PopulationHappiness { get; set; } = 0.3f; // with world creation, maybe add a setting to choose your starting happiness?
    public int Population { get; set; } = 0;
    public int EmployedPopulation { get; set; } = 0;
    public int IllPopulation { get; set; } = 0;
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
    public BuildingObject NormalBuilding;
    [SerializeField] private float whenUpdate = 30f;
    public float DefaultWhenUpdate = 30f;
    [SerializeField] private float LandPollutionHappinessDampener = 1f; // Dampens happiness based on land pollution, 1 = 1% happiness lost per point of land pollution
    [SerializeField] private float AirPollutionProduceDampener = 0.1f; // Chance for buildings to not produce due to air pollution, 0.1 = 0.1% chance per point of air pollution
    [SerializeField] private float WaterPollutionSicknessChance = 0.1f; // The chance for the population to get sick based on water pollution, 0.1 = 0.1% chance per point of water pollution
    public float HappinessFromNature = 0;
    public float HappinessLost = 0;
    public double sickChance = 0.5;
    public bool hasWon = false;
    [SerializeField] private AudioClip winSound;
    public void ChangeSpMats(bool Add, string Material)
    {
        // If Add is true, adds the material to the list of special materials, if it isn't, it removes it.
        // Does nothing if the material is already in the list and Add is true, or if the material is not in the list and Add is false.
        string[] MaterialArray = new string[0];
        MaterialArray = Material.Split(',');
        foreach (string MaterialString in MaterialArray)
        { 
            string thisMat = MaterialString.Replace(" ", "");
            if (Add && !SpecialMaterials.Contains(thisMat))
            {
                this.SpecialMaterials.Add(thisMat);
            }
            else if (!Add && SpecialMaterials.Contains(thisMat))
            {
                this.SpecialMaterials.Remove(thisMat);
            }
        }
    }
    public void setDayLength(float length)
    {
        whenUpdate = length;
    }
    public float CalculateHappiness(float input)
    {
        // based on the current land pollution, calculates a dampened happiness value. The more land pollution, the lower the happiness.
        float dampener = 1f - LandPollution * LandPollutionHappinessDampener * 0.01f;
        float happy = input * dampener * (1f - (HappinessLost / HappinessFromNature) / 10f); // Reduce happiness based on lost happiness and pollution dampening
        float normalizedHappiness = Mathf.Clamp(happy, 0, 100) / 100f; // Normalize to a 0-1 scale
        return normalizedHappiness;
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

        // Count buildings with inspiration
        foreach (var building in buildings)
        {
            if (building.Inspiration != 0)
            {
                totalInspiration += building.Inspiration;
                
            }
        }
        this.Inspiration = totalInspiration;
    }
    void TriggerUpkeep()
    {
        float newHappiness = 0f;
        BuildingObject[] buildings = FindObjectsByType<BuildingObject>(FindObjectsSortMode.None).ToArray();
        float sickPpl = UnityEngine.Random.Range(0, (2 * WaterPollution * WaterPollutionSicknessChance * Population)); // Chance for population to get sick based on water pollution
        IllPopulation += Mathf.FloorToInt(sickPpl); // Chance for population to get sick based on water pollution
        foreach (var building in buildings)
        {
            System.Random rand = new System.Random();
            if (rand.Next(0, 100) > (AirPollution * AirPollutionProduceDampener)) // Chance for buildings to not produce due to air pollution, 0.1 = 0.1% chance per point of air pollution
            {
                building.Upkeep();
            }
            if (building.canProvide)
            {
                newHappiness += building.populationHappiness;
            }
        }
        PopulationHappiness = CalculateHappiness(30 + newHappiness);
        PopulationHappiness = CalculateHappiness(30 + newHappiness);
    }
    public void CreateChallenge()
    {
        ChallengePt1 = ChallengeType.Population;
        ChallengePt2 = (ChallengeType)UnityEngine.Random.Range(0, 4);
        while (ChallengePt2 == ChallengePt1)
        {
            ChallengePt2 = (ChallengeType)UnityEngine.Random.Range(0, 4);
        }
        ChallengeType[] built = { ChallengePt1, ChallengePt2 };
        foreach (ChallengeType challenge in built)
        {
            switch (challenge)
            {
                case ChallengeType.Pollution:
                    ChallengePollutionType = (PollutionType)UnityEngine.Random.Range(0, 3);
                    RequiredPollution = 5 * UnityEngine.Random.Range(0, 5);
                    break;
                case ChallengeType.Happiness:
                    RequiredHappiness = 5 * UnityEngine.Random.Range(12, 16);
                    break;
                case ChallengeType.Population:
                    RequiredPopulation = 10 * UnityEngine.Random.Range(5, 25);
                    break;
                case ChallengeType.Inspiration:
                    RequiredInspiration = 5 * UnityEngine.Random.Range(4, 10);
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
                strPt1 = "less than " + RequiredPollution + " " + ChallengePollutionType.ToString() + " pollution";
                break;
            case ChallengeType.Happiness:
                strPt1 = "minimum of " + RequiredHappiness + " total happiness";
                break;
            case ChallengeType.Population:
                strPt1 = "population of " + RequiredPopulation;
                break;
            case ChallengeType.Inspiration:
                strPt1 = "at least " + RequiredInspiration + " inspiration";
                break;
        }
        switch (ChallengePt2)
        {
            case ChallengeType.Pollution:
                strPt2 = "less than " + RequiredPollution + " " + ChallengePollutionType.ToString() + " pollution";
                break;
            case ChallengeType.Happiness:
                strPt2 = "minimum of " + RequiredHappiness + " total happiness";
                break;
            case ChallengeType.Population:
                strPt2 = "a population of " + RequiredPopulation;
                break;
            case ChallengeType.Inspiration:
                strPt2 = "at least " + RequiredInspiration + " inspiration";
                break;
        }
        return "Have " + strPt1 + " with " + strPt2;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("SlowMode") != whenUpdate)
        {
            if (PlayerPrefs.GetFloat("SlowMode") <= 0)
            {
                PlayerPrefs.SetFloat("SlowMode", DefaultWhenUpdate);
            }
            else
            {
                whenUpdate = PlayerPrefs.GetFloat("SlowMode");
            }
            
        }

        worldGen.WorldStart();
        if (!hasWon)
        {
            bool[] challengeMet = {
                ChallengePt1 == ChallengeType.Pollution ? (ChallengePollutionType == PollutionType.Air ? AirPollution < RequiredPollution : ChallengePollutionType == PollutionType.Water ? WaterPollution < RequiredPollution : LandPollution < RequiredPollution) : true,
                ChallengePt1 == ChallengeType.Happiness ? PopulationHappiness >= (RequiredHappiness / 100f) : true,
                ChallengePt1 == ChallengeType.Population ? Population >= RequiredPopulation : true,
                ChallengePt1 == ChallengeType.Inspiration ? Inspiration >= RequiredInspiration : true,
                ChallengePt2 == ChallengeType.Pollution ? (ChallengePollutionType == PollutionType.Air ? AirPollution < RequiredPollution : ChallengePollutionType == PollutionType.Water ? WaterPollution < RequiredPollution : LandPollution < RequiredPollution) : true,
                ChallengePt2 == ChallengeType.Happiness ? PopulationHappiness >= (RequiredHappiness / 100f) : true,
                ChallengePt2 == ChallengeType.Population ? Population >= RequiredPopulation : true,
                ChallengePt2 == ChallengeType.Inspiration ? Inspiration >= RequiredInspiration : true
            };
            hasWon = challengeMet.All(met => met);
            if (hasWon)
            {
                // this.PopupCreate("Challenge Completed! Would you like to continue?");
                SoundManager.instance.PlaySoundClip(winSound,this.transform,1);
                //creates a popup object to show the popup
                GameObject popup = Instantiate(GameObject.FindAnyObjectByType<DataStorage>().UI[0]);
                PopupEvents actualPopup = popup.GetComponent<PopupEvents>();
                actualPopup.UpdatePopUp("Challenge completed \n Congratulations! You won!");
                Debug.Log("Challenge Completed");
            }
        }
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
