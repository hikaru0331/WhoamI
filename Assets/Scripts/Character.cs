using System;
using R3;
using UnityEngine;

[Serializable]
public class Character
{
    public ControllerType type;
    public ControllerSwitch controller;
    public CharacterPanel panel;
    public Observable<(CharacterPanel ownPanel, CharacterPanel otherPanel)> OnSwapObservable =>
        panel != null ? panel.OnSwapObservable : null;

    private IAIController aiControllerCache;

    public void Setup()
    {
        controller.SwitchController(type);
    }

    public void ChangeType(ControllerType controllerType)
    {
        type = controllerType;
        controller.SwitchController(controllerType);
    }

    public void SetTarget(Transform target)
    {
        if (IsNull(aiControllerCache) && controller.TryGetComponent(out IAIController aiController))
        {
            aiControllerCache = aiController;
        }

        if (!IsNull(aiControllerCache))
        {
            aiControllerCache.SetTarget(target);
        }
    }

    private bool IsNull<T>(T component) where T : class
    {
        if (component is null) return true;

        return component is MonoBehaviour monoBehaviour && monoBehaviour == null;
    }
}