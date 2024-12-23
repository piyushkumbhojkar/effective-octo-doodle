using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : BaseScreen
{

    public override void ShowScreen()
    {
        base.ShowScreen();

        GameManager.Instance.StartGame();
        GameManager.Instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        UIManager.Instance.ShowScreen(ScreenType.ResultScreen);
    }
}
