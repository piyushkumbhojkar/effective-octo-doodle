using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Card Setup")]
    [SerializeField] private Button cardButton;
    [SerializeField] private TMP_Text valueText;

    [HideInInspector] public int CardValue;
    [HideInInspector] public bool IsMatched;

    private Action<Card> onCardFlipped;

    public void Setup(int cardValue, Action<Card> callbackOnCardFlipped)
    {
        CardValue = cardValue;
        valueText.text = cardValue.ToString();
        onCardFlipped = callbackOnCardFlipped;
        cardButton.onClick.AddListener(OnCardButtonClicked);
    }

    private void OnCardButtonClicked()
    {
        onCardFlipped?.Invoke(this);
    }
}
