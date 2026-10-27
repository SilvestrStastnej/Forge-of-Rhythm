using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private Dictionary<string, List<string>> quoteDictionary;

    void Awake()
    {
        LoadQuotesFromJson();
    }

    private void LoadQuotesFromJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Text");

        if (jsonFile != null)
        {
            quoteDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonFile.text);
        }
        else
        {
            Debug.LogError("Nada");
        }
    }

    public string GetRandomHelloQuote(string characterId)
    {
        if (quoteDictionary != null && quoteDictionary.TryGetValue(characterId, out List<string> quotes))
        {
            if (quotes.Count > 0)
            {
                int randomIndex = Random.Range(0, 1);
                return quotes[randomIndex];
            }
        }
        return "Filler";
    }

    public string GetRandomByeQuote(string characterId)
    {
        if (quoteDictionary != null && quoteDictionary.TryGetValue(characterId, out List<string> quotes))
        {
            if (quotes.Count > 0)
            {
                int randomIndex = Random.Range(2, quotes.Count);
                return quotes[randomIndex];
            }
        }
        return "Filler";
    }
}