using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ControllerInputScript : MonoBehaviour
{
    private InputDevice inputDevice;

    // Update is called once per frame
    private void Awake()
    {
        inputDevice = InputManager.Devices[0];
    }
    //void Update()
    //{
    //    inputDevice = InputManager.Devices[playerNumber];
    //}

    public float getLeftStickX()
    {
        return inputDevice.LeftStickX;
    }

    public float getLeftStickY()
    {
        return inputDevice.LeftStickY;
    }

    public float getRightStickX()
    {
        return inputDevice.RightStickX;
    }

    public float getRightStickY()
    {
        return inputDevice.RightStickY;
    }

    public bool getRightTrigger()
    {
        return inputDevice.GetControl(InputControlType.RightTrigger).IsPressed;
    }

    public bool getRightTriggerDown()
    {
        return inputDevice.GetControl(InputControlType.RightTrigger).WasPressed;
    }

    public bool getRightTriggerUp()
    {
        return inputDevice.GetControl(InputControlType.RightTrigger).WasReleased;
    }

    public bool getLeftTriggerDown()
    {
        return inputDevice.GetControl(InputControlType.LeftTrigger).WasPressed;
    }

    public bool getLeftTriggerUp()
    {
        return inputDevice.GetControl(InputControlType.LeftTrigger).WasReleased;
    }

    public bool getLeftBumperDown()
    {
        return inputDevice.GetControl(InputControlType.LeftBumper).WasPressed;
    }

    public bool getRightBumperDown()
    {
        return inputDevice.GetControl(InputControlType.RightBumper).WasPressed;
    }

    public bool getDPadDownDown()
    {
        return inputDevice.GetControl(InputControlType.DPadDown).WasPressed;
    }

    public bool getADown() 
    {
        return inputDevice.GetControl(InputControlType.Action1).WasPressed;
    }

    public bool getStartDown()
    {
        return inputDevice.GetControl(InputControlType.Start).IsPressed;
    }

    public bool getStartReleased()
    {
        return inputDevice.GetControl(InputControlType.Start).WasReleased;
    }
}