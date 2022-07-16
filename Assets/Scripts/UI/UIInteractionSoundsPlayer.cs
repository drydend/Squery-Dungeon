using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInteractionSoundsPlayer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField]
    private AudioClip _onPointerEnterSound;
    [SerializeField]
    private AudioClip _onPointerDownSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = AudioSourceProvider.Instance.GetSoundsSource();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(_onPointerDownSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(_onPointerEnterSound);
    }
}
