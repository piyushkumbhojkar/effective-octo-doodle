using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardGenerator cardGenerator;

    private int currentMatches = 0;
    private int totalMatches = 0;

    private Card firstFlippedCard;
    private Card secondFlippedCard;
    private bool matchInProgress = false;

    private Queue<Card> flippedCards = new Queue<Card>();

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {       
        cardGenerator.GenerateCards(OnCardFlipped);

        currentMatches = 0;
        totalMatches = cardGenerator.TotalValues;
    }

    private void OnCardFlipped(Card flippedCard)
    {
        Debug.Log($"Flipped Card value: {flippedCard.CardValue}");

        if (flippedCards.Contains(flippedCard))
            return;

        flippedCards.Enqueue(flippedCard);
        flippedCard.SetFlipped();

        if (flippedCards.Count >= 2 && !matchInProgress)
        {
            StartCoroutine(CheckForCardMatch());
        }
    }

    IEnumerator CheckForCardMatch()
    {
        matchInProgress = true;
        
        yield return new WaitForSeconds(0.5f);

        if (flippedCards.Count >= 2)
        {
            firstFlippedCard = flippedCards.Dequeue();
            secondFlippedCard = flippedCards.Dequeue();

            if(firstFlippedCard.CardValue == secondFlippedCard.CardValue)
            {
                Debug.Log("It's a match!");

                firstFlippedCard.SetMatched();
                secondFlippedCard.SetMatched();
                currentMatches++;

                if(currentMatches >= totalMatches)
                {
                    Debug.Log("Game Over. You matched all the cards!");
                }
            }
            else
            {
                Debug.Log("Wrong match. Try again.");

                firstFlippedCard.ResetCard();
                secondFlippedCard.ResetCard();
            }
        }

        ResetCards();
        matchInProgress = false;
    }

    private void ResetCards()
    {
        firstFlippedCard = null;
        secondFlippedCard = null;
    }
}
