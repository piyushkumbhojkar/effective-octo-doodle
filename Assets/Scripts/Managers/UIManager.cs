using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private GameplayScreen gameplayScreen;
    [SerializeField] private ResultScreen resultScreen;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    public void ShowScreen(ScreenType screenType)
    {
        switch(screenType)
        {
            case ScreenType.MenuScreen:
                {
                    menuScreen.ShowScreen();
                    break;
                }
            case ScreenType.GameplayScreen:
                {
                    gameplayScreen.ShowScreen();
                    break;
                }
            case ScreenType.ResultScreen:
                {
                    resultScreen.ShowScreen();
                    break;
                }
        }
    }
}
