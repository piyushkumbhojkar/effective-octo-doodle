using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
    [Header("Card Setup")]
    [SerializeField] private Card card;
    [SerializeField] private Transform cardsParent;
    [SerializeField] private GridLayoutGroup gridLayout;

    [Header("Grid Setup")]
    [SerializeField] int noOfRows = 2;
    [SerializeField] int noOfColumns = 2;

    private List<Card> generatedCards = new();
    private Action<Card> onCardFlipped;

    public int TotalValues = 0;

    public void GenerateCards(Action<Card> onCardFlipped)
    {
        List<int> cardValues = GenerdateCardValues();
        SetupCards(cardValues, onCardFlipped);
    }

    private List<int> GenerdateCardValues()
    {
        int totalCards = noOfRows * noOfColumns;
        int totalValues = totalCards / 2;
        TotalValues = totalValues;

        List<int> cardValues = new();

        for (int i = 0; i < totalValues; i++)
        {
            cardValues.Add(i);
            cardValues.Add(i);
        }

        cardValues.Shuffle();

        return cardValues;
    }

    private void SetupCards(List<int> cardValues, Action<Card> onCardFlipped)
    {
        int valueIndex = 0;
        gridLayout.constraintCount = noOfRows;

        for (int row = 0; row < noOfRows; row++)
        {
            for (int column = 0; column < noOfColumns; column++)
            {
                Card newCard = Instantiate(card, gridLayout.transform);
                newCard.name = $"Card_{row}_{column}";
                newCard.Setup(cardValues[valueIndex++], onCardFlipped);

                generatedCards.Add(newCard);
            }
        }
    }
}
