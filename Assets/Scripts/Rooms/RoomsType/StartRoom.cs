using System;
using UnityEngine;

public class StartRoom : Room
{
    [SerializeField]
    private Transform _playerStartPosition;

    public Transform PlayerStartPosition => _playerStartPosition;

    public void Start()
    {
        OpenExits();
        OnRoomEntered();
        OnRoomCompleated();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            OnRoomEntered();
        }
    }

    public void OpenExits()
    {
        _upperEntrance.Open();
        _lowerEntrance.Open();
        _rightEntrance.Open();
        _leftEntrance.Open();
    }
}
