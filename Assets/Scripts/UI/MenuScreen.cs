using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button playButton;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();

        bool isDataSaved = GameManager.Instance.GameData.IsDataSaved;
        continueButton.gameObject.SetActive(isDataSaved);
    }

    private void OnContinueButtonClicked()
    {
        UIManager.Instance.ShowScreen(ScreenType.GameplayScreen);
    }

    private void OnPlayButtonClicked()
    {
        GameManager.Instance.GameData.ResetData();
        UIManager.Instance.ShowScreen(ScreenType.GameplayScreen);
    }
}
