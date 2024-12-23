using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : BaseScreen
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnMenuButtonClicked()
    {
        UIManager.Instance.ShowScreen(ScreenType.MenuScreen);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        scoreText.text = $"Congratulations you scored {GameManager.Instance.CurrentScore}.";
    }
}
