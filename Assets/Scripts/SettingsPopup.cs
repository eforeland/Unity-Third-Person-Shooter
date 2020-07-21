using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsPopup : BasePopup
{
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private Slider slider;
    [SerializeField] private Text numEnemiesValue;
    
    override public void Open()
    {
        base.Open();
        slider.value = PlayerPrefs.GetInt("difficulty", 1);
    }

    public void OnOKButton()
    {
        PlayerPrefs.SetInt("difficulty", (int)slider.value);
        Messenger<int>.Broadcast(GameEvent.DIFFICULTY_CHANGED, (int)slider.value);
        Close();
        optionsPopup.Open();
    }

    public void OnCancelButton()
    {
        Close();
        optionsPopup.Open();
    }

    public void OnDifficultyValue(float difficulty)
    {
        numEnemiesValue.text = difficulty.ToString();
    }

    
}
