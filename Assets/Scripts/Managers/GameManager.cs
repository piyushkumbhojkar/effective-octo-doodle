using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardGenerator cardGenerator;
    [SerializeField] private FruitsData fruitsData;

    [Header("Timer Setup")]
    [SerializeField] private float matchDuration = 0.25f;

    private int currentMatches = 0;
    private int totalMatches = 0;

    private Card firstSelectedCard;
    private Card secondSelectedCard;
    private bool matchInProgress = false;

    private Queue<Card> flippedCards = new Queue<Card>();

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {       
        cardGenerator.GenerateCards(OnCardFlipped, fruitsData);

        currentMatches = 0;
        totalMatches = cardGenerator.TotalValues;
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

                firstSelectedCard.SetMatched();
                secondSelectedCard.SetMatched();
                currentMatches++;

                if(currentMatches >= totalMatches)
                {
                    Debug.Log("Game Over. You matched all the cards!");
                }
            }
            else
            {
                Debug.Log("Wrong match. Try again.");

                firstSelectedCard.FlipBack();
                secondSelectedCard.FlipBack();
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
}
