using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    private const string MainMenuSceneName = "Main Menu";

    [SerializeField]
    private ScreneFade _screneFade;

    public void SwitchToScene(string name)
    {
        StartCoroutine(SceneLoadCoroutine(name));
    }

    public void RestartScene()
    {
        SwitchToScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchToMainMenu()
    {
        SwitchToScene(MainMenuSceneName);
    }

    private IEnumerator SceneLoadCoroutine(string name)
    {
        _screneFade.Fade();
        yield return new WaitUntil(() => _screneFade.IsAnimated == false);
        SceneManager.LoadScene(name);
    }
}
