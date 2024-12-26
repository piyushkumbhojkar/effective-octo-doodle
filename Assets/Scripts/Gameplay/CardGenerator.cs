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

    [HideInInspector] public int TotalValues = 0;
    [HideInInspector]
    public List<Card> GeneratedCards
    {
        get { return generatedCards; }
    }

    private List<Card> generatedCards = new();
    private FruitsData fruitsData;

    private Pool<Card> cardPool = new();

    private void Awake()
    {
        cardPool.Initialize(card);
    }

    private void UpdateValues()
    {
        int totalCards = noOfRows * noOfColumns;
        int totalValues = totalCards / 2;
        TotalValues = totalValues;
    }

    public void GenerateCards(Action<Card> onCardFlipped, FruitsData data)
    {
        cardPool.ResetPool();
        generatedCards.Clear();

        UpdateValues();

        fruitsData = data;
        List<int> cardValues = GenerdateCardValues();
        SetupCards(cardValues, onCardFlipped);
    }

    private List<int> GenerdateCardValues()
    {
        List<int> cardValues = new();

        for (int i = 0; i < TotalValues; i++)
        {
            cardValues.Add(i);
            cardValues.Add(i);
        }

        cardValues.Shuffle();

        return cardValues;
    }

    private Fruit GetFruitData(int index)
    {
        return fruitsData.fruits[index];
    }

    private void SetupCards(List<int> cardValues, Action<Card> onCardFlipped)
    {
        int valueIndex = 0;
        gridLayout.constraintCount = noOfRows;

        for (int row = 0; row < noOfRows; row++)
        {
            for (int column = 0; column < noOfColumns; column++)
            {
                Card newCard = cardPool.GetObject();
                newCard.transform.SetParent(cardsParent);
                newCard.name = $"Card_{row}_{column}";

                Fruit data = GetFruitData(cardValues[valueIndex++]);
                newCard.Setup(data, onCardFlipped);

                generatedCards.Add(newCard);
            }
        }
    }

    #region Load Game Data

    public void GenerateCardsFromPrefs(Action<Card> onCardFlipped, FruitsData data, List<PrefCard> prefCards)
    {
        cardPool.ResetPool();
        generatedCards.Clear();

        UpdateValues();

        fruitsData = data;
        SetupCards(prefCards, onCardFlipped);
    }

    private Fruit GetFruitData(FruitType type)
    {
        return fruitsData.fruits.Find(fruit => fruit.FruitType == type);
    }

    private void SetupCards(List<PrefCard> cardValues, Action<Card> onCardFlipped)
    {
        int valueIndex = 0;
        gridLayout.constraintCount = noOfRows;

        for (int row = 0; row < noOfRows; row++)
        {
            for (int column = 0; column < noOfColumns; column++)
            {
                Card newCard = cardPool.GetObject();
                newCard.transform.SetParent(cardsParent);
                newCard.name = $"Card_{row}_{column}";

                PrefCard prefData = cardValues[valueIndex++];
                Fruit data = GetFruitData(prefData.CardType);
                newCard.Setup(data, onCardFlipped, prefData.IsMatched);

                generatedCards.Add(newCard);
            }
        }
    }

    #endregion
}
