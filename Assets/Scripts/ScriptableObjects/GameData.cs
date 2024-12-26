using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Game Data", order = 2)]
public class GameData : ScriptableObject
{
    public bool IsDataSaved = false;
    public int CurrentScore;
    public int CurrentMatchCount;

    public List<PrefCard> CurrentCards = new();

    private readonly string KeyGameData = "GameData";

    public void SaveData(int score, int matchCount, List<Card> cards)
    {        
        CurrentScore = score;
        CurrentMatchCount = matchCount;

        CurrentCards.Clear();
        foreach (Card card in cards)
        {
            PrefCard pCard = new PrefCard();
            pCard.CardType = card.CardValue.FruitType;
            pCard.IsMatched = card.IsMatched;

            CurrentCards.Add(pCard);
        }

        IsDataSaved = true;

        string prefDataStr = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(KeyGameData, prefDataStr);

        Debug.Log($"Data saved {prefDataStr}");
    }

    public void ResetData()
    {
        IsDataSaved = false;
        CurrentScore = 0;
        CurrentMatchCount = 0;
        CurrentCards.Clear();

        PlayerPrefs.SetString(KeyGameData, string.Empty);
    }

    public void LoadData()
    {
        string prefDataStr = PlayerPrefs.GetString(KeyGameData, string.Empty);
        GameData prefData = JsonUtility.FromJson<GameData>(prefDataStr);

        IsDataSaved = prefData.IsDataSaved;
        CurrentScore = prefData.CurrentScore;
        CurrentMatchCount = prefData.CurrentMatchCount;
        CurrentCards = new List<PrefCard>(prefData.CurrentCards);
    }
}

[Serializable]
public class PrefCard
{
    public FruitType CardType;
    public bool IsMatched;
}