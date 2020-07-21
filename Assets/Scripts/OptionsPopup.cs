using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : BasePopup
{
    //[SerializeField] private UIController uiController;
    [SerializeField] public SettingsPopup settingsPopup;

    public void OnSettingsButton()
    {
        Close();
        settingsPopup.Open();
    }

    public void OnExitGameButton()
    {
        Application.Quit();
    }

    public void OnReturnToGameButton()
    {
        Close();
    }

}
