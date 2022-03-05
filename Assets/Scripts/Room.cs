using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Vector2Int _mapPosition;
    private float _levelDifficulty;
    private int _maxConnections;
    private int _connectionsAmount;
    private RoomState _roomState = RoomState.NotFinished;
    private List<EnemyWave> _enemyWaves;
    [SerializeField]
    private int _maxEnemiesPerWave;

    [SerializeField]
    private GameObject _gatewayPrefab;
    [SerializeField]
    private RoomEntrance _upperEntrance;
    [SerializeField]
    private RoomEntrance _lowerEntrance;
    [SerializeField]
    private RoomEntrance _rightEntrance;
    [SerializeField]
    private RoomEntrance _leftEntrance;

    public int MaxConnections => _maxConnections;
    public float LevelDifficulty => _levelDifficulty;
    public bool CanBeConnected => _maxConnections > _connectionsAmount;
    public Vector2Int MapPoistion => _mapPosition;

    public void Initialize(Vector2Int mapPosition, int connectionsAmount, List<EnemyWave> enemyWaves)
    {
        _enemyWaves = _enemyWaves;
        _mapPosition = mapPosition;
        _maxConnections = connectionsAmount;
        _upperEntrance.Block();
        _rightEntrance.Block();
        _leftEntrance.Block();
        _lowerEntrance.Block();
    }

    public bool TryConnectToRoom(Room room)
    {
        if (CanBeConnected && room.CanBeConnected)
        {
            room._connectionsAmount++;
            _connectionsAmount++;
            var directionToRoom =  room.MapPoistion - _mapPosition;

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
            else if(directionToRoom == Vector2Int.right)
            {
                _rightEntrance.Unblock();
                room._leftEntrance.Unblock();
            }
            else if (directionToRoom == Vector2Int.left)
            {
                _leftEntrance.Unblock();
                room._rightEntrance.Unblock();
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartRoomTrial()
    {
        _upperEntrance.Close();
        _lowerEntrance.Close();
        _rightEntrance.Close();
        _leftEntrance.Close();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character character) && _roomState == RoomState.NotFinished)
        {
            _roomState = RoomState.InProcess;
            StartRoomTrial();
        }
    }
}

enum RoomState
{   
    NotFinished,
    Finished,
    InProcess,
}
