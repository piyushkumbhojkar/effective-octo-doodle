using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardGenerator cardGenerator;
    [SerializeField] private FruitsData fruitsData;
    [SerializeField] private GameData gameData;

    [Header("Timer Setup")]
    [SerializeField] private float matchDuration = 0.25f;

    [Header("Scoring Setup")]
    [SerializeField] private int baseScore = 10;
    [SerializeField] private int bonusMultiplier = 5;

    private int currentScore = 0;
    private int bonusStreak = 0;

    private int currentMatchCount = 0;
    private int totalMatches = 0;

    private Card firstSelectedCard;
    private Card secondSelectedCard;
    private bool matchInProgress = false;

    private Queue<Card> flippedCards = new Queue<Card>();

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public GameData GameData
    {
        get { return gameData; }
    }

    public Action OnGameOver;
    public Action<int> OnScoreUpdate;
    public Action<int> OnMatchCountUpdate;

    public int CurrentScore
    {
        get { return currentScore; }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {       
        if (gameData.IsDataSaved)
        {
            ContinueGame();
        }
        else
        {
            cardGenerator.GenerateCards(OnCardFlipped, fruitsData);

            currentMatchCount = 0;
            totalMatches = cardGenerator.TotalValues;
        }
    }

    public void ContinueGame()
    {
        currentScore = gameData.CurrentScore;
        currentMatchCount = gameData.CurrentMatchCount;

        cardGenerator.GenerateCardsFromPrefs(OnCardFlipped, fruitsData, gameData.CurrentCards);
        totalMatches = cardGenerator.TotalValues;

        OnScoreUpdate?.Invoke(currentScore);
        OnMatchCountUpdate?.Invoke(currentMatchCount);
    }


    private void OnCardFlipped(Card flippedCard)
    {
        Debug.Log($"Flipped Card value: {flippedCard.CardValue}");

        if (flippedCards.Contains(flippedCard))
            return;

        flippedCards.Enqueue(flippedCard);
        flippedCard.Flip();

        if (flippedCards.Count >= 2 && !matchInProgress)
        {
            StartCoroutine(CheckForCardMatch());
        }
    }

    IEnumerator CheckForCardMatch()
    {
        matchInProgress = true;
        
        yield return new WaitForSeconds(matchDuration);

        if (flippedCards.Count >= 2)
        {
            firstSelectedCard = flippedCards.Dequeue();
            secondSelectedCard = flippedCards.Dequeue();

            if(firstSelectedCard.CardValue.FruitType == secondSelectedCard.CardValue.FruitType)
            {
                Debug.Log("It's a match!");

                AudioManager.Instance.PlayAudio(AudioType.CorrectMatch);

                firstSelectedCard.SetMatched();
                secondSelectedCard.SetMatched();
                
                currentMatchCount++;
                OnMatchCountUpdate?.Invoke(currentMatchCount);

                UpdateScore(true);

                if(currentMatchCount >= totalMatches)
                {
                    Debug.Log("Game Over. You matched all the cards!");
                    gameData.ResetData();

                    AudioManager.Instance.PlayAudio(AudioType.GameComplete);
                    OnGameOver?.Invoke();
                }
                else
                {
                    SaveGameData();
                }
            }
            else
            {
                Debug.Log("Wrong match. Try again.");

                AudioManager.Instance.PlayAudio(AudioType.WrongMatch);

                firstSelectedCard.FlipBack();
                secondSelectedCard.FlipBack();

                UpdateScore(false);
            }
        }

        ResetCards();
        matchInProgress = false;
    }

    private void ResetCards()
    {
        firstSelectedCard = null;
        secondSelectedCard = null;
    }

    private void UpdateScore(bool isMatched)
    {
        if (isMatched)
        {
            int bonusScore = bonusStreak * bonusMultiplier;
            int score = baseScore + bonusScore;

            currentScore += score;
            OnScoreUpdate?.Invoke(currentScore);

            bonusStreak++;
        }
        else
        {
            bonusStreak = 0;
        }
    }

    private void SaveGameData()
    {
        gameData.SaveData(currentScore, currentMatchCount, cardGenerator.GeneratedCards);
    }
}
