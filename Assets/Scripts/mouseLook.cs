using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX  = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHorz = 9.0f;
    public float sensitivityVert = 9.0f;
    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    private float rotationX = 0.0f;
    int open = 0;

    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.UI_POPUP_OPENED, OnPopupOpen);
        Messenger<int>.AddListener(GameEvent.UI_POPUP_CLOSED, OnPopupClosed);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.UI_POPUP_OPENED, OnPopupOpen);
        Messenger<int>.RemoveListener(GameEvent.UI_POPUP_CLOSED, OnPopupClosed);
    }

    void OnPopupOpen(int numOpen)
    {
        open = numOpen;
    }

    void OnPopupClosed(int numOpen)
    {
        open = numOpen;
    }
    // Update is called once per frame
    void Update()
    {
        if (open == 0)
        {
            if (axes == RotationAxes.MouseX)
            {
                //horizontal roation here
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorz, 0);

            }
            else if (axes == RotationAxes.MouseY)
            {
                //vertical rotation here
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

                transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            }
            else
            {
                // both vertical; and horizontal here
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

                float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHorz;
                float rotationY = transform.localEulerAngles.y + deltaHoriz;

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
           
        }
    }
}
