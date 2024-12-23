using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private GameplayScreen gameplayScreen;
    [SerializeField] private ResultScreen resultScreen;

    private BaseScreen currentScreen = null;
    private BaseScreen previousScreen = null;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    public void Start()
    {
        ShowScreen(ScreenType.MenuScreen);
    }

    public void ShowScreen(ScreenType screenType)
    {
        previousScreen = currentScreen;

        switch (screenType)
        {
            case ScreenType.MenuScreen:
                {
                    currentScreen = menuScreen;
                    break;
                }
            case ScreenType.GameplayScreen:
                {
                    currentScreen = gameplayScreen;
                    break;
                }
            case ScreenType.ResultScreen:
                {
                    currentScreen = resultScreen;
                    break;
                }
        }

        if (currentScreen != null)
        {
            if (previousScreen != null)
            {
                previousScreen.HideSceen();
            }

            currentScreen.ShowScreen();
        }
    }
}
