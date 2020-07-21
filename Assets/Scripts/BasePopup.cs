using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    protected int numOpen = 0;

    virtual public void Open()
    {
        if (!IsActive())
        {
            this.gameObject.SetActive(true);
            Messenger.Broadcast(GameEvent.POPUP_OPENED);
        } else
        {
            Debug.LogError(this + ".open - already open");
        }
        
    }

    virtual public void Close()
    {
        if (IsActive())
        {
            this.gameObject.SetActive(false);
            Messenger.Broadcast(GameEvent.POPUP_CLOSED);
        } else
        {
            Debug.LogError(this + ".close - already close");
        }
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }
   
}
