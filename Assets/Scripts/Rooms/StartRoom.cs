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

    public void OpenExits()
    {
        _upperEntrance.Open();
        _lowerEntrance.Open();
        _rightEntrance.Open();
        _leftEntrance.Open();
    }
}
