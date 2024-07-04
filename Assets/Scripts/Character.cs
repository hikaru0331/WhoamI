using System;
using Cinemachine;
using R3;
using UnityEngine;

[Serializable]
public class Character
{
    public ControllerType type;
    public ControllerSwitch controller;
    public CharacterPanel panel;
    public CinemachineVirtualCamera virtualCamera;
    public Observable<(CharacterPanel ownPanel, CharacterPanel otherPanel)> OnSwapObservable =>
        panel != null ? panel.OnSwapObservable : null;

    private IAIController aiControllerCache;

    // ReSharper disable once ParameterHidesMember
    public void Setup(CinemachineVirtualCamera virtualCamera)
    {
        this.virtualCamera = virtualCamera;
        this.virtualCamera.name = $"VirtualCamera_{controller.name}";
        this.virtualCamera.Follow = controller.transform;
        controller.SwitchController(type);
        this.virtualCamera.m_Priority = type == ControllerType.Player ? 10 : 0;
    }

    public void ChangeType(ControllerType controllerType)
    {
        type = controllerType;
        controller.SwitchController(controllerType);
        virtualCamera.m_Priority = type == ControllerType.Player ? 10 : 0;
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