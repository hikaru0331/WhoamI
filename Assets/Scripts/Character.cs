using System;

[Serializable]
public class Character
{
    public ControllerType type;
    public ControllerSwitch controller;
    public CharacterPanel panel;

    public void ChangeType(ControllerType controllerType)
    {
        type = controllerType;
        controller.SwitchController(controllerType);
    }
}