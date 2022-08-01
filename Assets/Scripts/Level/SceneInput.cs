using UnityEngine;
using UnityEngine.EventSystems;

public class SceneInput : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private EventSystem _eventSystem;

    public void OffAllInput()
    {   
        _playerInput?.gameObject.SetActive(false);
        _eventSystem?.gameObject.SetActive(false);
    }

    public void OnAllInput()
    {
        _playerInput?.gameObject.SetActive(true);
        _eventSystem?.gameObject.SetActive(true);
    }
}

