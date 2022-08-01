using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class EnergyBarUI : MonoBehaviour
{
    private const string ShakingAnimationTrigger = "Shaking trigger";

    [SerializeField]
    private Player _player;
    [SerializeField]
    private Slider _slider;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player.OnDontHaveEnoungtEnergy += ShakeBar;
    }

    private void Update()
    {
        _slider.value = _player.CurrentEnergy / _player.MaxEnergy;
    }

    private void ShakeBar()
    {
        _animator.SetTrigger(ShakingAnimationTrigger);
    }
}

