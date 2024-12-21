using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardGenerator cardGenerator;

    // Start is called before the first frame update
    void Start()
    {
        cardGenerator.GenerateCards(OnCardFlipped);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCardFlipped(Card flippedCard)
    {
        Debug.Log($"Flipped Card value: {flippedCard.CardValue}");
    }
}
