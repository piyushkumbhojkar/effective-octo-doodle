using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : BaseScreen
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        UIManager.Instance.ShowScreen(ScreenType.GameplayScreen);
    }
}
