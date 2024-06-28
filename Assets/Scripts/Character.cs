using System;
using R3;

[Serializable]
public class Character
{
    public ControllerType type;
    public ControllerSwitch controller;
    public CharacterPanel panel;
    public Observable<(CharacterPanel ownPanel, CharacterPanel otherPanel)> OnSwapObservable =>
        panel != null ? panel.OnSwapObservable : null;

    public void Setup()
    {
        controller.SwitchController(type);
    }

    public void ChangeType(ControllerType controllerType)
    {
        type = controllerType;
        controller.SwitchController(controllerType);
    }
}