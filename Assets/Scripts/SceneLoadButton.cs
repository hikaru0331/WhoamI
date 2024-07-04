using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] [SceneName] private string sceneName;

    private void Reset()
    {
        TryGetComponent(out button);
    }

    // ReSharper disable once Unity.IncorrectMethodSignature
    // ReSharper disable once UnusedMember.Local
    private async UniTaskVoid Start()
    {
        await button.OnClickAsync(destroyCancellationToken);
        SceneManager.LoadScene(sceneName);
    }
}