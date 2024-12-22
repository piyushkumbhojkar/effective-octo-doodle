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
    [SerializeField] private Transform flipTransform;

    // Front Side
    [SerializeField] private GameObject frontSide;
    [SerializeField] private TMP_Text valueText;

    //Back Side
    [SerializeField] private GameObject backSide;    

    [HideInInspector] public int CardValue;

    private Action<Card> onCardFlipped;
    private CardState currentState;

    private float flipAnimDuration = 0.1f;
    private Vector3 flipVector = new Vector3(0, 90f, 0);

    public void Setup(int cardValue, Action<Card> callbackOnCardFlipped)
    {
        CardValue = cardValue;
        onCardFlipped = callbackOnCardFlipped;

        cardButton.onClick.AddListener(OnCardButtonClicked);
    }

    public void Flip()
    {
        valueText.text = CardValue.ToString();
        currentState = CardState.Opened;
        FlipCard();
    }

    public void FlipBack()
    {
        currentState = CardState.Closed;
        FlipCard();
    }

    public void SetMatched()
    {
        currentState = CardState.Matched;
        flipTransform.gameObject.SetActive(false);
    }

    private void FlipCard()
    {
        GameObject objectToActivate;
        GameObject objectToDeactivate;

        if(currentState == CardState.Opened)
        {
            objectToActivate = frontSide;
            objectToDeactivate = backSide;
        }
        else
        {
            objectToActivate = backSide;
            objectToDeactivate = frontSide;
        }

        // Flip Animation Logic
        Sequence sequence = DOTween.Sequence();
        sequence.Append(flipTransform.DORotate(flipVector, flipAnimDuration)).OnComplete(() =>
        {
            objectToActivate.SetActive(true);
            objectToDeactivate.SetActive(false);
        });
        sequence.Append(flipTransform.DORotate(Vector3.zero, flipAnimDuration));
        sequence.Play();
    }

    public void OnCardButtonClicked()
    {
        onCardFlipped?.Invoke(this);
    }   
}
