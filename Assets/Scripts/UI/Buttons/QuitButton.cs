using UnityEngine.UI;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}

