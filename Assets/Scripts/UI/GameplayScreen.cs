using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayScreen : BaseScreen
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text matcheCountText;

    public override void ShowScreen()
    {
        base.ShowScreen();

        ResetScreen();

        GameManager.Instance.StartGame();
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnScoreUpdate += OnScoreUpdate;
        GameManager.Instance.OnMatchCountUpdate += OnMatchCountUpdate;
    }

    private void ResetScreen()
    {
        OnScoreUpdate(0);
        OnMatchCountUpdate(0);
    }

    private void OnGameOver()
    {
        UIManager.Instance.ShowScreen(ScreenType.ResultScreen);
    }

    private void OnScoreUpdate(int currentScore)
    {
        scoreText.text = $"Score: {currentScore}";
    }

    private void OnMatchCountUpdate(int currentMatchCount)
    {
        matcheCountText.text = $"Match Count: {currentMatchCount}";
    }
}
