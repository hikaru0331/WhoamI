using System;
using UnityEngine;

public class ControllerSwitch : MonoBehaviour
{
    [SerializeField] private ControllerBase playerController;
    [SerializeField] private ControllerBase aiController;

    public void SwitchController(ControllerType controllerType)
    {
        switch (controllerType)
        {
            case ControllerType.None:
                EnablePlayerController(false);
                EnableAIController(false);
                break;
            case ControllerType.Player:
                EnablePlayerController(true);
                EnableAIController(false);
                break;
            case ControllerType.AI:
                EnablePlayerController(false);
                EnableAIController(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(controllerType), controllerType, null);
        }
    }

    private void EnablePlayerController(bool enable)
    {
        if (playerController)
        {
            playerController.enabled = enable;
        }
    }

    private void EnableAIController(bool enable)
    {
        if (aiController)
        {
            aiController.enabled = enable;
        }
    }
}