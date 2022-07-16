using UnityEngine;
using UnityEngine.UI;

public class DeathScrene : UIMenu
{
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _exitButton;

    [SerializeField]
    private ScreneFade _screneFade;
    [SerializeField]
    [Range(0, 1f)]
    private float _screneFadeIntencity;
    [SerializeField]
    private SceneTransition _sceneTransition;

    [SerializeField]
    private GameObject _screne;

    public override bool CanBeClosed { get => false; set { } }

    public override void Close()
    {
        
    }

    public override void Initialize()
    {
        _restartButton.onClick.AddListener(() => _sceneTransition.RestartScene());
        _exitButton.onClick.AddListener(() => _sceneTransition.SwitchToMainMenu());
    }

    public override void OnCovered()
    {
        
    }

    public override void Open()
    {
        _screne.SetActive(true);
        _screneFade.Fade(_screneFadeIntencity);
    }
}

