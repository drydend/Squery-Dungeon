﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{   
    protected Vector2Int _mapPosition;

    [SerializeField]
    protected Sprite _minimapIcon;
    [SerializeField]
    protected RoomEntrance _upperEntrance;
    [SerializeField]
    protected RoomEntrance _lowerEntrance;
    [SerializeField]
    protected RoomEntrance _rightEntrance;
    [SerializeField]
    protected RoomEntrance _leftEntrance;
    [SerializeField]
    protected List<Trap> _traps;

    protected List<Room> _connectedRooms = new List<Room>();
    
    [SerializeField]
    [Range(1, 4)]
    private int _connectionsNumber = 4;
    [SerializeField]
    private int _roomID;

    private bool _canBeConnected = true;
    private int _distanceFromStartRoom;

    public virtual event Action OnEntered;
    public virtual event Action OnCompleated;

    public int DistanceFromStartRoom => _distanceFromStartRoom;
    public List<Room> ConnectedRooms => _connectedRooms;
    public Sprite MinimapIcon => _minimapIcon;
    public Vector2Int MapPoistion => _mapPosition;
    public bool CanBeConnected => _canBeConnected;

    public virtual void Initialize(Vector2Int mapPosition, int distanceFromStartRoom)
    {
        _mapPosition = mapPosition;
        _distanceFromStartRoom = distanceFromStartRoom;
        _upperEntrance.Block();
        _rightEntrance.Block();
        _leftEntrance.Block();
        _lowerEntrance.Block();
    }

    public void ConnectToRoom(Room room, GameObject passage)
    {
        room._connectedRooms.Add(this);
        _connectedRooms.Add(room);

        var directionToRoom = GetDirectionToRoom(room);

        var passagePosition = transform.position + (room.transform.position - transform.position) / 2;
        var passageRotation = (room.MapPoistion - MapPoistion).x == 0 ? 90 : 0;

        Instantiate(passage, passagePosition, Quaternion.Euler(0, 0, passageRotation));
        
        _connectionsNumber--;
        if(_connectionsNumber == 0)
        {
            _canBeConnected = false;
        }

        if (directionToRoom == Vector2Int.up)
        {
            _upperEntrance.Unblock();
            room._lowerEntrance.Unblock();
        }
        else if (directionToRoom == Vector2Int.down)
        {
            _lowerEntrance.Unblock();
            room._upperEntrance.Unblock();
        }
        else if (directionToRoom == Vector2Int.right)
        {
            _rightEntrance.Unblock();
            room._leftEntrance.Unblock();
        }
        else if (directionToRoom == Vector2Int.left)
        {
            _leftEntrance.Unblock();
            room._rightEntrance.Unblock();
        }
    }

    public Vector2Int GetDirectionToRoom(Room room)
    {
        return room.MapPoistion - _mapPosition;
    }

    public void SetDistanceFromStartRoom(int value)
    {
        if (value < 0)
        {
            throw new Exception("Distance can`t be less than zero");
        }
        _distanceFromStartRoom = value;
    }

    public bool IsRoomEquals(Room otherRoom)
    {
        return _roomID == otherRoom._roomID;
    }

    protected void ActivateTraps()
    {
        foreach (var trap in _traps)
        {
            trap.Activate();
        }
    }

    protected void DeactivateTraps()
    {
        foreach (var trap in _traps)
        {
            trap.Deactivate();
        }
    }

    protected void OnRoomEntered()
    {
        OnEntered?.Invoke();
    }

    protected void OnRoomCompleated()
    {
        OnCompleated?.Invoke();
    }
}
