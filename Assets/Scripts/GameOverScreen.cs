using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : BasePopup
{

    public void OnExitGameButton()
    {
        Application.Quit();
    }

    public void OnStartAgainButton()
    {
        Messenger.Broadcast(GameEvent.RESTART_GAME);
    }
}
