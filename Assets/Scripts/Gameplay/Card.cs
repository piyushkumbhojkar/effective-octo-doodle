using DG.Tweening;
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
        valueText.text = "Open";
        onCardFlipped = callbackOnCardFlipped;
        cardButton.onClick.AddListener(OnCardButtonClicked);
    }

    public void SetFlipped()
    {
        valueText.text = CardValue.ToString();
        cardButton.interactable = false;
    }

    public void SetMatched()
    {
        cardButton.interactable = false;
    }

    public void ResetCard()
    {
        valueText.text = "Open";
        cardButton.interactable = true;
    }

    private void OnCardButtonClicked()
    {
        onCardFlipped?.Invoke(this);
    }   
}
