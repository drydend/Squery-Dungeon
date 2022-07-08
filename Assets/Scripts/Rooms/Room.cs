using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    protected Vector2Int _mapPosition;

    [SerializeField]
    protected Sprite _minimapIcon;
    [SerializeField]
    protected GameObject _gatewayPrefab;
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

    public virtual event Action OnEntered;
    public virtual event Action OnCompleated;

    public List<Room> ConnectedRooms => _connectedRooms;
    public Sprite MinimapIcon => _minimapIcon;
    public Vector2Int MapPoistion => _mapPosition;

    public void Initialize(Vector2Int mapPosition)
    {
        OnEntered += ActivateTraps;
        OnCompleated += DeactivateTraps;
        _mapPosition = mapPosition;
        _upperEntrance.Block();
        _rightEntrance.Block();
        _leftEntrance.Block();
        _lowerEntrance.Block();
    }

    public void ConnectToRoom(Room room)
    {
        room._connectedRooms.Add(this);
        _connectedRooms.Add(room);

        var directionToRoom = GetDirectionToRoom(room);

        var gatewayPosition = transform.position + (room.transform.position - transform.position) / 2;
        var gatewayRotation = (room.MapPoistion - MapPoistion).x == 0 ? 90 : 0;

        Instantiate(_gatewayPrefab, gatewayPosition, Quaternion.Euler(0, 0, gatewayRotation));

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

    private void ActivateTraps()
    {
        foreach (var trap in _traps)
        {
            trap.Activate();
        }
    }

    private void DeactivateTraps()
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
