using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardGenerator cardGenerator;

    private Card firstCard = null;
    private Card secondCard = null;

    private int currentMatches = 0;
    private int totalMatches = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        if(firstCard == null)
        {
            firstCard = flippedCard;
        }
        else
        {
            secondCard = flippedCard;   

            if(firstCard.CardValue == secondCard.CardValue)
            {
                Debug.Log("Found a match!");

                firstCard.SetMatched();
                secondCard.SetMatched();  
                currentMatches++;

                if(currentMatches >= totalMatches)
                {
                    Debug.Log("Wow. You finished the game.");
                }
            }
            else
            {
                Debug.Log("Try again!");
            }

            ResetCards();
        }
    }

    private void ResetCards()
    {
        firstCard = null;
        secondCard = null;
    }
}
