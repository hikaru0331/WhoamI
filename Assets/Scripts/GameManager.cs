using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Goal goal;
    public DeadZone deadZone;
    [SceneName] public string clearScene;
    [SceneName] public string overScene;

    private void Start()
    {
        goal.OnTriggerEnter2DAsObservable()
            .Where(col => col.attachedRigidbody && col.attachedRigidbody.CompareTag("Player"))
            .Subscribe(_ => GameClear())
            .RegisterTo(destroyCancellationToken);

        deadZone.OnTriggerEnter2DAsObservable()
            .Where(col => col.attachedRigidbody && col.attachedRigidbody.CompareTag("Player"))
            .Subscribe(_ => GameOver())
            .RegisterTo(destroyCancellationToken);
    }

    private void GameClear()
    {
        SceneManager.LoadScene(clearScene);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(overScene);
    }
}