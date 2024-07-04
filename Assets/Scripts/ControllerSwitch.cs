using System;
using UnityEngine;

public class ControllerSwitch : MonoBehaviour
{
    private const string Player = "Player";
    private const string Damage = "Damage";
    private const string Goal = "Goal";

    [SerializeField] private ControllerBase playerController;
    [SerializeField] private ControllerBase aiController;
    [SerializeField] private Collider2D trigger;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void SwitchController(ControllerType controllerType)
    {
        switch (controllerType)
        {
            case ControllerType.None:
                EnablePlayerController(false);
                EnableAIController(false);
                trigger.tag = Goal;
                break;
            case ControllerType.Player:
                EnablePlayerController(true);
                EnableAIController(false);
                trigger.tag = Player;
                break;
            case ControllerType.AI:
                EnablePlayerController(false);
                EnableAIController(true);
                trigger.tag = Damage;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!trigger.CompareTag(Player)) return;

        if (other.CompareTag(Goal))
        {
            gameManager.GameClear();
        }
        else if (other.CompareTag(Damage))
        {
            gameManager.GameOver();
        }
    }
}