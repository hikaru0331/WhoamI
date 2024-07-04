using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public sealed class GameManager : MonoBehaviour
{
    [SceneName] public string clearScene;
    [SceneName] public string overScene;

    public void GameClear()
    {
        SceneManager.LoadScene(clearScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(overScene);
    }
}